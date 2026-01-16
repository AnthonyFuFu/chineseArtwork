IF NOT EXISTS (
    SELECT 1 
    FROM sys.databases 
    WHERE name = N'chinese_artwork'
)
BEGIN
    CREATE DATABASE chinese_artwork;
END
GO
USE chinese_artwork;
GO
-- 會員
CREATE TABLE [member] (
    MEM_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,         -- 會員ID
    MEM_NAME NVARCHAR(45) NOT NULL,                        -- 姓名
    MEM_ACCOUNT VARCHAR(50) NOT NULL,                      -- 帳號
    MEM_PASSWORD VARCHAR(20) NOT NULL,                     -- 密碼
    MEM_GENDER CHAR(1),                                    -- 性別(f:女 m:男)
    MEM_PHONE VARCHAR(20),                                 -- 電話
    MEM_EMAIL VARCHAR(50) NOT NULL,                        -- 信箱
    MEM_ADDRESS NVARCHAR(100),                             -- 地址
    MEM_BIRTHDAY DATE,                                     -- 生日
    MEM_PICTURE VARCHAR(300),                              -- 會員圖片路徑
    MEM_IMAGE VARCHAR(100),                                -- 會員圖片名稱
    MEM_STATUS INT DEFAULT 1,                              -- 會員狀態(1:正常 0:停權)
    MEM_VERIFICATION_STATUS INT DEFAULT 1,                 -- 會員驗證狀態(1:開通 0:停權)
    MEM_VERIFICATION_CODE VARCHAR(100),                    -- 會員驗證碼
    MEM_GOOGLE_UID VARCHAR(100),                           -- 會員googleUid
    CONSTRAINT UQ_MEMBER_ACCOUNT UNIQUE (MEM_ACCOUNT)
);
INSERT INTO [member]
(
    MEM_NAME,
    MEM_ACCOUNT,
    MEM_PASSWORD,
    MEM_GENDER,
    MEM_PHONE,
    MEM_EMAIL,
    MEM_ADDRESS,
    MEM_BIRTHDAY,
    MEM_STATUS,
    MEM_VERIFICATION_STATUS,
    MEM_GOOGLE_UID
)
VALUES
(N'傅勝宏', 'FU830917', '830917', 'm', '0999000000', 's9017611@gmail.com', N'台北市中正區博愛路36號', '1994-09-17', 1, 1, NULL),
(N'阿勝',   'ASHENG',   '830917', 'f', '0988000000', 's9017622@gmail.com', N'台北市中正區博愛路36號', '1994-09-17', 1, 1, NULL),
(N'傅',     'FU30917',  '830917', 'm', '0999000000', 's9017633@gmail.com', N'台北市中正區博愛路36號', '1994-09-17', 1, 1, NULL),
(N'勝',     'FU83917',  '830917', 'm', '0999000000', 's9017644@gmail.com', N'台北市中正區博愛路36號', '1994-09-17', 1, 1, NULL),
(N'宏',     'FU83097',  '830917', 'm', '0999000000', 's9017655@gmail.com', N'台北市中正區博愛路36號', '1994-09-17', 1, 1, NULL);
-- 藝術家
CREATE TABLE artist (
    ART_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,         -- 藝術家ID
    ART_NAME NVARCHAR(20) NOT NULL,                        -- 姓名
    ART_PHONE VARCHAR(20) NOT NULL,                        -- 電話
    ART_GENDER CHAR(1),                                    -- 性別(f:女 m:男)
    ART_GIVEN_NAME NVARCHAR(10) NOT NULL,                  -- 名
    ART_COURTESY_NAME NVARCHAR(10) NOT NULL,               -- 字
    ART_PSEUDONYM_NAME NVARCHAR(10) NOT NULL,              -- 號
    ART_BIRTHDAY DATE,                                     -- 生日
    ART_PICTURE VARCHAR(300),                              -- 照片路徑
    ART_IMAGE VARCHAR(100),                                -- 照片名稱
    ART_ACCOUNT VARCHAR(20) NOT NULL,                      -- 帳號
    ART_PASSWORD VARCHAR(20) NOT NULL,                     -- 密碼
    ART_EMAIL VARCHAR(40) NOT NULL,                        -- 信箱
    ART_STATUS INT NOT NULL DEFAULT 1,                     -- 狀態(0:停權 1:啟用)
    CONSTRAINT UQ_ART_ACCOUNT UNIQUE (ART_ACCOUNT)
);
INSERT INTO artist (
    ART_NAME,
    ART_PHONE,
    ART_GENDER,
    ART_GIVEN_NAME,
    ART_COURTESY_NAME,
    ART_PSEUDONYM_NAME,
    ART_BIRTHDAY,
    ART_PICTURE,
    ART_IMAGE,
    ART_ACCOUNT,
    ART_PASSWORD,
    ART_EMAIL,
    ART_STATUS
) VALUES
(N'傅勝宏', '0912345678', 'm', N'勝宏', N'勝子', N'宏者', '1994-09-17', 'sealcarving/admin/upload/images/carver/user.jpg', 'user.jpg', 'sealcarver101', 'carverpassword1', 's9017688@yahoo.com.tw', 1),
(N'傅勝宏', '0912345678', 'm', N'勝宏', N'勝', N'者', '1994-09-17', 'sealcarving/admin/upload/images/carver/user.jpg', 'user.jpg', 'sealcarver102', 'carverpassword2', 's9017611@yahoo.com.tw', 1),
(N'傅勝宏', '0912345678', 'f', N'勝宏', N'子', N'宏', '1994-09-17', 'sealcarving/admin/upload/images/carver/user.jpg', 'user.jpg', 'sealcarver103', 'carverpassword3', 's9017622@yahoo.com.tw', 1);
-- 作品分類
CREATE TABLE category (
    CAT_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,         -- 作品分類ID
    CAT_NAME NVARCHAR(30) NOT NULL,                        -- 作品分類名稱
    CAT_KEYWORD NVARCHAR(200) NOT NULL,                    -- 作品分類關鍵字
    CAT_STATUS INT NOT NULL DEFAULT 1                      -- 狀態(0:下架 1:上架)
);
INSERT INTO category (CAT_NAME, CAT_KEYWORD, CAT_STATUS) VALUES
(N'人物肖像', N'肖像,人物,寫實,藝術', 1),
(N'風景畫', N'山水,風景,自然,抽象', 1),
(N'抽象創作', N'抽象,色彩,自由,創意', 1);
-- 作品風格
CREATE TABLE style (
    STYLE_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,       -- 作品風格ID
    STYLE_NAME NVARCHAR(30) NOT NULL,                      -- 作品風格名稱
    STYLE_KEYWORD NVARCHAR(200) NOT NULL,                  -- 作品風格關鍵字(抽象、寫實、寫意…)
    STYLE_STATUS INT NOT NULL DEFAULT 1                    -- 狀態(0:下架 1:上架)
);
INSERT INTO style (STYLE_NAME, STYLE_KEYWORD, STYLE_STATUS) VALUES
(N'寫實', N'真實,精細,細膩,立體感', 1),
(N'印象派', N'光影,色彩,筆觸,印象', 1),
(N'水墨', N'水墨,傳統,中國畫,意境', 1);
-- 作品
CREATE TABLE artwork (
    AW_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,          -- 作品ID
    ART_ID INT NOT NULL,                                   -- 藝術家ID
    CAT_ID INT NOT NULL,                                   -- 分類ID
    STYLE_ID INT NOT NULL,                                 -- 風格ID
    AW_TITLE NVARCHAR(100) NOT NULL,                       -- 作品名稱
    AW_DESCRIPTION NVARCHAR(500) NOT NULL,                 -- 作品說明 / 創作理念
    AW_PRICE DECIMAL(10,2) NOT NULL,                       -- 作品原價
    AW_CREATED DATETIME2(3) NOT NULL,                      -- 創作年月
    AW_DIMENSION NVARCHAR(50) NOT NULL,                    -- 尺寸(長×寬×高)
    AW_IS_FOR_SALE INT NOT NULL DEFAULT 1,                 -- 販售(0:不販售 1:販售 2:售出)
    AW_STATUS INT NOT NULL DEFAULT 1,                      -- 展示(0:不展示 1:展示)
    AW_IS_DEL INT NOT NULL DEFAULT 0,                      -- 刪除(0:未刪除 1:刪除)
    AW_CREATE_TIME DATETIME2(3) DEFAULT SYSDATETIME(),     -- 建立時間
    AW_UPDATE_TIME DATETIME2(3) DEFAULT SYSDATETIME(),     -- 修改時間
    AW_SOLD_TIME DATETIME2(3),                             -- 售出時間
    CONSTRAINT artwork_artist_fk FOREIGN KEY (ART_ID) REFERENCES artist(ART_ID),
    CONSTRAINT artwork_category_fk FOREIGN KEY (CAT_ID) REFERENCES category(CAT_ID),
    CONSTRAINT artwork_style_fk FOREIGN KEY (STYLE_ID) REFERENCES style(STYLE_ID)
);
INSERT INTO artwork (
    ART_ID,
    CAT_ID,
    STYLE_ID,
    AW_TITLE,
    AW_DESCRIPTION,
    AW_PRICE,
    AW_CREATED,
    AW_DIMENSION,
    AW_IS_FOR_SALE,
    AW_STATUS,
    AW_IS_DEL
) VALUES
(1, 1, 1, N'晨曦', N'描繪清晨的光線與溫暖氛圍，表達新開始的希望', 15000.00, '2025-01-01', N'50×70×2 cm', 1, 1, 0),
(2, 2, 2, N'夜之舞', N'以抽象手法表現夜晚城市的節奏與燈光律動', 22000.50, '2024-12-15', N'60×80×3 cm', 1, 1, 0),
(3, 3, 3, N'山水情', N'傳統水墨風格，描繪山川與雲霧的意境', 18000.75, '2023-08-10', N'100×150×5 cm', 1, 1, 0);
-- 作品圖片
CREATE TABLE artwork_pic (
    AW_PIC_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,      -- 作品圖片ID
    AW_ID INT,                                             -- 作品ID
    AW_PIC_SORT INT,                                       -- 作品圖片排序
    AW_PICTURE VARCHAR(300),                               -- 作品圖片路徑
    AW_IMAGE VARCHAR(100),                                 -- 作品圖片名稱
    CONSTRAINT artwork_pic_artwork_fk FOREIGN KEY (AW_ID) REFERENCES artwork(AW_ID)
);
INSERT INTO artwork_pic (AW_ID, AW_PIC_SORT, AW_PICTURE, AW_IMAGE)
VALUES
    (1, 1, 'chineseArtwork/admin/upload/images/artworkPic/20240731093732.jpg', '20240731093732.jpg'),
    (1, 2, 'chineseArtwork/admin/upload/images/artworkPic/20240731093732.jpg', '20240731093732.jpg'),
    (1, 3, 'chineseArtwork/admin/upload/images/artworkPic/20240731093732.jpg', '20240731093732.jpg'),
    (2, 1, 'chineseArtwork/admin/upload/images/artworkPic/20240731093746.jpg', '20240731093746.jpg'),
    (2, 3, 'chineseArtwork/admin/upload/images/artworkPic/20240731093746.jpg', '20240731093746.jpg'),
    (2, 2, 'chineseArtwork/admin/upload/images/artworkPic/20240731093746.jpg', '20240731093746.jpg');
