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
INSERT INTO [member] (
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
) VALUES
(N'傅勝宏', N'FU830917', N'830917', N'm', N'0999000000', N's9017611@gmail.com', N'台北市中正區博愛路36號', N'1994-09-17', 1, 1, NULL),
(N'阿勝',   N'ASHENG',   N'830917', N'f', N'0988000000', N's9017622@gmail.com', N'台北市中正區博愛路36號', N'1994-09-17', 1, 1, NULL),
(N'傅',     N'FU30917',  N'830917', N'm', N'0999000000', N's9017633@gmail.com', N'台北市中正區博愛路36號', N'1994-09-17', 1, 1, NULL),
(N'勝',     N'FU83917',  N'830917', N'm', N'0999000000', N's9017644@gmail.com', N'台北市中正區博愛路36號', N'1994-09-17', 1, 1, NULL),
(N'宏',     N'FU83097',  N'830917', N'm', N'0999000000', N's9017655@gmail.com', N'台北市中正區博愛路36號', N'1994-09-17', 1, 1, NULL);
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
(N'傅勝宏', N'0912345678', N'm', N'勝宏', N'勝子', N'宏者', N'1994-09-17', N'/upload/images/artist/user1.jpg', N'user1.jpg', N'artist01', N'artistpassword1', N's9017688@yahoo.com.tw', 1),
(N'傅勝宏', N'0912345678', N'm', N'勝宏', N'勝', N'者', N'1994-09-17', N'/upload/images/artist/user2.jpg', N'user2.jpg', N'artist02', N'artistpassword2', N's9017611@yahoo.com.tw', 1),
(N'傅勝宏', N'0912345678', N'f', N'勝宏', N'子', N'宏', N'1994-09-17', N'/upload/images/artist/user3.jpg', N'user3.jpg', N'artist03', N'artistpassword3', N's9017622@yahoo.com.tw', 1);
-- 聊天室 --
CREATE TABLE chat_room (
    ROOM_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,        -- 聊天室ID
    ROOM_URL VARCHAR(300),                                 -- 聊天室URL
    ROOM_STATUS INT NOT NULL DEFAULT 1,                    -- 聊天室狀態
    ROOM_UPDATE_STATUS INT NOT NULL DEFAULT 1,             -- 聊天室已讀狀態
    ROOM_LAST_UPDATE DATETIME2(3) DEFAULT SYSDATETIME(),   -- 建立時間
);
INSERT INTO chat_room (
    ROOM_URL,
    ROOM_STATUS,
    ROOM_UPDATE_STATUS,
    ROOM_LAST_UPDATE
) VALUES
    (N'chatRoomUrl',1,0,NOW());
-- 聊天紀錄 --
CREATE TABLE [message] (
    MSG_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,         -- 聊天紀錄ID
	ART_ID INT NOT NULL,                                   -- 藝術家ID
	MEM_ID INT NOT NULL,                                   -- 會員ID
	ROOM_ID INT NOT NULL,                                  -- 聊天室ID
    MSG_CONTENT NVARCHAR(3000) NOT NULL,                   -- 聊天內容
    MSG_TIME DATETIME2(3) DEFAULT SYSDATETIME(),           -- 訊息時間
    MSG_DIRECTION INT NOT NULL,                            -- 發送方向(0:藝術家對會員 1:會員對藝術家)
    MSG_PICTURE NVARCHAR(300),                             -- 聊天圖片路徑
    MSG_IMAGE NVARCHAR(100),                               -- 聊天圖片名稱
    CONSTRAINT message_member_fk FOREIGN KEY (MEM_ID) REFERENCES [member] (MEM_ID),
    CONSTRAINT message_chat_room_fk FOREIGN KEY (ROOM_ID) REFERENCES chat_room(ROOM_ID)
);
INSERT INTO [message] (
    ART_ID,
    MEM_ID,
    ROOM_ID,
    MSG_CONTENT,
    MSG_DIRECTION
) VALUES
    (1,1,1,N'HI',0),
    (1,1,1,N'Hello',0);
