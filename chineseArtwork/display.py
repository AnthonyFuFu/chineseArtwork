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

# ==========================================================
# 工具函式
# ==========================================================

def sanitize_filename(name):
    return re.sub(r'[\\/:*?"<>|]', '_', name)

def clean_html_title(raw_html):
    soup = BeautifulSoup(raw_html, "html.parser")
    text = soup.get_text(separator="_")
    return sanitize_filename(text)

def build_display_name(raw_html):
    """
    建立顯示名稱：
    - 保留 bronzeRemark 圖片
    - 改成本地路徑 bronzeRemark_images/xxx.png
    - 其他 HTML 移除
    """
    soup = BeautifulSoup(raw_html, "html.parser")

    for img in soup.find_all("img"):
        src = img.get("src", "")
        code_match = re.search(r'code=([A-Z0-9]+)', src)
        if code_match:
            code = code_match.group(1)
            img['src'] = f"bronzeRemark_images/{code}.png"

    parts = []

    for element in soup.contents:
        if getattr(element, "name", None) == "img":
            parts.append(str(element))
        else:
            text = BeautifulSoup(str(element), "html.parser").get_text()
            if text.strip():
                parts.append(text.strip())

    return "_".join(parts)

def download_dynamic_image(session, img_url, save_path, referer_url=None):

    if not img_url.startswith("http"):
        img_url = urllib.parse.urljoin(BASE_URL, img_url)

    headers = {'User-Agent': 'Mozilla/5.0'}

    if ".php" in img_url:
        headers['Referer'] = referer_url or BASE_URL

    try:
        r = session.get(img_url, headers=headers)
        if r.status_code == 200 and r.content:
            with open(save_path, "wb") as f:
                f.write(r.content)
            return True
    except:
        pass

    return False


# ==========================================================
# 解析與下載
# ==========================================================

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
                clean_title = clean_html_title(raw_title)
                display_name = build_display_name(raw_title)
            else:
                clean_title = "unknown"
                display_name = "unknown"

            ext = ".jpg" if ".jpg" in img_src else ".png"
            if ".php" in img_src:
                ext = ".png"

            save_path = os.path.join(style_folder, clean_title + ext)

            if not os.path.exists(save_path):
                download_dynamic_image(session, img_src, save_path, referer_url=url)

            # ==================================================
            # 下載 bronzeRemark 圖片
            # ==================================================
            bronze_imgs = re.findall(r'bronzeRemark\.php\?code=([A-Z0-9]+)', mouseover)

            if bronze_imgs:
                remark_folder = os.path.join(base_folder, "bronzeRemark_images")
                os.makedirs(remark_folder, exist_ok=True)

                for code in bronze_imgs:
                    remark_url = f"{BASE_URL}bronzeRemark.php?code={code}"
                    remark_path = os.path.join(remark_folder, f"{code}.png")

                    if not os.path.exists(remark_path):
                        download_dynamic_image(session, remark_url, remark_path)

            csv_rows.append({
                "漢字": character,
                "書體": clean_title,
                "圖片路徑": save_path,
                "顯示名稱": display_name
            })

    # ==================================================
    # 每個字建立 image_data.csv
    # ==================================================
    if csv_rows:
        per_char_csv = os.path.join(base_folder, "image_data.csv")

        with open(per_char_csv, "w", newline='', encoding='utf-8-sig') as f:
            writer = csv.DictWriter(
                f,
                fieldnames=["漢字", "書體", "圖片路徑", "顯示名稱"]
            )
            writer.writeheader()
            writer.writerows(csv_rows)

    print("完成:", character)
    return csv_rows


# ==========================================================
# 主程式（每次完整重跑）
# ==========================================================

if __name__ == "__main__":

    csv_path = "radical_characters/all_radical_characters.csv"
    df = pd.read_csv(csv_path, encoding='utf-8-sig')

    session = requests.Session()
    session.headers.update({'User-Agent': 'Mozilla/5.0'})

    master_csv_path = "all_characters_images.csv"

    # 每次都重新建立
    master_df = pd.DataFrame(
        columns=["漢字", "書體", "圖片路徑", "顯示名稱"]
    )

    for index, row in tqdm(df.iterrows(), total=len(df), desc="處理進度"):

        character = row['漢字']

        try:
            rows = parse_and_download_ancient_images(session, character)
            if rows:
                master_df = pd.concat(
                    [master_df, pd.DataFrame(rows)],
                    ignore_index=True
                )
        except Exception as e:
            print("錯誤:", character, e)

        time.sleep(0.5)

        if (index + 1) % 10 == 0:
            master_df.to_csv(master_csv_path, index=False, encoding='utf-8-sig')
            print(f"已保存進度 ({index+1}/{len(df)})")

    master_df.to_csv(master_csv_path, index=False, encoding='utf-8-sig')

    print("全部處理完成")