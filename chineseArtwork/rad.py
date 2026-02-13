import requests
from bs4 import BeautifulSoup
import csv
import time
import os
import socket
import urllib3

# 設置全局連接和讀取超時時間
socket.setdefaulttimeout(20)  # 20秒全局超時

def scrape_radical_page(rad_number, retries=3):
    """爬取指定部首編號的頁面，提取radical_table表格中的筆畫數和漢字"""
    url = f"https://humanum.arts.cuhk.edu.hk/Lexis/lexi-mf/radical.php?rad={rad_number}"
    
    # 添加 User-Agent 避免被網站封鎖
    # 使用你提供的完整標頭
    headers = {
        "user-agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/144.0.0.0 Safari/537.36",
        "accept": "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7",
        "accept-encoding": "gzip, deflate, br, zstd",
        "accept-language": "zh-TW,zh;q=0.9,en-US;q=0.8,en;q=0.7",
        "cache-control": "max-age=0",
        "connection": "keep-alive",
        "host": "humanum.arts.cuhk.edu.hk",
        "referer": "https://humanum.arts.cuhk.edu.hk/Lexis/lexi-mf/radical.php",
        "sec-ch-ua": "\"Not(A:Brand\";v=\"8\", \"Chromium\";v=\"144\", \"Google Chrome\";v=\"144\"",
        "sec-ch-ua-mobile": "?0",
        "sec-ch-ua-platform": "\"Windows\"",
        "sec-fetch-dest": "document",
        "sec-fetch-mode": "navigate",
        "sec-fetch-site": "same-origin",
        "sec-fetch-user": "?1",
        "upgrade-insecure-requests": "1"
    }
    
    # 設置超時參數
    timeout_params = (5, 20)  # (連接超時, 讀取超時)
    
    # 重試機制
    for attempt in range(retries):
        try:
            # 顯示重試訊息
            if attempt > 0:
                print(f"第 {attempt+1}/{retries} 次嘗試爬取部首 {rad_number}...")
                
            response = requests.get(url, headers=headers, timeout=timeout_params)
            response.raise_for_status()
            
            # 設置正確的編碼
            response.encoding = response.apparent_encoding
            
            soup = BeautifulSoup(response.text, 'html.parser')
            
            # 驗證頁面載入是否正確
            title = soup.find('title')
            if title and "Lexi-mf" in title.text:
                print(f"成功載入部首 {rad_number} 頁面")
            else:
                print(f"警告：無法確認部首 {rad_number} 頁面內容是否正確")
            
            # 尋找 radical_table 表格
            radical_table = soup.find('table', class_='radical_table')
            
            if not radical_table:
                print(f"在部首 {rad_number} 頁面找不到 radical_table 表格")
                # 保存錯誤的HTML以便檢查
                with open(f"radical_data/error_page_{rad_number}.html", 'w', encoding='utf-8') as f:
                    f.write(response.text)
                print(f"已保存頁面源碼到 radical_data/error_page_{rad_number}.html 供檢查")
                return []
            
            # 提取表格中的筆畫數和漢字
            rows = radical_table.find_all('tr')
            
            # 跳過標題行
            data = []
            for row in rows[1:]:  # 從第二行開始（跳過標題行）
                th = row.find('th')
                td = row.find('td')
                
                if th and td:
                    stroke_count = th.text.strip()  # 筆畫數
                    
                    # 找出所有漢字連結中的 span 元素
                    characters = []
                    for a_tag in td.find_all('a'):
                        span = a_tag.find('span')
                        if span:
                            characters.append(span.text.strip())
                    
                    # 對每個筆畫數建立多個資料行，每行一個漢字
                    for char in characters:
                        data.append([stroke_count, char])
            
            # 添加一些基本數據驗證
            if data:
                print(f"成功從部首 {rad_number} 提取了 {len(data)} 個漢字數據")
            else:
                print(f"警告：部首 {rad_number} 沒有提取到任何數據")
            
            return data
            
        except requests.exceptions.Timeout:
            print(f"爬取部首 {rad_number} 超時，等待後重試...")
            # 增加延遲時間進行重試
            time.sleep(5)
        except requests.exceptions.ConnectionError as e:
            print(f"連接錯誤：{e}")
            # 檢查是否能 ping 通網站
            try:
                import subprocess
                ping_result = subprocess.run(["ping", "-n", "2", "humanum.arts.cuhk.edu.hk"], 
                                          capture_output=True, text=True)
                print(f"Ping 結果: {ping_result.stdout}")
            except:
                print("無法執行 ping 測試")
            
            print(f"網絡連接問題，稍後重試...")
            time.sleep(10)  # 等待時間更長
        except Exception as e:
            print(f"爬取部首 {rad_number} 時發生其他錯誤: {e}")
            time.sleep(3)
    
    # 如果所有重試都失敗
    print(f"經過 {retries} 次嘗試後仍無法成功爬取部首 {rad_number}")
    return []

