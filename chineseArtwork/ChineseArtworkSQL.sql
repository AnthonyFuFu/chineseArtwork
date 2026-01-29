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
    MEM_STATUS INT NOT NULL DEFAULT 1,                     -- 會員狀態(1:正常 0:停權)
    MEM_VERIFICATION_STATUS INT NOT NULL DEFAULT 0,        -- 會員驗證狀態(1:開通 0:停權)
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
    ART_GIVEN_NAME NVARCHAR(10),                           -- 名
    ART_COURTESY_NAME NVARCHAR(10),                        -- 字
    ART_PSEUDONYM_NAME NVARCHAR(10),                       -- 號
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
(N'傅勝宏', '0912345678', 'm', N'勝宏', N'勝子', N'宏者', '1994-09-17', 'chineseArtwork/upload/images/artist/user.jpg', 'user.jpg', 'sealcarver101', 'carverpassword1', 's9017688@yahoo.com.tw', 1),
(N'傅勝宏', '0912345678', 'm', N'勝宏', N'勝', N'者', '1994-09-17', 'chineseArtwork/upload/images/artist/user.jpg', 'user.jpg', 'sealcarver102', 'carverpassword2', 's9017611@yahoo.com.tw', 1),
(N'傅勝宏', '0912345678', 'f', N'勝宏', N'子', N'宏', '1994-09-17', 'chineseArtwork/upload/images/artist/user.jpg', 'user.jpg', 'sealcarver103', 'carverpassword3', 's9017622@yahoo.com.tw', 1);
-- 知名藝術家
CREATE TABLE famous_artist (
    FMS_ART_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,     -- 知名藝術家ID
    FMS_ART_NAME NVARCHAR(20) NOT NULL,                    -- 知名藝術家姓名
    FMS_ART_GENDER CHAR(1),                                -- 性別(f:女 m:男)
    FMS_ART_GIVEN_NAME NVARCHAR(10),                       -- 名
    FMS_ART_COURTESY_NAME NVARCHAR(10),                    -- 字
    FMS_ART_PSEUDONYM_NAME NVARCHAR(10),                   -- 號
    FMS_ART_DESCRIPTION NVARCHAR(500),                     -- 知名藝術家介紹
    FMS_ART_PICTURE VARCHAR(300),                          -- 照片路徑
    FMS_ART_IMAGE VARCHAR(100),                            -- 照片名稱
);
INSERT INTO famous_artist (
    FMS_ART_NAME, 
    FMS_ART_GENDER, 
    FMS_ART_GIVEN_NAME, 
    FMS_ART_COURTESY_NAME, 
    FMS_ART_PSEUDONYM_NAME, 
    FMS_ART_DESCRIPTION, 
    FMS_ART_PICTURE, 
    FMS_ART_IMAGE
) VALUES
(N'王福庵','m',N'福庵',N'叔明',N'葉舟',N'王福庵（1883年－1965年），字叔明，號葉舟，浙江鄞縣人。他是中國近代著名的書法家、篆刻家，被譽為「近代篆書第一人」。','chineseArtwork/upload/images/famousArtist/wang_fu_an.jpg','wang_fu_an.jpg'),
(N'齊白石','m',N'白石',N'文圭',N'籲吟',N'齊白石（1864年1月22日－1957年9月16日），原名齊純芝，後改名璜，一作璜之，改字白石，號濰翁、老木、公遺、木人、三百石印富翁、借山吟館主者等，湖南湘潭人，近現代中國繪畫、篆刻藝術大師，善於花鳥、山水和人物畫。','chineseArtwork/upload/images/famousArtist/qi_bai_shi.jpg','qi_bai_shi.jpg'
);
-- 作品分類
CREATE TABLE category (
    CAT_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,         -- 作品分類ID
    CAT_NAME NVARCHAR(30) NOT NULL,                        -- 作品分類名稱
    CAT_KEYWORD NVARCHAR(200) NOT NULL,                    -- 作品分類關鍵字
    CAT_STATUS INT NOT NULL DEFAULT 1                      -- 狀態(0:不可選 1:可選)
);
INSERT INTO category (CAT_NAME, CAT_KEYWORD, CAT_STATUS) VALUES
(N'印章', N'篆刻,印章,書法,雕刻', 1),                        -- 把印章分類的關鍵字綁定
(N'字畫', N'書法,行書,隸書,篆書', 1),                        -- 把字畫分類的關鍵字綁定
(N'畫作', N'畫,繪畫,山水畫,花鳥畫,工筆畫,寫意', 1)            -- 把畫作分類的關鍵字綁定
;
-- 作品風格
CREATE TABLE style (
    STYLE_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,       -- 作品風格ID
    STYLE_NAME NVARCHAR(30) NOT NULL,                      -- 作品風格名稱
    STYLE_KEYWORD NVARCHAR(200) NOT NULL,                  -- 作品風格關鍵字(寫實、寫意)
    STYLE_STATUS INT NOT NULL DEFAULT 1                    -- 狀態(0:下架 1:上架)
);
INSERT INTO style (STYLE_NAME, STYLE_KEYWORD, STYLE_STATUS) VALUES
(N'寫實', N'寫實', 1),
(N'寫意', N'寫意', 1),
(N'花鳥畫', N'花鳥畫', 1),
(N'工筆畫', N'工筆畫', 1),
(N'走獸', N'走獸', 1),
(N'鳥蟲', N'鳥蟲', 1),
(N'山水', N'山水', 1),
(N'篆書', N'篆書', 1),
(N'隸書', N'隸書', 1),
(N'楷書', N'楷書', 1),
(N'草書', N'草書', 1),
(N'行書', N'行書', 1);
-- 作品
CREATE TABLE artwork (
    AW_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,          -- 作品ID
    ART_ID INT NOT NULL,                                   -- 藝術家ID
    CAT_ID INT NOT NULL,                                   -- 分類ID
    STYLE_ID INT NOT NULL,                                 -- 風格ID
    AW_TITLE NVARCHAR(100) NOT NULL,                       -- 作品名稱
    AW_DESCRIPTION NVARCHAR(500) NOT NULL,                 -- 作品說明 / 創作理念
    AW_PRICE DECIMAL(10,2) NOT NULL,                       -- 作品原價
    AW_CREATED DATETIME2(3),                               -- 創作年月
    AW_DIMENSION NVARCHAR(100),                            -- 尺寸(長×寬×高)
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
(1, 1, 1, N'吉祥', N'這是描述印章作品內容的', 15000.00, '2025-01-01', N'1.8×1.8 cm', 1, 1, 0),
(2, 3, 1, N'夜之舞', N'以寫實手法表現夜晚城市', 22000.50, '2024-12-15', N'60×80×3 cm', 1, 1, 0);
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
(1, 1, 'chineseArtwork/upload/images/artworkPic/20240731093732.jpg', '20240731093732.jpg'),
(1, 2, 'chineseArtwork/upload/images/artworkPic/20240731093732.jpg', '20240731093732.jpg'),
(1, 3, 'chineseArtwork/upload/images/artworkPic/20240731093732.jpg', '20240731093732.jpg'),
(2, 1, 'chineseArtwork/upload/images/artworkPic/20240731093746.jpg', '20240731093746.jpg'),
(2, 3, 'chineseArtwork/upload/images/artworkPic/20240731093746.jpg', '20240731093746.jpg'),
(2, 2, 'chineseArtwork/upload/images/artworkPic/20240731093746.jpg', '20240731093746.jpg');
-- 知名藝術家作品
CREATE TABLE famous_artwork (
    FMS_AW_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,      -- 知名藝術家作品ID
    FMS_ART_ID INT NOT NULL,                               -- 知名藝術家ID
    CAT_ID INT NOT NULL,                                   -- 分類ID
    STYLE_ID INT NOT NULL,                                 -- 風格ID
    FMS_AW_TITLE NVARCHAR(100) NOT NULL,                   -- 知名藝術家作品名稱
    FMS_AW_DIMENSION NVARCHAR(100),                        -- 知名藝術家作品尺寸(長×寬×高)
    FMS_AW_STATUS INT NOT NULL DEFAULT 1,                  -- 展示(0:不展示 1:展示)
    FMS_AW_CREATE_TIME DATETIME2(3) DEFAULT SYSDATETIME(), -- 建立時間
    FMS_AW_UPDATE_TIME DATETIME2(3) DEFAULT SYSDATETIME(), -- 修改時間
    CONSTRAINT famous_artwork_famous_artist_fk FOREIGN KEY (FMS_ART_ID) REFERENCES famous_artist(FMS_ART_ID),
    CONSTRAINT famous_artwork_category_fk FOREIGN KEY (CAT_ID) REFERENCES category(CAT_ID),
    CONSTRAINT famous_artwork_style_fk FOREIGN KEY (STYLE_ID) REFERENCES style(STYLE_ID)
);
INSERT INTO famous_artwork (
    FMS_ART_ID,
    CAT_ID,
    STYLE_ID,
    FMS_AW_TITLE, 
    FMS_AW_DIMENSION,
    FMS_AW_STATUS
)
VALUES 
(1, 1, 2, N'四君子篆刻作品', N'10cm x 10cm x 5cm', 1),       -- 作品 1：由王福庵創作，分類:印章，風格:寫意
(1, 2, 11, N'嵩陽書院草書對聯', N'100cm x 30cm', 1),         -- 作品 2：由王福庵創作，分類:字畫，風格:寫意
(2, 3, 4, N'螞蟻工筆畫', N'20cm x 20cm', 1),                -- 作品 3：由齊白石創作，分類:畫作，風格:寫意
(2, 3, 2, N'荷花寫意畫', N'50cm x 70cm', 1);                -- 作品 4：由齊白石創作，分類:畫作，風格:寫意
-- 知名藝術家作品圖片
CREATE TABLE famous_artwork_pic (
    FMS_AW_PIC_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,  -- 知名藝術家作品圖片ID
    FMS_AW_ID INT,                                         -- 知名藝術家作品ID
    FMS_AW_PIC_SORT INT,                                   -- 知名藝術家作品圖片排序
    FMS_AW_PICTURE VARCHAR(300),                           -- 知名藝術家作品圖片路徑
    FMS_AW_IMAGE VARCHAR(100),                             -- 知名藝術家作品圖片名稱
    CONSTRAINT famous_artwork_pic_famous_artwork_fk FOREIGN KEY (FMS_AW_ID) REFERENCES famous_artwork(FMS_AW_ID)
);
INSERT INTO famous_artwork_pic (FMS_AW_ID, FMS_AW_PIC_SORT, FMS_AW_PICTURE, FMS_AW_IMAGE)
VALUES 
-- 四君子篆刻作品 (ID: 1)
(1, 1, 'chineseArtwork/upload/images/famousArtworkPic', 'four_gentlemen_carving_1.jpg'),
(1, 2, 'chineseArtwork/upload/images/famousArtworkPic', 'four_gentlemen_carving_2.jpg'),
-- 嵩陽書院草書對聯 (ID: 2)
(2, 1, 'chineseArtwork/upload/images/famousArtworkPic', 'songyang_grass_script_couplet_1.jpg'),
(2, 2, 'chineseArtwork/upload/images/famousArtworkPic', 'songyang_grass_script_couplet_2.jpg'),
-- 螞蟻工筆畫 (ID: 3)
(3, 1, 'chineseArtwork/upload/images/famousArtworkPic', 'ant_gongbi_1.jpg'),
(3, 2, 'chineseArtwork/upload/images/famousArtworkPic', 'ant_gongbi_2.jpg'),
-- 荷花寫意畫 (ID: 4)
(4, 1, 'chineseArtwork/upload/images/famousArtworkPic', 'lotus_freehand_1.jpg'),
(4, 2, 'chineseArtwork/upload/images/famousArtworkPic', 'lotus_freehand_2.jpg');
-- 字體
CREATE TABLE script_style (
    SCRIPT_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,      -- 字體ID
    SCRIPT_WORD NVARCHAR(100) NOT NULL,                    -- 字體
    SCRIPT_DESCRIPTION NVARCHAR(500)                       -- 字體說明
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
    DICT_DESCRIPTION NVARCHAR(500),                        -- 說文解字
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