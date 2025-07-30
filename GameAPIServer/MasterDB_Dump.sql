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
DROP TABLE IF EXISTS day_in_attendance;
CREATE TABLE day_in_attendance (
    attendance_book_id BIGINT NOT NULL, -- 출석 ID
    attendance_day INT NOT NULL, -- 출석 일수 (1부터 시작)
    reward_id INT NOT NULL, -- 보상 ID
    reward_type_cd CHAR(2) NOT NULL, -- 보상 타입 (예: "01": gold, "02": item)
    reward_qty INT NOT NULL, -- 보상 수량
    PRIMARY KEY (attendance_id, attendance_day)
);

DROP TABLE IF EXISTS currency;
CREATE TABLE currency (
    currency_id INT NOT NULL PRIMARY KEY, -- 화폐 ID
    currency_name VARCHAR(50) NOT NULL, -- 화폐 이름
    'description' VARCHAR(255) NOT NULL, -- 화폐 설명
    premium_yn CHAR(1) NOT NULL DEFAULT 0, -- 프리미엄 여부 (0: 일반, 1: 프리미엄)
    create_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP -- 생성일시
);


DROP TABLE IF EXISTS item;
CREATE TABLE item (
    item_id INT NOT NULL PRIMARY KEY, -- 아이템 ID
    item_name VARCHAR(100) NOT NULL, -- 아이템 이름
    item_description VARCHAR(255) NOT NULL, -- 아이템 설명
    item_type_cd CHAR(2) NOT NULL, -- 아이템 타입 코드 (예: "WP": 무기, "AR": 방어구)
    rarity_cd INT NOT NULL, -- 아이템 희귀도 (1: 일반, 2: 희귀, 3: 영웅, 4: 전설, 5: 신화)
    create_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP -- 생성일시
);

-- version 테이블 임시 데이터
INSERT INTO version (app_version, master_data_version) VALUES
('1.0.0', '20250725'),
('1.0.1', '20250726'),
('1.1.0', '20250801'),
('2.0.0', '20250901'),
('2.1.0', '20251001');


INSERT INTO day_in_attendance (attendance_id, attendance_day, reward_id, reward_type_cd, reward_qty) VALUES
(1, 1, 101, 'GD', 100),
(1, 2, 102, 'IT', 1),
(1, 3, 103, 'GD', 200),
(1, 4, 104, 'IT', 2),
(1, 5, 105, 'GD', 300);


INSERT INTO attendance_book (attendance_id, start_dt, end_dt, create_dt) VALUES
(1, '2025-07-01 00:00:00', '2025-07-31 23:59:59', '2025-06-30 12:00:00'),
(2, '2025-08-01 00:00:00', '2025-08-31 23:59:59', '2025-07-31 12:00:00'),
(3, '2025-09-01 00:00:00', '2025-09-30 23:59:59', '2025-08-31 12:00:00'),
(4, '2025-10-01 00:00:00', '2025-10-31 23:59:59', '2025-09-30 12:00:00'),
(5, '2025-11-01 00:00:00', '2025-11-30 23:59:59', '2025-10-31 12:00:00');


INSERT INTO currency (currency_id, currency_name, 'description',premium_yn, create_dt) VALUES
(1, 'Gold','Gold','N', '2025-07-01 00:00:00'),
(2, 'Gem','dsd' ,'Y', '2025-07-01 00:00:00'),
(3, 'Energy', 'ds','N', '2025-07-01 00:00:00'),
(4, 'Token','asd' ,'Y', '2025-07-01 00:00:00'),
(5, 'Honor','asd','N', '2025-07-01 00:00:00');


INSERT INTO item (item_id, item_name,item_description ,item_type_cd, rarity_cd, create_dt) VALUES
(1, 'Sword of Valor','asd' ,'WP', 5, '2025-07-01 00:00:00'),
(2, 'Shield of Honor','asd' ,'AR', 4, '2025-07-01 00:00:00'),
(3, 'Potion of Healing','asd' ,'IT', 1, '2025-07-01 00:00:00'),
(4, 'Ring of Power','asdsa' ,'AC', 3, '2025-07-01 00:00:00'),
(5, 'Helmet of Wisdom', 'adsa','AR', 2, '2025-07-01 00:00:00');