-- 最新消息 --
CREATE TABLE news (
    NEWS_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,        -- 最新消息ID
    NEWS_NAME VARCHAR(50) NOT NULL,                        -- 最新消息名稱
    NEWS_CONTENT VARCHAR(3000) NOT NULL,                   -- 最新消息內容
    NEWS_STATUS INT NOT NULL DEFAULT 1,                    -- 最新消息狀態 (0:下架, 1:上架)
    NEWS_START DATETIME,                                   -- 最新消息開始時間
    NEWS_END DATETIME,                                     -- 最新消息結束時間
    NEWS_CREATE_BY NVARCHAR(100) NOT NULL,                 -- 最新消息建立人
    NEWS_CREATE_DATE DATETIME DEFAULT SYSDATETIME(),       -- 最新消息建立時間
    NEWS_UPDATE_BY NVARCHAR(100) NOT NULL,                 -- 最新消息修改人
    NEWS_UPDATE_DATE DATETIME DEFAULT SYSDATETIME()        -- 最新消息修改時間
);
INSERT INTO news (
    NEWS_NAME,
    NEWS_CONTENT,
    NEWS_STATUS,
    NEWS_START,
    NEWS_END,
    NEWS_CREATE_BY,
    NEWS_UPDATE_BY
) VALUES
    ('篆刻展覽', '篆刻展覽資訊', 1, CAST(CONVERT(DATE, GETDATE()) AS DATETIME), DATEADD(DAY, 7, CAST(CONVERT(DATE, GETDATE()) AS DATETIME)),N'傅勝宏',N'傅勝宏'),
    ('書法展覽', '書法展覽資訊', 1, CAST(CONVERT(DATE, GETDATE()) AS DATETIME), DATEADD(DAY, 7, CAST(CONVERT(DATE, GETDATE()) AS DATETIME)),N'傅勝宏',N'傅勝宏'),
    ('水墨展覽', '水墨展覽資訊', 1, CAST(CONVERT(DATE, GETDATE()) AS DATETIME), DATEADD(DAY, 7, CAST(CONVERT(DATE, GETDATE()) AS DATETIME)),N'傅勝宏',N'傅勝宏');
-- 公告 Banner --
CREATE TABLE banner (
    BAN_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,         -- 公告BannerID
    NEWS_ID INT NOT NULL,                                  -- 最新消息ID
    BAN_PICTURE VARCHAR(300),                              -- 公告Banner圖片路徑
    BAN_IMAGE VARCHAR(100),                                -- 公告Banner圖片名稱
    CONSTRAINT banner_news_fk FOREIGN KEY (NEWS_ID) REFERENCES news(NEWS_ID)
);
INSERT INTO banner (
    NEWS_ID,
    BAN_PICTURE,
    BAN_IMAGE
) VALUES
    (1, N'/upload/images/banner/seal_carving.jpg', N'seal_carving.jpg'),
    (2, N'/upload/images/banner/calligraphy.jpg', N'calligraphy.jpg'),
    (3, N'/upload/images/banner/ink_painting.jpg', N'ink_painting.jpg');
-- 學會
CREATE TABLE group_info (
    GROUP_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,       -- 學會ID
    GROUP_NAME NVARCHAR(100) NOT NULL,                     -- 學會名稱
    GROUP_DESCRIPTION NVARCHAR(500),                       -- 學會描述
    GROUP_PHONE VARCHAR(20),                               -- 學會聯絡電話
    GROUP_EMAIL VARCHAR(50),                               -- 學會聯絡電子郵件
    GROUP_ADDRESS NVARCHAR(200),                           -- 學會地址
    GROUP_ESTABLISHED_DATE DATE DEFAULT GETDATE(),         -- 成立日期
    GROUP_OWNER NVARCHAR(100),                             -- 負責人
    GROUP_STATUS INT NOT NULL DEFAULT 1,                   -- 學會狀態（0:停用 1:啟用）
);
INSERT INTO group_info (
    GROUP_NAME,
    GROUP_DESCRIPTION,
    GROUP_PHONE,
    GROUP_EMAIL,
    GROUP_ADDRESS,
    GROUP_OWNER
) VALUES
    (N'書法愛好者學會', N'專注於書法藝術及推廣的協會', N'02-12345678', N'calligraphy@association.org', N'Taipei, Taiwan', N'李大文'),
    (N'國際藝術家聯盟', N'國際支持藝術發展的平台與協會', N'03-87654321', N'intl.art@association.org', N'Taichung, Taiwan', N'王小明'),
    (N'東方藝術思想協會', N'注重東方藝術與哲學的研究與合作', N'04-90876543', N'eastern_art@association.org', N'Kaohsiung, Taiwan', N'張三豐');
