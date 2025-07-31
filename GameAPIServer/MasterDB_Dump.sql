use master_db;

DROP TABLE IF EXISTS version;
CREATE TABLE version (
    app_version VARCHAR(50) NOT NULL PRIMARY KEY, -- 애플리케이션 버전
    master_data_version VARCHAR(50) NOT NULL -- 마스터 데이터 버전
);

-- Attendance 테이블
DROP TABLE IF EXISTS attendance_book;
CREATE TABLE attendance_book (
    attendance_book_id BIGINT NOT NULL PRIMARY KEY, -- 출석 ID
    start_dt DATETIME NOT NULL, -- 출석 시작 날짜
    end_dt DATETIME NOT NULL, -- 출석 종료 날짜
    create_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP -- 생성일시
);

-- DayInAttendance 테이블
DROP TABLE IF EXISTS day_in_attendance_book;
CREATE TABLE day_in_attendance_book (
    attendance_book_id BIGINT NOT NULL, -- 출석 ID
    attendance_day INT NOT NULL, -- 출석 일수 (1부터 시작)
    reward_id INT NOT NULL, -- 보상 ID
    reward_type_cd CHAR(2) NOT NULL, -- 보상 타입 (예: "01": gold, "02": item)
    reward_qty INT NOT NULL, -- 보상 수량
    PRIMARY KEY (attendance_book_id, attendance_day)
);

DROP TABLE IF EXISTS currency;
CREATE TABLE currency (
    currency_id INT NOT NULL PRIMARY KEY, -- 화폐 ID
    currency_name VARCHAR(50) NOT NULL, -- 화폐 이름
    description VARCHAR(255) NOT NULL, -- 화폐 설명
    premium_yn CHAR(1) NOT NULL DEFAULT 0, -- 프리미엄 여부 (0: 일반, 1: 프리미엄)
    create_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP -- 생성일시
);


DROP TABLE IF EXISTS item;
CREATE TABLE item (
    item_id INT NOT NULL PRIMARY KEY, -- 아이템 ID
    item_name VARCHAR(100) NOT NULL, -- 아이템 이름
    item_description VARCHAR(255) NOT NULL, -- 아이템 설명
    item_type_cd CHAR(2) NOT NULL, -- 아이템 타입 코드 (예: "WP": 무기, "AR": 방어구)
    rarity_cd CHAR(1) NOT NULL, -- 아이템 희귀도 (C: 일반, U: 희귀, H: 영웅, L: 전설, M: 신화)
    create_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP -- 생성일시
);

DROP TABLE IF EXISTS guide_mission;
CREATE TABLE guide_mission (
    guide_mission_seq INT PRIMARY KEY AUTO_INCREMENT,
    title VARCHAR(100) NOT NULL,
    description VARCHAR(255) NOT NULL,
    update_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- 수정일시
    reward_type_cd VARCHAR(2) NOT NULL,
    reward_id INT NOT NULL,
    reward_qty INT NOT NULL
);

DROP TABLE IF EXISTS stage;
CREATE TABLE stage (
    stage_id INT PRIMARY KEY,                -- 스테이지 ID (백의 자리: 챕터, 0~99: 스테이지)
    boss_stage_yn CHAR(1) NOT NULL,          -- 보스 여부 (Y: 보스, N: 일반)
    reward_type_cd VARCHAR(10) NOT NULL,     -- 리워드 타입 코드
    reward_id INT NOT NULL,                  -- 리워드 ID
    reward_qty INT NOT NULL                  -- 리워드 수량
);

INSERT INTO stage (stage_id, boss_stage_yn, reward_type_cd, reward_id, reward_qty) VALUES
(101, 'N', '01', 1001, 100),   -- 1챕터 1스테이지
(102, 'N', '01', 1002, 120),   -- 1챕터 2스테이지
(103, 'N', '01', 1003, 140),   -- 1챕터 3스테이지
(104, 'N', '01', 1004, 160),   -- 1챕터 4스테이지
(105, 'Y', '01', 1005, 200),   -- 1챕터 5스테이지 (보스)
(201, 'N', '01', 2001, 200),   -- 2챕터 1스테이지
(202, 'N', '01', 2002, 220),   -- 2챕터 2스테이지
(203, 'N', '01', 2003, 240),   -- 2챕터 3스테이지
(204, 'N', '01', 2004, 260),   -- 2챕터 4스테이지
(205, 'Y', '01', 2005, 300);   -- 2챕터 5스테이지 (보스)

INSERT INTO guide_mission (title, description, reward_type_cd, reward_id, reward_qty) VALUES
('튜토리얼 시작', '튜토리얼을 시작하세요.', '01', 1001, 100),
('첫 전투 승리', '첫 전투에서 승리하세요.', '01', 1002, 200),
('장비 착용', '장비를 한 번 착용하세요.', '02', 2001, 1),
('친구 추가', '친구를 한 명 추가하세요.', '01', 1003, 300),
('길드 가입', '길드에 가입하세요.', '01', 1004, 400),
('스테이지 1 클리어', '스테이지 1을 클리어하세요.', '02', 2002, 2),
('아이템 강화', '아이템을 한 번 강화하세요.', '01', 1005, 500),
('퀘스트 완료', '퀘스트를 한 번 완료하세요.', '01', 1006, 600),
('상점 이용', '상점을 한 번 이용하세요.', '02', 2003, 3),
('캐릭터 레벨업', '캐릭터를 레벨업하세요.', '01', 1007, 700);

