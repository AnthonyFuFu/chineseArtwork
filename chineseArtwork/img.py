import pandas as pd
import requests
from bs4 import BeautifulSoup
import time
import os
import re
import urllib.parse
import csv
from tqdm import tqdm

BASE_URL = "https://humanum.arts.cuhk.edu.hk/Lexis/lexi-mf/"

# ========= 工具函式 =========

# def sanitize_filename(name):
#     return re.sub(r'[\\/:*?"<>|]', '_', name)

def sanitize_filename(name):
    # 只處理真正的檔名用字串
    # HTML 不清理
    if "<img" in name:
        return name
    return re.sub(r'[\\/:*?"<>|]', '_', name)

def clean_html_title(raw_html):
    soup = BeautifulSoup(raw_html, "html.parser")
    text = soup.get_text(separator="_")  # 可以用下劃線替代標籤
    return re.sub(r'[\\/:*?"<>|]', '_', text)


def download_dynamic_image(session, img_url, save_path, referer_url=None):
    """
    通用下載圖片，如果是 PHP 生成的圖片（如 silk.php、bronze.php）則帶 Referer。
    """
    if not img_url.startswith("http"):
        img_url = urllib.parse.urljoin(BASE_URL, img_url)

    # 判斷是否為 PHP 生成圖片
    if ".php" in img_url:
        headers = {
            'User-Agent': 'Mozilla/5.0',
            'Referer': referer_url or BASE_URL
        }
    else:
        headers = {
            'User-Agent': 'Mozilla/5.0'
        }

    try:
        r = session.get(img_url, headers=headers)
        if r.status_code == 200 and r.content:
            with open(save_path, "wb") as f:
                f.write(r.content)
            print("已下載:", save_path)
            return True
        else:
            print("下載失敗，status:", r.status_code, img_url)
    except Exception as e:
        print("下載失敗:", e, img_url)
    return False

def parse_and_download_ancient_images(session, character):

    encoded_char = urllib.parse.quote(character)
    url = f"{BASE_URL}search.php?word={encoded_char}"

    response = session.get(url)
    response.encoding = 'utf-8'

    if response.status_code != 200:
        print("讀取失敗:", character)
        return []

    soup = BeautifulSoup(response.text, 'html.parser')
    char_div = soup.find(id="char_anc_div")

    if not char_div:
        print("無古文字資料:", character)
        return []

    base_folder = character
    os.makedirs(base_folder, exist_ok=True)

    csv_rows = []

    tables = char_div.find_all("table", class_="char_anc_table")

    for table in tables:

        th = table.find("th")
        if not th:
            continue

        style_name = th.get_text(strip=True)

        if "部件" in style_name:
            continue

        style_folder = os.path.join(base_folder, style_name)
        os.makedirs(style_folder, exist_ok=True)

        imgs = table.find_all("img")

        for img in imgs:

            img_src = img.get("src")
            if not img_src:
                continue

            mouseover = img.get("onmouseover", "")
            match = re.search(r"mouseoverlayer\(1, '(.+?)'\)", mouseover)

            if match:
                
                raw_title = match.group(1)
                soup_title = BeautifulSoup(raw_title, "html.parser")

                clean_title = clean_html_title(raw_title)

                for bronze_img in soup_title.find_all("img"):
                    src = bronze_img.get("src", "")
                    code_match = re.search(r'code=([A-Z0-9]+)', src)
                    if code_match:
                        code = code_match.group(1)
                        bronze_img['src'] = f"bronzeRemark_images/{code}.png"
            else:
                clean_title = "unknown"

            clean_title = sanitize_filename(clean_title)

            # 自動判斷 PHP 或普通圖片
            ext = ".jpg" if ".jpg" in img_src else ".png"
            if ".php" in img_src:
                ext = ".png"  # PHP 生成的圖片統一存成 png

            save_path = os.path.join(style_folder, clean_title + ext)

            if not os.path.exists(save_path):
                download_dynamic_image(session, img_src, save_path, referer_url=url)

            csv_rows.append({
                "書體": clean_title,
                "圖片路徑": save_path
            })

            # 處理 bronzeRemark 圖片
            bronze_imgs = re.findall(r'bronzeRemark\.php\?code=([A-Z0-9]+)', mouseover)
            if bronze_imgs:

                remark_folder = os.path.join(base_folder, "bronzeRemark_images")
                os.makedirs(remark_folder, exist_ok=True)

                for code in bronze_imgs:
                    remark_url = f"{BASE_URL}bronzeRemark.php?code={code}"
                    remark_path = os.path.join(remark_folder, f"{code}.png")

                    if not os.path.exists(remark_path):
                        download_dynamic_image(session, remark_url, remark_path)

    # 建立 CSV
    csv_path = os.path.join(base_folder, "image_data.csv")

    with open(csv_path, "w", newline='', encoding='utf-8-sig') as f:
        writer = csv.DictWriter(f, fieldnames=["漢字", "書體", "圖片路徑"])
        writer.writeheader()

        for r in csv_rows:
            r["漢字"] = character
        writer.writerows(csv_rows)

    print("完成:", character)
    return csv_rows


# ========= 主程式 =========
if __name__ == "__main__":

    csv_path = "radical_characters/all_radical_characters.csv"
    df = pd.read_csv(csv_path, encoding='utf-8-sig')

    session = requests.Session()
    session.headers.update({
        'User-Agent': 'Mozilla/5.0'
    })

    # ===== 全字庫總 CSV =====
    master_csv_path = "all_characters_images.csv"

    # 如果已存在就讀取（避免重跑）
    if os.path.exists(master_csv_path):
        master_df = pd.read_csv(master_csv_path, encoding='utf-8-sig')
        processed_chars = set(master_df["漢字"].unique())
    else:
        master_df = pd.DataFrame(columns=["漢字", "書體", "圖片路徑"])
        processed_chars = set()

    # ===== 進度條 =====
    for index, row in tqdm(df.iterrows(), total=len(df), desc="處理進度"):

        character = row['漢字']

        # ✅ 自動跳過已完成字
        if character in processed_chars:
            continue

        retry_count = 0
        success = False

        # ✅ 失敗重試機制（最多3次）
        while retry_count < 3 and not success:
            try:
                rows = parse_and_download_ancient_images(session, character) or []
                # 加入「漢字」欄位
                for r in rows:
                    r["漢字"] = character

                # 合併進 master_df
                if rows:
                    master_df = pd.concat([master_df, pd.DataFrame(rows)], ignore_index=True)

                success = True

            except Exception as e:
                retry_count += 1
                print(f"錯誤，重試 {retry_count}/3:", character, e)
                time.sleep(0.5)

        time.sleep(0.5)

        # ===== 每10個字保存一次 =====
        if (index + 1) % 10 == 0:
            master_df.to_csv(master_csv_path, index=False, encoding='utf-8-sig')
            print(f"已保存進度 ({index+1}/{len(df)})")

    # ===== 最終保存 =====
    master_df.to_csv(master_csv_path, index=False, encoding='utf-8-sig')

    print("全部處理完成")