def check_internet_connection():
    """檢查網際網路連接是否正常"""
    try:
        # 嘗試訪問 Google DNS 服務器
        socket.create_connection(("8.8.8.8", 53), timeout=5)
        print("網際網路連接正常")
        return True
    except OSError:
        print("警告：無法連接到網際網路，請檢查網絡設置")
        return False

def main():
    # 檢查網絡連接
    if not check_internet_connection():
        print("由於網絡連接問題，程式終止")
        return
        
    # 檢查目標網站是否可訪問
    try:
        test_response = requests.get("https://humanum.arts.cuhk.edu.hk", timeout=10)
        print(f"目標網站可訪問，狀態碼：{test_response.status_code}")
    except Exception as e:
        print(f"無法訪問目標網站: {e}")
        print("程式將繼續，但可能會遇到更多錯誤")

    # 創建資料夾存放結果
    if not os.path.exists("radical_data"):
        os.makedirs("radical_data")
    
    # 爬取 rad=1 到 rad=214 的所有頁面
    for rad_number in range(1, 215):
        print(f"\n========== 正在爬取部首 {rad_number}/214... ==========")
        
        data = scrape_radical_page(rad_number)
        
        if data:
            # 保存為 CSV 檔案
            csv_filename = f"radical_data/radical_{rad_number}_characters.csv"
            with open(csv_filename, 'w', newline='', encoding='utf-8-sig') as f:
                writer = csv.writer(f)
                writer.writerow(["筆畫數", "漢字"])  # 寫入標題行
                writer.writerows(data)
            
            print(f"部首 {rad_number} 的數據已保存到 {csv_filename}")
        else:
            print(f"部首 {rad_number} 沒有數據可保存")
        
        # 動態調整爬蟲間隔時間，避免被封鎖
        wait_time = 0.2 + (rad_number % 3)  # 2-4秒隨機延遲
        print(f"等待 {wait_time} 秒後繼續...")
        time.sleep(wait_time)
    
    # 合併所有資料到一個主檔案
    merge_all_data()

def merge_all_data():
    """合併所有CSV檔案到一個主檔案"""
    all_data = []
    
    # 讀取所有CSV檔案
    for rad_number in range(1, 215):
        csv_filename = f"radical_data/radical_{rad_number}_characters.csv"
        if os.path.exists(csv_filename):
            try:
                with open(csv_filename, 'r', encoding='utf-8-sig') as f:
                    reader = csv.reader(f)
                    # 跳過每個檔案的標題行
                    next(reader, None)
                    for row in reader:
                        # 添加部首編號
                        all_data.append([rad_number] + row)
            except Exception as e:
                print(f"讀取檔案 {csv_filename} 時出錯: {e}")
    
    if all_data:
        # 寫入主檔案
        with open("radical_data/all_radical_characters.csv", 'w', newline='', encoding='utf-8-sig') as f:
            writer = csv.writer(f)
            writer.writerow(["部首編號", "筆畫數", "漢字"])
            writer.writerows(all_data)
        
        print(f"所有資料已合併到 radical_data/all_radical_characters.csv，共 {len(all_data)} 筆記錄")
    else:
        print("沒有任何數據可合併")

if __name__ == "__main__":
    main()