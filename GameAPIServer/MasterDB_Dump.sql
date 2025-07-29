use master_db;

-- Attendance 테이블
DROP TABLE IF EXISTS Attendance;
CREATE TABLE Attendance (
    attendance_id BIGINT NOT NULL PRIMARY KEY, -- 출석 ID
    start_dt DATETIME NOT NULL, -- 출석 시작 날짜
    end_dt DATETIME NOT NULL, -- 출석 종료 날짜
    create_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP -- 생성일시
);

-- DayInAttendance 테이블
DROP TABLE IF EXISTS DayInAttendance;
CREATE TABLE DayInAttendance (
    attendance_id BIGINT NOT NULL, -- 출석 ID
    attendance_day INT NOT NULL, -- 출석 일수 (1부터 시작)
    reward_id INT NOT NULL, -- 보상 ID
    reward_type_cd CHAR(2) NOT NULL, -- 보상 타입 (예: "GD": gold, "IT": item)
    reward_qty INT NOT NULL, -- 보상 수량
    PRIMARY KEY (attendance_id, attendance_day)
);

DROP TABLE IF EXISTS Currency;
CREATE TABLE Currency (
    currency_id INT NOT NULL PRIMARY KEY, -- 화폐 ID
    currency_name VARCHAR(50) NOT NULL, -- 화폐 이름
    currency_type_cd CHAR(2) NOT NULL, -- 화폐 타입 코드 (예: "GD": gold, "GM": gem)
    is_premium TINYINT(1) NOT NULL DEFAULT 0, -- 프리미엄 여부 (0: 일반, 1: 프리미엄)
    create_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP -- 생성일시
);


DROP TABLE IF EXISTS Item;
CREATE TABLE Item (
    item_id INT NOT NULL PRIMARY KEY, -- 아이템 ID
    item_name VARCHAR(100) NOT NULL, -- 아이템 이름
    item_type_cd CHAR(2) NOT NULL, -- 아이템 타입 코드 (예: "WP": 무기, "AR": 방어구)
    rarity INT NOT NULL DEFAULT 1, -- 아이템 희귀도 (1~5)
    create_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP -- 생성일시
);

INSERT INTO DayInAttendance (attendance_id, attendance_day, reward_id, reward_type_cd, reward_qty) VALUES
(1, 1, 101, 'GD', 100),
(1, 2, 102, 'IT', 1),
(1, 3, 103, 'GD', 200),
(1, 4, 104, 'IT', 2),
(1, 5, 105, 'GD', 300);


INSERT INTO Attendance (attendance_id, start_dt, end_dt, create_dt) VALUES
(1, '2025-07-01 00:00:00', '2025-07-31 23:59:59', '2025-06-30 12:00:00'),
(2, '2025-08-01 00:00:00', '2025-08-31 23:59:59', '2025-07-31 12:00:00'),
(3, '2025-09-01 00:00:00', '2025-09-30 23:59:59', '2025-08-31 12:00:00'),
(4, '2025-10-01 00:00:00', '2025-10-31 23:59:59', '2025-09-30 12:00:00'),
(5, '2025-11-01 00:00:00', '2025-11-30 23:59:59', '2025-10-31 12:00:00');


INSERT INTO Currency (currency_id, currency_name, currency_type_cd, is_premium, create_dt) VALUES
(1, 'Gold', 'GD', 0, '2025-07-01 00:00:00'),
(2, 'Gem', 'GM', 1, '2025-07-01 00:00:00'),
(3, 'Energy', 'EN', 0, '2025-07-01 00:00:00'),
(4, 'Token', 'TK', 1, '2025-07-01 00:00:00'),
(5, 'Honor', 'HN', 0, '2025-07-01 00:00:00');


INSERT INTO Item (item_id, item_name, item_type_cd, rarity, create_dt) VALUES
(1, 'Sword of Valor', 'WP', 5, '2025-07-01 00:00:00'),
(2, 'Shield of Honor', 'AR', 4, '2025-07-01 00:00:00'),
(3, 'Potion of Healing', 'IT', 1, '2025-07-01 00:00:00'),
(4, 'Ring of Power', 'AC', 3, '2025-07-01 00:00:00'),
(5, 'Helmet of Wisdom', 'AR', 2, '2025-07-01 00:00:00');