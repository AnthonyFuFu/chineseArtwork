import pandas as pd
import requests
from bs4 import BeautifulSoup
import time
import os
import re
import urllib.parse

# 讀取現有的CSV檔案
csv_path = "radical_data/all_radical_characters.csv"
df = pd.read_csv(csv_path)

# 創建新欄位
df['總筆劃'] = None
df['說文解字'] = None  # 新增說文解字欄位

# 爬取函數
def get_character_info(character):
    # 對中文字符進行URL編碼
    encoded_char = urllib.parse.quote(character)
    url = f"https://humanum.arts.cuhk.edu.hk/Lexis/lexi-mf/search.php?word={encoded_char}"
    
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
            result = {}
            
            # 1. 獲取總筆劃
            char_stroke = soup.find('td', id='char_stroke')
            if char_stroke:
                content = char_stroke.get_text()
                match = re.search(r'\((\d+)\)', content)
                if match:
                    result['stroke_count'] = match.group(1)
                else:
                    result['stroke_count'] = content.strip()
            else:
                result['stroke_count'] = None
                
            # 2. 獲取說文解字內容
            shuowen_table = soup.find('table', id='shuoWenTable')
            if shuowen_table:
                # 獲取第二個tr
                trs = shuowen_table.find_all('tr')
                if len(trs) >= 2:
                    # 獲取第二個tr中的第二個td
                    tds = trs[1].find_all('td')
                    if len(tds) >= 2:
                        # 擷取第二個td的完整文字內容
                        explanation_td = tds[1]
                        
                        # 獲取純文本內容，不包含任何標籤
                        main_text = ''
                        for content in explanation_td.contents:
                            if isinstance(content, str):
                                main_text += content.strip()
                        
                        # 獲取所有span標籤內容
                        spans = explanation_td.find_all('span')
                        span_texts = []
                        for span in spans:
                            span_text = span.get_text().strip()
                            if span_text:
                                span_texts.append(span_text)
                        
                        # 組合內容：主要文本加上所有span標籤內容
                        combined_text = main_text
                        if span_texts:
                            combined_text += " " + " ".join(span_texts)
                        
                        result['shuowen'] = combined_text.strip()
                    else:
                        result['shuowen'] = None
                else:
                    result['shuowen'] = None
            else:
                result['shuowen'] = None
                
            return result
        else:
            print(f"無法獲取 {character} 的資料，狀態碼: {response.status_code}")
            return {'stroke_count': None, 'shuowen': None}
    except Exception as e:
        print(f"處理 {character} 時發生錯誤: {e}")
        return {'stroke_count': None, 'shuowen': None}

# 遍歷每個漢字並爬取資料
for index, row in df.iterrows():
    character = row['漢字']
    print(f"正在處理: {character}")
    
    info = get_character_info(character)
    df.at[index, '總筆劃'] = info['stroke_count']
    df.at[index, '說文解字'] = info['shuowen']
    
    # 添加延遲以避免被網站封鎖
    time.sleep(0.01)
    
    # 每處理10個字保存一次，以防程序中斷
    if index % 10 == 0 and index > 0:
        df.to_csv(csv_path.replace(".csv", "_with_data.csv"), index=False)
        df.to_csv(csv_path, index=False, encoding='utf-8-sig')
        print(f"已保存進度 ({index} / {len(df)})")

# 最終保存結果
df.to_csv(csv_path.replace(".csv", "_with_data.csv"), index=False)
df.to_csv(csv_path, index=False, encoding='utf-8-sig')
print("處理完成！")