-- version 테이블 임시 데이터
INSERT INTO version (app_version, master_data_version) VALUES
('1.0.0', '20250725'),
('1.0.1', '20250726'),
('1.1.0', '20250801'),
('2.0.0', '20250901'),
('2.1.0', '20251001');


INSERT INTO day_in_attendance_book (attendance_book_id, attendance_day, reward_id, reward_type_cd, reward_qty)
VALUES
    (1, 1, 1, '01', 100),  -- 1일차: 골드 100
    (1, 2, 1, '01', 200),  -- 2일차: 골드 200
    (1, 3, 103, '02', 1),    -- 3일차: 아이템 1개
    (1, 4, 1, '01', 300),  -- 4일차: 골드 300
    (1, 5, 105, '02', 2),    -- 5일차: 아이템 2개
    (1, 6, 1, '01', 400),  -- 6일차: 골드 400
    (1, 7, 1, '01', 500),  -- 7일차: 골드 500
    (1, 8, 108, '02', 3),    -- 8일차: 아이템 3개
    (1, 9, 1, '01', 600),  -- 9일차: 골드 600
    (1, 10, 110, '02', 4),   -- 10일차: 아이템 4개
    (1, 11, 1, '01', 700), -- 11일차: 골드 700
    (1, 12, 1, '01', 800), -- 12일차: 골드 800
    (1, 13, 113, '02', 5),   -- 13일차: 아이템 5개
    (1, 14, 1, '01', 900), -- 14일차: 골드 900
    (1, 15, 115, '02', 6),   -- 15일차: 아이템 6개
    (1, 16, 1, '01', 1000),-- 16일차: 골드 1000
    (1, 17, 1, '01', 1100),-- 17일차: 골드 1100
    (1, 18, 118, '02', 7),   -- 18일차: 아이템 7개
    (1, 19, 1, '01', 1200),-- 19일차: 골드 1200
    (1, 20, 120, '02', 8),   -- 20일차: 아이템 8개
    (1, 21, 1, '01', 1300),-- 21일차: 골드 1300
    (1, 22, 1, '01', 1400),-- 22일차: 골드 1400
    (1, 23, 123, '02', 9),   -- 23일차: 아이템 9개
    (1, 24, 1, '01', 1500),-- 24일차: 골드 1500
    (1, 25, 125, '02', 10),  -- 25일차: 아이템 10개
    (1, 26, 1, '01', 1600),-- 26일차: 골드 1600
    (1, 27, 1, '01', 1700),-- 27일차: 골드 1700
    (1, 28, 128, '02', 11),  -- 28일차: 아이템 11개
    (1, 29, 1, '01', 1800),-- 29일차: 골드 1800
    (1, 30, 130, '02', 12);  -- 30일차: 아이템 12개


INSERT INTO attendance_book (attendance_book_id, start_dt, end_dt, create_dt) VALUES
(1, '2001-07-01 00:00:00', '2099-07-31 23:59:59', '2001-06-30 12:00:00'),
(2, '2025-08-01 00:00:00', '2025-08-31 23:59:59', '2025-07-31 12:00:00'),
(3, '2025-09-01 00:00:00', '2025-09-30 23:59:59', '2025-08-31 12:00:00'),
(4, '2025-10-01 00:00:00', '2025-10-31 23:59:59', '2025-09-30 12:00:00'),
(5, '2025-11-01 00:00:00', '2025-11-30 23:59:59', '2025-10-31 12:00:00');


INSERT INTO currency (currency_id, currency_name, description,premium_yn, create_dt) VALUES
(1, 'Gold','Gold','N', '2025-07-01 00:00:00'),
(2, 'Gem','dsd' ,'Y', '2025-07-01 00:00:00'),
(3, 'Energy', 'ds','N', '2025-07-01 00:00:00'),
(4, 'Token','asd' ,'Y', '2025-07-01 00:00:00'),
(5, 'Honor','asd','N', '2025-07-01 00:00:00');


INSERT INTO item (item_id, item_name,item_description ,item_type_cd, rarity_cd, create_dt) VALUES
(1, 'Sword of Valor','asd' ,'WP', 'H', '2025-07-01 00:00:00'),
(2, 'Shield of Honor','asd' ,'AR', 'U', '2025-07-01 00:00:00'),
(3, 'Potion of Healing','asd' ,'IT', 'C', '2025-07-01 00:00:00'),
(4, 'Ring of Power','asdsa' ,'AC', 'H', '2025-07-01 00:00:00'),
(5, 'Helmet of Wisdom', 'adsa','AR', 'R', '2025-07-01 00:00:00');