-- 入會
CREATE TABLE association (
    ASSOC_ID INT IDENTITY(1,1) PRIMARY KEY,                -- 入會ID
    ART_ID INT NOT NULL,                                   -- 藝術家ID
    GROUP_ID INT NOT NULL,                                 -- 學會ID
    ASSOC_JOIN_DATE DATE DEFAULT GETDATE(),                -- 入會日期
    ASSOC_ROLE NVARCHAR(50),                               -- 入會角色（例如「會員」或「理事」）
    ASSOC_STATUS INT NOT NULL DEFAULT 1,                   -- 入會狀態（0: 停用，1: 啟用，2: 永久會員）
    ASSOC_PAYMENT_DATE DATE DEFAULT GETDATE(),             -- 最近繳費日期
    ASSOC_VALID_UNTIL DATE,                                -- 繳費有效期
    ASSOC_REMARK NVARCHAR(500),                            -- 備註
    CONSTRAINT association_artist_fk FOREIGN KEY (ART_ID) REFERENCES artist(ART_ID),
    CONSTRAINT association_group_info_fk FOREIGN KEY (GROUP_ID) REFERENCES group_info(GROUP_ID)
);
INSERT INTO association (
    ART_ID,
    GROUP_ID,
    ASSOC_ROLE,
    ASSOC_STATUS,
    ASSOC_VALID_UNTIL,
    ASSOC_REMARK
) VALUES 
    (1, 1, N'會員', 1, '2026-10-01', N'學會年度費用已繳納'),
    (1, 2, N'理事', 1, '2026-10-02', N'協會支持藝術推廣'),
    (2, 1, N'會員', 1, '2025-10-03', N'初次加入'),
    (3, 3, N'會員', 1, '2025-12-31', NULL);

