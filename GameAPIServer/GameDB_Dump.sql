use game_db;

DROP TABLE IF EXISTS user;
-- User 테이블 (예시)
CREATE TABLE user (
    account_uid BIGINT PRIMARY KEY,
    create_dt DATETIME NOT NULL,
    last_login_dt DATETIME NOT NULL
);

DROP TABLE IF EXISTS user_attendance;
-- Attendance 테이블
CREATE TABLE user_attendance (
    account_uid BIGINT NOT NULL,
    attendance_book_id BIGINT NOT NULL,
    last_attendance_dt DATETIME NOT NULL,
    attendance_continue_cnt INT NOT NULL,
    PRIMARY KEY (account_uid, attendance_book_id)
);

DROP TABLE IF EXISTS user_currency;
-- UserCurrency 테이블
CREATE TABLE user_currency (
    account_uid BIGINT NOT NULL,
    currency_id INT NOT NULL,
    amount INT NOT NULL DEFAULT 0,
    last_update_dt DATETIME NOT NULL,
    PRIMARY KEY (account_uid, currency_id)
);


-- UserCharacter 테이블
DROP TABLE IF EXISTS user_character;
CREATE TABLE user_character (
    account_uid BIGINT NOT NULL, -- 사용자 계정 ID
    character_name VARCHAR(50) NOT NULL, -- 캐릭터 이름
    level INT NOT NULL DEFAULT 1, -- 캐릭터 레벨
    create_dt DATETIME NOT NULL, -- 캐릭터 생성 날짜
    last_login_dt DATETIME NOT NULL, -- 마지막 로그인 시간
    character_hp INT NOT NULL DEFAULT 100, -- 캐릭터 체력
    character_mp INT NOT NULL DEFAULT 50, -- 캐릭터 마나
    character_atk INT NOT NULL DEFAULT 10, -- 캐릭터 공격력
    character_def INT NOT NULL DEFAULT 5, -- 캐릭터 방어력
    character_job_cd VARCHAR(2) NOT NULL DEFAULT '01', -- 캐릭터 직업 코드 (예: '01': Warrior, '02': Mage, '03': Archer)
    PRIMARY KEY (account_uid, character_name)
);

DROP TABLE IF EXISTS user_inventory;
-- UserInventory 테이블
CREATE TABLE user_inventory (
    account_uid BIGINT NOT NULL,
    item_id INT NOT NULL,
    item_qty INT NOT NULL DEFAULT 0,
    acquire_dt DATETIME NOT NULL,
    last_update_dt DATETIME NOT NULL,
    PRIMARY KEY (account_uid, item_id)
);

-- UserMail 테이블
DROP TABLE IF EXISTS mail;
CREATE TABLE mail (
    mail_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    account_uid BIGINT NOT NULL,
    mail_type_cd VARCHAR(2) NOT NULL DEFAULT 'N', -- e.g., 'N': normal, 'I': important
    sender_cd VARCHAR(2) NOT NULL DEFAULT '01',   -- e.g., '01': System, '02': Admin, '03': Event
    title VARCHAR(100) NOT NULL,
    content TEXT,
    create_dt DATETIME NOT NULL,
    expire_dt DATETIME NULL,
    receive_dt DATETIME NULL,
    receive_yn CHAR(1) NOT NULL DEFAULT 'N',      -- 'Y' or 'N'
    reward_id INT NULL,
    reward_type_cd VARCHAR(2) NULL,               -- e.g., '01': gold, '02': item
    reward_qty INT NULL
);

