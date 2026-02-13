import pandas as pd
import requests
from bs4 import BeautifulSoup
import time
import os
import re

# 讀取現有的CSV檔案
csv_path = "radical_data/all_radical_characters.csv"
df = pd.read_csv(csv_path)

# 創建新欄位
df['總筆劃'] = None

# 爬取函數
def get_stroke_count(character):
    url = f"https://humanum.arts.cuhk.edu.hk/Lexis/lexi-mf/search.php?word={character}"
    try:
        headers = {
            'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36',
            'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8',
            'Accept-Language': 'zh-TW,zh;q=0.9,en-US;q=0.8,en;q=0.7'
        }
        response = requests.get(url, headers=headers)
        response.encoding = response.apparent_encoding
        # response.encoding = 'utf-8'  # 確保正確的編碼
        if response.status_code == 200:
            soup = BeautifulSoup(response.text, 'html.parser')
            
            # 直接尋找 char_stroke 元素
            char_stroke = soup.find('td', id='char_stroke')
            if char_stroke:
                # 獲取元素內容
                content = char_stroke.get_text()
                
                # 使用正則表達式提取括號內的數字
                match = re.search(r'\((\d+)\)', content)
                if match:
                    return match.group(1)  # 返回括號內的數字
                else:
                    # 如果沒有括號，則返回直接的數字
                    return content.strip()
            else:
                print(f"找不到 {character} 的筆劃資訊")
                return None
        else:
            print(f"無法獲取 {character} 的資料，狀態碼: {response.status_code}")
            return None
    except Exception as e:
        print(f"處理 {character} 時發生錯誤: {e}")
        return None

# 遍歷每個漢字並爬取總筆劃
for index, row in df.iterrows():
    character = row['漢字']
    print(f"正在處理: {character}")
    stroke_count = get_stroke_count(character)
    df.at[index, '總筆劃'] = stroke_count
    
    # 添加延遲以避免被網站封鎖
    time.sleep(0.01)
    
    # 每處理10個字保存一次，以防程序中斷
    if index % 10 == 0 and index > 0:
        df.to_csv(csv_path.replace(".csv", "_with_strokes.csv"), index=False)
        print(f"已保存進度 ({index} / {len(df)})")

# 最終保存結果
df.to_csv(csv_path.replace(".csv", "_with_strokes.csv"), index=False)
print("處理完成！")