-- 功能-- 
CREATE TABLE [function] (
    FUNC_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,        -- 功能ID
    FUNC_NAME NVARCHAR(20) NOT NULL,                       -- 功能名稱
    FUNC_LAYER NVARCHAR(5) NOT NULL,                       -- 功能層級
    FUNC_PARENT_ID NVARCHAR(5) NOT NULL,                   -- 功能隸屬
    FUNC_LINK NVARCHAR(100) NOT NULL,                      -- 功能連結
    FUNC_STATUS INT DEFAULT 1,                             -- 功能狀態
    FUNC_ICON NVARCHAR(50) DEFAULT 1,                      -- ICON
);
INSERT INTO [function] (
    FUNC_NAME,
    FUNC_LAYER,
    FUNC_PARENT_ID,
    FUNC_LINK,
    FUNC_STATUS,
    FUNC_ICON
) VALUES
(N'功能管理',N'1',N'0',N'/admin/function/list',1,N'shop'),
(N'會員管理',N'1',N'0',N'/admin/member/list',1,N'people'),
(N'字典管理',N'1',N'0',N'/admin/dictionary/list',1,N'group_add'),
(N'作品管理',N'1',N'0',N'/admin/artwork/list',1,N'date_range'),
(N'藝術家管理',N'1',N'0',N'/admin/artist/list',1,N'payment'),
(N'詩詞管理',N'1',N'0',N'/admin/poetry/list',1,N'date_range');
-- 管理員權限 --
CREATE TABLE authority (
    ART_ID INT NOT NULL,                                   -- 管理員ID
    FUNC_ID INT NOT NULL,                                  -- 功能ID
    AUTH_STATUS INT NOT NULL DEFAULT 1,                    -- 權限狀態
    CONSTRAINT authority_artist_fk FOREIGN KEY (ART_ID) REFERENCES artist(ART_ID),
    CONSTRAINT authority_function_fk FOREIGN KEY (FUNC_ID) REFERENCES [function](FUNC_ID), 
    CONSTRAINT authority_ART_ID_FUNC_ID_pk PRIMARY KEY (ART_ID, FUNC_ID)
);
INSERT INTO authority (
    ART_ID,
    FUNC_ID,
    AUTH_STATUS
) VALUES
(1,1,1),(1,2,1),(1,3,1),(1,4,1),(1,5,1),(1,6,1),
(2,1,1),(2,2,1),(2,3,1),(2,4,1),(2,5,1),(2,6,1),
(3,1,1),(3,2,1),(3,3,1),(3,4,1),(3,5,1),(3,6,1);
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
(N'王福庵',N'm',N'福庵',N'叔明',N'葉舟',N'王福庵（1883年－1965年），字叔明，號葉舟，浙江鄞縣人。他是中國近代著名的書法家、篆刻家，被譽為「近代篆書第一人」。',N'/upload/images/famousArtist/wang_fu_an.jpg',N'wang_fu_an.jpg'),
(N'齊白石',N'm',N'白石',N'文圭',N'籲吟',N'齊白石（1864年1月22日－1957年9月16日），原名齊純芝，後改名璜，一作璜之，改字白石，號濰翁、老木、公遺、木人、三百石印富翁、借山吟館主者等，湖南湘潭人，近現代中國繪畫、篆刻藝術大師，善於花鳥、山水和人物畫。',N'/upload/images/famousArtist/qi_bai_shi.jpg',N'qi_bai_shi.jpg'
);
-- 作品分類
CREATE TABLE category (
    CAT_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,         -- 作品分類ID
    CAT_NAME NVARCHAR(30) NOT NULL,                        -- 作品分類名稱
    CAT_KEYWORD NVARCHAR(200) NOT NULL,                    -- 作品分類關鍵字
    CAT_STATUS INT NOT NULL DEFAULT 1                      -- 狀態(0:不可選 1:可選)
);
INSERT INTO category (
    CAT_NAME,
    CAT_KEYWORD,
    CAT_STATUS
) VALUES
(N'印章', N'篆刻,印章,書法,雕刻', 1),                        -- 把印章分類的關鍵字綁定
(N'字畫', N'書法,行書,隸書,篆書', 1),                        -- 把字畫分類的關鍵字綁定
(N'畫作', N'畫,繪畫,山水畫,花鳥畫,工筆畫,寫意', 1)            -- 把畫作分類的關鍵字綁定
;
-- 作品風格
CREATE TABLE style (
    STYLE_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,       -- 作品風格ID
    CAT_ID INT NOT NULL,                                   -- 分類ID
    STYLE_NAME NVARCHAR(30) NOT NULL,                      -- 作品風格名稱
    STYLE_KEYWORD NVARCHAR(200) NOT NULL,                  -- 作品風格關鍵字(寫實、寫意)
    STYLE_DESCRIPTION NVARCHAR(500),                       -- 作品風格說明
    STYLE_STATUS INT NOT NULL DEFAULT 1                    -- 狀態(0:下架 1:上架)
    CONSTRAINT style_category_fk FOREIGN KEY (CAT_ID) REFERENCES category(CAT_ID),
);
INSERT INTO style (
    CAT_ID,
    STYLE_NAME,
    STYLE_KEYWORD,
    STYLE_DESCRIPTION,
    STYLE_STATUS
) VALUES
(3,N'寫實', N'寫實', N'寫實', 1),
(3,N'寫意', N'寫意', N'寫意', 1),
(3,N'花鳥畫', N'花鳥畫', N'花鳥畫', 1),
(3,N'工筆畫', N'工筆畫', N'工筆畫', 1),
(3,N'走獸', N'走獸', N'走獸', 1),
(3,N'鳥蟲', N'鳥蟲', N'鳥蟲', 1),
(3,N'山水', N'山水', N'山水', 1),
(2,N'篆書', N'篆書', N'篆書', 1),
(2,N'隸書', N'隸書', N'隸書', 1),
(2,N'楷書', N'楷書', N'楷書', 1),
(2,N'草書', N'草書', N'草書', 1),
(2,N'行書', N'行書', N'行書', 1),
(1,N'印篆', N'印篆', N'印篆', 1);
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
(1, 1, 13, N'吉祥', N'這是描述印章作品內容的', 15000.00, N'2025-01-01', N'1.8×1.8 cm', 1, 1, 0),
(2, 3, 1, N'夜之舞', N'以寫實手法表現夜晚城市', 22000.50, N'2024-12-15', N'60×80×3 cm', 1, 1, 0);
-- 作品圖片
CREATE TABLE artwork_pic (
    AW_PIC_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,      -- 作品圖片ID
    AW_ID INT,                                             -- 作品ID
    AW_PIC_SORT INT,                                       -- 作品圖片排序
    AW_PICTURE VARCHAR(300),                               -- 作品圖片路徑
    AW_IMAGE VARCHAR(100),                                 -- 作品圖片名稱
    CONSTRAINT artwork_pic_artwork_fk FOREIGN KEY (AW_ID) REFERENCES artwork(AW_ID)
);
INSERT INTO artwork_pic (
    AW_ID,
    AW_PIC_SORT,
    AW_PICTURE,
    AW_IMAGE
) VALUES
(1, 1, N'/upload/images/artworkPic/artwork_pic1.jpg', N'artwork_pic1.jpg'),
(1, 2, N'/upload/images/artworkPic/artwork_pic2.jpg', N'artwork_pic2.jpg'),
(1, 3, N'/upload/images/artworkPic/artwork_pic3.jpg', N'artwork_pic3.jpg'),
(2, 1, N'/upload/images/artworkPic/artwork_pic4.jpg', N'artwork_pic4.jpg'),
(2, 3, N'/upload/images/artworkPic/artwork_pic5.jpg', N'artwork_pic5.jpg'),
(2, 2, N'/upload/images/artworkPic/artwork_pic6.jpg', N'artwork_pic6.jpg');
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
) VALUES 
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
INSERT INTO famous_artwork_pic (
    FMS_AW_ID,
    FMS_AW_PIC_SORT,
    FMS_AW_PICTURE,
    FMS_AW_IMAGE
) VALUES 
-- 四君子篆刻作品 (ID: 1)
(1, 1, N'/upload/images/famousArtworkPic/four_gentlemen_carving_1.jpg', N'four_gentlemen_carving_1.jpg'),
(1, 2, N'/upload/images/famousArtworkPic/four_gentlemen_carving_2.jpg', N'four_gentlemen_carving_2.jpg'),
-- 嵩陽書院草書對聯 (ID: 2)
(2, 1, N'/upload/images/famousArtworkPic/songyang_grass_script_couplet_1.jpg', N'songyang_grass_script_couplet_1.jpg'),
(2, 2, N'/upload/images/famousArtworkPic/songyang_grass_script_couplet_2.jpg', N'songyang_grass_script_couplet_2.jpg'),
-- 螞蟻工筆畫 (ID: 3)
(3, 1, N'/upload/images/famousArtworkPic/ant_gongbi_1.jpg', N'ant_gongbi_1.jpg'),
(3, 2, N'/upload/images/famousArtworkPic/ant_gongbi_2.jpg', N'ant_gongbi_2.jpg'),
-- 荷花寫意畫 (ID: 4)
(4, 1, N'/upload/images/famousArtworkPic/lotus_freehand_1.jpg', N'lotus_freehand_1.jpg'),
(4, 2, N'/upload/images/famousArtworkPic/lotus_freehand_2.jpg', N'lotus_freehand_2.jpg');
-- 部首
CREATE TABLE radical (
    RADICAL_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,     -- 部首ID
    RADICAL_WORD NVARCHAR(100) NOT NULL,                   -- 部首
    RADICAL_STROKES INT                                    -- 部首筆劃數量
);
INSERT INTO radical (
    RADICAL_WORD,
    RADICAL_STROKES
) VALUES
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
INSERT INTO radical_pic (
    RADICAL_ID,
    RADICAL_PIC_SORT,
    RADICAL_PICTURE,
    RADICAL_IMAGE
) VALUES
(1, 1, N'/upload/images/radical/water/', N'water_01.png'),
(2, 1, N'/upload/images/radical/wood/',  N'wood_01.png'),
(3, 1, N'/upload/images/radical/heart/', N'heart_01.png');
-- 字典
CREATE TABLE dictionary (
    DICT_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,        -- 字典ID
    RADICAL_ID INT NOT NULL,                               -- 部首ID
    DICT_WORD NVARCHAR(20) NOT NULL,                       -- 文字
    DICT_DESCRIPTION NVARCHAR(500),                        -- 說文解字
    DICT_STROKES INT NOT NULL,                             -- 文字筆劃數量
    DICT_STATUS INT NOT NULL DEFAULT 1,                    -- 展示(0:不展示 1:展示)
    DICT_IS_DEL INT NOT NULL DEFAULT 0,                    -- 刪除(0:未刪除 1:刪除)
    DICT_CREATE_TIME DATETIME2(3) DEFAULT SYSDATETIME(),   -- 建立時間
    DICT_UPDATE_TIME DATETIME2(3) DEFAULT SYSDATETIME(),   -- 修改時間
    CONSTRAINT dictionary_radical_fk FOREIGN KEY (RADICAL_ID) REFERENCES radical(RADICAL_ID)
);
INSERT INTO dictionary (
    RADICAL_ID,
    DICT_WORD,
    DICT_DESCRIPTION,
    DICT_STROKES,
    DICT_STATUS,
    DICT_IS_DEL
) VALUES
(1, N'河', N'水之通道也，從水可聲。', 8, 1, 0),
(2, N'林', N'木之聚也，二木成林。', 8, 1, 0),
(3, N'念', N'心之所思也，今心為念。', 8, 1, 0);
-- 字典圖片
CREATE TABLE dictionary_pic (
    DICT_PIC_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,    -- 字典圖片ID
    DICT_ID INT NOT NULL,                                  -- 字典ID
    STYLE_ID INT NOT NULL,                                 -- 作品風格ID
    DICT_PIC_SORT INT,                                     -- 字典圖片排序
    DICT_PICTURE NVARCHAR(300),                            -- 字典圖片路徑
    DICT_IMAGE NVARCHAR(100),                              -- 字典圖片名稱
    CONSTRAINT dictionary_pic_dictionary_fk FOREIGN KEY (DICT_ID) REFERENCES dictionary(DICT_ID),
    CONSTRAINT dictionary_pic_style_fk FOREIGN KEY (STYLE_ID) REFERENCES style(STYLE_ID)
);
INSERT INTO dictionary_pic (
    DICT_ID,
    STYLE_ID,
    DICT_PIC_SORT,
    DICT_PICTURE,
    DICT_IMAGE
) VALUES
(1, 8, 1, N'/upload/images/dictionary/river/', N'river_01.png'),
(2, 8, 1, N'/upload/images/dictionary/forest/', N'forest_01.png'),
(3, 8, 1, N'/upload/images/dictionary/thought/', N'thought_01.png');
-- 朝代
CREATE TABLE dynasty (
    DYNASTY_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,     -- 朝代ID
    DYNASTY_NAME NVARCHAR(50) NOT NULL,                    -- 朝代名稱（例如：唐、宋、元）
    DYNASTY_DESCRIPTION NVARCHAR(500)                      -- 朝代描述（選填，例如該時代背景、歷史事件等）
);
INSERT INTO dynasty (
    DYNASTY_NAME,
    DYNASTY_DESCRIPTION
) VALUES 
(N'周', N'中國曆史上存在時間最長的朝代。分為西周和東周，其中東周又分為春秋和戰國時期。'),
(N'秦', N'統一天下的第一個封建王朝，歷時約15年。'),
(N'漢', N'分為西漢與東漢，是中國歷史上重要的封建王朝。'),
(N'唐', N'詩歌文化繁榮的時代，中國古代歷史的盛世。'),
(N'宋', N'宋代分為北宋和南宋，經濟文化高度發展。');
-- 詩詞作者
CREATE TABLE author (
    AUTHOR_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,      -- 詩詞作者ID
    DYNASTY_ID INT NOT NULL,                               -- 對應的朝代ID
    AUTHOR_NAME NVARCHAR(100) NOT NULL,                    -- 詩詞作者姓名（例如：杜甫、李白）
    AUTHOR_GIVEN_NAME NVARCHAR(50),                        -- 名
    AUTHOR_COURTESY_NAME NVARCHAR(50),                     -- 字
    AUTHOR_PSEUDONYM_NAME NVARCHAR(50),                    -- 號
    AUTHOR_DESCRIPTION NVARCHAR(1000),                     -- 作者介紹
    CONSTRAINT author_dynasty_fk FOREIGN KEY (DYNASTY_ID) REFERENCES dynasty(DYNASTY_ID)
);
INSERT INTO author (
    DYNASTY_ID,
    AUTHOR_NAME,
    AUTHOR_GIVEN_NAME,
    AUTHOR_COURTESY_NAME,
    AUTHOR_PSEUDONYM_NAME,
    AUTHOR_DESCRIPTION
) VALUES 
(4, N'李白', N'白', N'太白', N'青蓮居士', N'唐代大詩人，被譽為“詩仙”，以浪漫主義詩風聞名，代表作品有《靜夜思》、《將進酒》等。'),
(4, N'杜甫', N'甫', N'子美', N'少陵野老', N'唐代詩人，被後人尊稱為“詩聖”，作品多記錄戰亂現狀與百姓痛苦，代表作品有《春望》、《登高》。'),
(5, N'蘇軾', N'軾', N'子瞻', N'東坡居士', N'北宋時期著名文學家，書法家，善於詩詞散文，代表作品有《水調歌頭》、《赤壁賦》等。'),
(5, N'辛棄疾', N'棄疾', N'幼安', N'稼軒居士', N'南宋豪放派詞人，詞作多表現抗金抱負，代表作品有《破陣子》、《青玉案·元夕》等。');
-- 詩詞
CREATE TABLE poetry (
    POETRY_ID INT IDENTITY(1,1) PRIMARY KEY,               -- 詩詞ID
    AUTHOR_ID INT NOT NULL,                                -- 詩詞作者ID
    POETRY_TITLE NVARCHAR(255) NOT NULL,                   -- 詩詞標題
    POETRY_CONTENT NTEXT,                                  -- 詩詞內容
    POETRY_WORD_COUNT INT,                                 -- 字數
    POETRY_ANALYSIS NTEXT,                                 -- 詩詞賞析
    POETRY_CATEGORY NVARCHAR(100),                         -- 詩詞主題分類
    POETRY_KEYWORDS NVARCHAR(500),                         -- 詩詞關鍵詞
    POETRY_ADDED_BY NVARCHAR(100) NOT NULL,                -- 收錄者（管理者）
    POETRY_TRANSLATION NTEXT,                              -- 詩詞翻譯
    CONSTRAINT poetry_author_fk FOREIGN KEY (AUTHOR_ID) REFERENCES author(AUTHOR_ID)
);
INSERT INTO poetry (
    AUTHOR_ID,
    POETRY_TITLE,
    POETRY_CONTENT,
    POETRY_WORD_COUNT,
    POETRY_ANALYSIS,
    POETRY_CATEGORY,
    POETRY_KEYWORDS,
    POETRY_ADDED_BY,
    POETRY_TRANSLATION
) VALUES 
(1, N'靜夜思', N'床前明月光，疑是地上霜。舉頭望明月，低頭思故鄉。', 20, N'這是一首短小卻讓人回味無窮的田園詩，表達了漂泊異鄉的孤獨與濃厚的思鄉之情。', N'思鄉', N'月光, 思鄉, 故鄉', 'admin', 'Before my bed, the moonlight shines, resembling frost on the ground. Looking up I see the bright moon, then looking down I miss home.'),
(2, N'春望', N'國破山河在，城春草木深。感時花濺淚，恨別鳥驚心。烽火連三月，家書抵萬金。白頭搔更短，渾欲不勝簪。', 56, N'此詩主要表現的是杜甫對戰亂之苦的憂世之心，著重表現對國破家亡的哀愁之情。', N'時政', N'戰爭, 思鄉, 愛國', 'admin', 'The country’s ruined, but hills and rivers remain. In spring, flowering plants and trees exude sadness. Warflare lasts three months, a word from home is worth its weight in gold. I scratch my graying hair in anxiety until it’s too thin to hold my hairpin.'),
(3, N'水調歌頭·明月幾時有', N'明月幾時有？把酒問青天。不知天上宮闕，今夕是何年？我欲乘風歸去，唯恐瓊樓玉宇，高處不勝寒。起舞弄清影，何似在人間！\n轉朱閣，低綺戶，照無眠。不應有恨，何事長向別時圓！人有悲歡離合，月有陰晴圓缺，此事古難全。但願人長久，千里共嬋娟。', 129,N'極富哲理的詞作，反映了對人生、親情的思考，通過月亮的陰晴圓缺象徵生活中的聚散離別。',N'哲理',N'月亮, 哲理, 思念', 'admin','When will the moon be bright and clear? / With a cup of wine in my hand, I ask the sky. / In the heavens on this night, I wonder what year it would be.'),
(4, N'青玉案·元夕', N'東風夜放花千樹。更吹落、星如雨。寶馬雕車香滿路。鳳簫聲動，玉壺光轉，一夜魚龍舞。\n蛾兒雪柳黃金縷。笑語盈盈暗香去。眾裏尋他千百度。驀然回首，那人卻在，燈火闌珊處。', 117, N'這首詞以華麗浪漫的筆調描寫元宵佳節的熱鬧景象，結尾處抒發了對愛情的執著與美好的憧憬。', N'愛情',N'元宵節, 愛情, 喜悅', 'admin','The east wind brings to life a thousand blossoming trees, / And blows down stars in showers to the ground. / Jewelled horse carriages fill the fragrant lanes, / The phoenix flute plays, jade pot of light turns, / One whole night of fish and dragon dance. / I look for her in vain, / When at last I turn round, / There she is, by the lanterns dimly lit.');
-- 建立index
-- artwork 表相關
CREATE INDEX idx_artwork_artist_id ON artwork (ART_ID);
CREATE INDEX idx_artwork_category_id ON artwork (CAT_ID);
CREATE INDEX idx_artwork_style_id ON artwork (STYLE_ID);
-- famous_artwork 表相關
CREATE INDEX idx_famous_artwork_status ON famous_artwork (FMS_AW_STATUS);
CREATE INDEX idx_famous_artwork_famous_artist_id ON famous_artwork (FMS_ART_ID);
-- radical 部首表相關
CREATE INDEX idx_radical_pic_radical_id ON radical_pic(RADICAL_ID);
-- dictionary 字典及圖片表相關
CREATE INDEX idx_dictionary_radical_id ON dictionary(RADICAL_ID);
CREATE INDEX idx_dictionary_pic_dict_id ON dictionary_pic(DICT_ID);
-- poetry 詩詞表相關
CREATE INDEX idx_poetry_author_id ON poetry (AUTHOR_ID);
CREATE INDEX idx_poetry_added_by ON poetry (POETRY_ADDED_BY);
-- author 作者表相關
CREATE INDEX idx_author_dynasty_id ON author (DYNASTY_ID);
-- member 表相關
CREATE INDEX idx_member_account ON member (MEM_ACCOUNT);
-- artist 表相關
CREATE INDEX idx_artist_phone ON artist (ART_PHONE);
CREATE INDEX idx_artist_account ON artist (ART_ACCOUNT);
