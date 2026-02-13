import requests
from bs4 import BeautifulSoup
import time
import pandas as pd
import os

def scrape_radical_page(rad_number):
    """爬取指定部首編號的頁面內容"""
    url = f"https://humanum.arts.cuhk.edu.hk/Lexis/lexi-mf/radical.php?rad={rad_number}"
    
    # 添加 User-Agent 避免被網站封鎖
    headers = {
        "User-Agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36"
    }
    
    try:
        response = requests.get(url, headers=headers)
        response.raise_for_status()  # 如果請求不成功則拋出異常
        
        # 檢測網頁編碼，確保中文正確顯示
        response.encoding = response.apparent_encoding
        
        soup = BeautifulSoup(response.text, 'html.parser')
        
        # 獲取部首資訊
        radical_info = {}
        
        # 通常部首資訊會在頁面的主要內容區域
        # 以下是示例提取方式，可能需要根據實際網頁結構調整
        try:
            # 找出部首名稱和其他相關資訊
            radical_title = soup.find('h1').text.strip() if soup.find('h1') else "未找到標題"
            radical_info['title'] = radical_title
            
            # 獲取部首表格內容
            tables = soup.find_all('table')
            characters_data = []
            
            if tables:
                for table in tables:
                    rows = table.find_all('tr')
                    for row in rows:
                        cells = row.find_all(['td', 'th'])
                        if cells:
                            row_data = [cell.text.strip() for cell in cells]
                            characters_data.append(row_data)
            
            radical_info['characters'] = characters_data
            
            # 提取頁面中的其他重要資訊
            # 這部分需要根據網頁實際結構進行定制化提取
            main_content = soup.find('div', class_='content') or soup.find('main') or soup.find('body')
            if main_content:
                paragraphs = main_content.find_all('p')
                radical_info['description'] = "\n".join([p.text.strip() for p in paragraphs])
            
        except Exception as e:
            print(f"解析部首 {rad_number} 頁面時發生錯誤: {e}")
        
        return radical_info
        
    except requests.exceptions.RequestException as e:
        print(f"請求部首 {rad_number} 時發生錯誤: {e}")
        return None

def main():
    """主函數：爬取所有部首頁面並保存數據"""
    all_radical_data = []
    
    # 創建保存結果的資料夾
    if not os.path.exists("radical_data"):
        os.makedirs("radical_data")
    
    # 爬取 rad=1 到 rad=214 的所有頁面
    for rad_number in range(1, 215):
        print(f"正在爬取部首 {rad_number}/214...")
        
        radical_data = scrape_radical_page(rad_number)
        if radical_data:
            radical_data['rad_number'] = rad_number
            all_radical_data.append(radical_data)
            
            # 將單個部首數據保存為 JSON
            with open(f"radical_data/radical_{rad_number}.json", "w", encoding="utf-8") as f:
                import json
                json.dump(radical_data, f, ensure_ascii=False, indent=4)
            
            # 將包含漢字的數據轉換為 DataFrame 並保存為 CSV
            if 'characters' in radical_data and radical_data['characters']:
                try:
                    df = pd.DataFrame(radical_data['characters'])
                    # 如果第一行是標題，則設置為列名
                    if len(df) > 0:
                        df.to_csv(f"radical_data/radical_{rad_number}_characters.csv", 
                                  encoding="utf-8-sig", index=False)
                except Exception as e:
                    print(f"保存部首 {rad_number} 的字符數據時發生錯誤: {e}")
        
        # 爬蟲間隔時間，避免對服務器造成過大壓力
        time.sleep(0.1)
    
    # 保存所有部首的總結數據
    try:
        with open("radical_data/all_radicals_summary.json", "w", encoding="utf-8") as f:
            import json
            summary_data = [{
                'rad_number': item['rad_number'],
                'title': item.get('title', ''),
                'character_count': len(item.get('characters', [])) if 'characters' in item else 0
            } for item in all_radical_data]
            json.dump(summary_data, f, ensure_ascii=False, indent=4)
            
        print("爬蟲完成！所有數據已保存到 radical_data 資料夾")
    except Exception as e:
        print(f"保存總結數據時發生錯誤: {e}")

if __name__ == "__main__":
    main()