-- 字體
CREATE TABLE script_style (
    SCRIPT_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,      -- 字體ID
    SCRIPT_WORD NVARCHAR(100) NOT NULL,                    -- 字體
    SCRIPT_DESCRIPTION NVARCHAR(500) NOT NULL              -- 字體說明
);
INSERT INTO script_style (SCRIPT_WORD, SCRIPT_DESCRIPTION)
VALUES
(N'楷書', N'結構端正、筆畫清楚，常用於正式書寫'),
(N'行書', N'介於楷書與草書之間，書寫流暢'),
(N'草書', N'筆畫簡化、連筆多，具有藝術性');
-- 部首
CREATE TABLE radical (
    RADICAL_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,     -- 部首ID
    RADICAL_WORD NVARCHAR(100) NOT NULL,                   -- 部首
    RADICAL_STROKES INT                                    -- 部首筆劃數量
);
INSERT INTO radical (RADICAL_WORD, RADICAL_STROKES)
VALUES
(N'水', 4),
(N'木', 4),
(N'心', 4);
-- 部首圖片
CREATE TABLE radical_pic (
    RADICAL_PIC_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY, -- 部首圖片ID
    RADICAL_ID INT NOT NULL,                               -- 部首ID
    RADICAL_PIC_SORT INT,                                  -- 部首圖片排序
    RADICAL_PICTURE NVARCHAR(300),                         -- 部首圖片路徑
    RADICAL_IMAGE NVARCHAR(100),                           -- 部首圖片名稱
    CONSTRAINT radical_pic_radical_fk FOREIGN KEY (RADICAL_ID) REFERENCES radical(RADICAL_ID)
);
INSERT INTO radical_pic (RADICAL_ID, RADICAL_PIC_SORT, RADICAL_PICTURE, RADICAL_IMAGE)
VALUES
(1, 1, N'/images/radical/water/', N'water_01.png'),
(2, 1, N'/images/radical/wood/',  N'wood_01.png'),
(3, 1, N'/images/radical/heart/', N'heart_01.png');
-- 字典
CREATE TABLE dictionary (
    DICT_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,        -- 字典ID
    SCRIPT_ID INT NOT NULL,                                -- 字體ID
    RADICAL_ID INT NOT NULL,                               -- 部首ID
    DICT_WORD NVARCHAR(100) NOT NULL,                      -- 文字
    DICT_DESCRIPTION NVARCHAR(500) NOT NULL,               -- 說文解字
    DICT_STROKES INT NOT NULL,                             -- 文字筆劃數量
    DICT_STATUS INT NOT NULL DEFAULT 1,                    -- 展示(0:不展示 1:展示)
    DICT_IS_DEL INT NOT NULL DEFAULT 0,                    -- 刪除(0:未刪除 1:刪除)
    DICT_CREATE_TIME DATETIME2(3) DEFAULT SYSDATETIME(),   -- 建立時間
    DICT_UPDATE_TIME DATETIME2(3) DEFAULT SYSDATETIME(),   -- 修改時間
    CONSTRAINT dictionary_script_style_fk FOREIGN KEY (SCRIPT_ID) REFERENCES script_style(SCRIPT_ID),
    CONSTRAINT dictionary_radical_fk FOREIGN KEY (RADICAL_ID) REFERENCES radical(RADICAL_ID)
);
INSERT INTO dictionary
(
    SCRIPT_ID,
    RADICAL_ID,
    DICT_WORD,
    DICT_DESCRIPTION,
    DICT_STROKES,
    DICT_STATUS,
    DICT_IS_DEL
)
VALUES
(1, 1, N'河', N'水之通道也，從水可聲。', 8, 1, 0),
(2, 2, N'林', N'木之聚也，二木成林。', 8, 1, 0),
(3, 3, N'念', N'心之所思也，今心為念。', 8, 1, 0);
-- 字典圖片
CREATE TABLE dictionary_pic (
    DICT_PIC_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,    -- 字典圖片ID
    DICT_ID INT NOT NULL,                                  -- 字典ID
    DICT_PIC_SORT INT,                                     -- 字典圖片排序
    DICT_PICTURE NVARCHAR(300),                            -- 字典圖片路徑
    DICT_IMAGE NVARCHAR(100),                              -- 字典圖片名稱
    CONSTRAINT dictionary_pic_dictionary_fk FOREIGN KEY (DICT_ID) REFERENCES dictionary(DICT_ID)
);
INSERT INTO dictionary_pic
(
    DICT_ID,
    DICT_PIC_SORT,
    DICT_PICTURE,
    DICT_IMAGE
)
VALUES
(1, 1, N'/images/dictionary/river/', N'river_01.png'),
(2, 1, N'/images/dictionary/forest/', N'forest_01.png'),
(3, 1, N'/images/dictionary/thought/', N'thought_01.png');
-- 建立index
CREATE INDEX idx_radical_pic_radical_id ON radical_pic(RADICAL_ID);
CREATE INDEX idx_dictionary_script_id ON dictionary(SCRIPT_ID);
CREATE INDEX idx_dictionary_radical_id ON dictionary(RADICAL_ID);
CREATE INDEX idx_dictionary_pic_dict_id ON dictionary_pic(DICT_ID);