INSERT INTO mail (account_uid, mail_type_cd, sender_cd, title, content, create_dt, expire_dt, receive_dt, receive_yn, reward_id, reward_type_cd, reward_qty) VALUES
(10001, 'N', '01', 'Welcome!', 'Welcome to the game!', '2025-07-29 09:00:00', NULL, NULL, 'N', 1, '01', 100),
(10001, 'N', '01', 'Daily Reward', 'Here is your daily reward.', '2025-07-29 09:05:00', NULL, '2025-07-29 09:10:00', 'Y', 2, '02', 10),
(10002, 'I', '02', 'Event Notice', 'Check out the new event!', '2025-07-28 08:30:00', NULL, NULL, 'N', 3, '02', 1),
(10003, 'N', '01', 'Maintenance', 'Server maintenance completed.', '2025-07-27 10:15:00', NULL, '2025-07-27 10:20:00', 'Y', 4, '01', 200),
(10004, 'N', '01', 'Gift', 'You received a special gift!', '2025-07-29 11:20:00', NULL, NULL, 'N', 5, '02', 2),
(10001, 'I', '03', 'Event Reward', 'Congrats on the event!', '2025-07-29 12:00:00', NULL, NULL, 'N', 6, '01', 5),
(10002, 'N', '01', 'Login Bonus', 'Thanks for logging in!', '2025-07-29 08:00:00', NULL, NULL, 'N', 7, '01', 50),
(10003, 'N', '01', 'Quest Clear', 'Quest completed!', '2025-07-28 15:00:00', NULL, NULL, 'N', 8, '02', 3),
(10004, 'I', '02', 'Special Offer', 'Check out this offer!', '2025-07-28 18:00:00', NULL, NULL, 'N', 9, '01', 20),
(10005, 'N', '01', 'Compensation', 'Sorry for the inconvenience.', '2025-07-27 20:00:00', NULL, NULL, 'N', 10, '01', 300);

INSERT INTO user (account_uid, create_dt, last_login_dt) VALUES
(10001, '2025-07-01 10:00:00', '2025-07-29 09:00:00'),
(10002, '2025-07-10 12:00:00', '2025-07-28 08:30:00'),
(10003, '2025-07-15 14:00:00', '2025-07-27 10:15:00'),
(10004, '2025-07-20 16:00:00', '2025-07-29 11:20:00'),
(10005, '2025-07-25 18:00:00', '2025-07-26 07:45:00');

-- 예시 데이터: Attendance
INSERT INTO user_attendance (account_uid, attendance_book_id, last_attendance_dt, attendance_continue_cnt) VALUES
(10001, 1, '2025-07-29 09:00:00', 3),
(10002, 1, '2025-07-28 08:30:00', 1),
(10003, 2, '2025-07-27 10:15:00', 5),
(10004, 1, '2025-07-29 11:20:00', 2),
(10005, 3, '2025-07-26 07:45:00', 7);

-- 예시 데이터: UserCurrency
INSERT INTO user_currency (account_uid, currency_id, amount, last_update_dt) VALUES
(10001, 1, 1000, '2025-07-29 09:00:00'),
(10001, 2, 50, '2025-07-29 09:00:00'),
(10002, 1, 500, '2025-07-28 08:30:00'),
(10003, 1, 200, '2025-07-27 10:15:00'),
(10004, 3, 10, '2025-07-29 11:20:00');

-- 예시 데이터: UserCharacter
INSERT INTO user_character (account_uid, character_name, level, create_dt, last_login_dt, character_hp, character_mp, character_atk, character_def, character_job_cd) VALUES
(10001, 'Arthur', 10, '2025-07-01 10:00:00', '2025-07-29 09:00:00', 150, 80, 25, 12, '01'),
(10002, 'Merlin', 8, '2025-07-10 12:00:00', '2025-07-28 08:30:00', 120, 150, 15, 8, '02'),
(10003, 'Lancelot', 12, '2025-07-15 14:00:00', '2025-07-27 10:15:00', 180, 60, 30, 15, '01'),
(10004, 'Guinevere', 7, '2025-07-20 16:00:00', '2025-07-29 11:20:00', 110, 90, 18, 10, '03'),
(10005, 'Morgana', 9, '2025-07-25 18:00:00', '2025-07-26 07:45:00', 130, 140, 22, 9, '02');

-- 예시 데이터: UserInventory
INSERT INTO user_inventory (account_uid, item_id, item_qty, acquire_dt, last_update_dt) VALUES
(10001, 101, 3, '2025-07-25 09:00:00', '2025-07-29 09:00:00'),
(10001, 102, 1, '2025-07-26 09:00:00', '2025-07-29 09:00:00'),
(10002, 101, 2, '2025-07-28 08:30:00', '2025-07-28 08:30:00'),
(10003, 103, 5, '2025-07-27 10:15:00', '2025-07-27 10:15:00'),
(10004, 104, 10, '2025-07-29 11:20:00', '2025-07-29 11:20:00');