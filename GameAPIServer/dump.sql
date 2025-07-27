use game_db;

CREATE TABLE user (
    account_id BIGINT PRIMARY KEY AUTO_INCREMENT,
    platform_id BIGINT NOT NULL,
    platform_name VARCHAR(255) NOT NULL DEFAULT 'hive',
    nickname VARCHAR(255) NOT NULL DEFAULT 'default_nickname',
    created_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

use game_db;

DROP TABLE IF EXISTS mail;
CREATE TABLE mail (
    mail_id BIGINT PRIMARY KEY AUTO_INCREMENT,
    account_id BIGINT NOT NULL,
    mail_type VARCHAR(50) NOT NULL DEFAULT 'normal',
    sender VARCHAR(100) NOT NULL DEFAULT 'System',
    receive_condition VARCHAR(50) NOT NULL DEFAULT 'none',
    subject VARCHAR(255) NOT NULL DEFAULT 'No Subject',
    content VARCHAR(150) NOT NULL DEFAULT 'No Content',
    create_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    expire_dt DATETIME NULL,
    receive_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    is_read BOOLEAN NOT NULL DEFAULT FALSE,
    is_claimed BOOLEAN NOT NULL DEFAULT FALSE,

    reward1_id INT NULL,
    reward1_type VARCHAR(50) NULL,
    reward1_qty INT NULL,

    reward2_id INT NULL,
    reward2_type VARCHAR(50) NULL,
    reward2_qty INT NULL,

    reward3_id INT NULL,
    reward3_type VARCHAR(50) NULL,
    reward3_qty INT NULL,

    reward4_id INT NULL,
    reward4_type VARCHAR(50) NULL,
    reward4_qty INT NULL,

    reward5_id INT NULL,
    reward5_type VARCHAR(50) NULL,
    reward5_qty INT NULL,

    reward6_id INT NULL,
    reward6_type VARCHAR(50) NULL,
    reward6_qty INT NULL,

    reward7_id INT NULL,
    reward7_type VARCHAR(50) NULL,
    reward7_qty INT NULL,

    reward8_id INT NULL,
    reward8_type VARCHAR(50) NULL,
    reward8_qty INT NULL
);

INSERT INTO mail (
    mail_type, sender, receive_condition, account_id, subject, content, create_dt, expire_dt, receive_dt, is_read, is_claimed,
    reward1_id, reward1_type, reward1_qty,
    reward2_id, reward2_type, reward2_qty,
    reward3_id, reward3_type, reward3_qty
) VALUES
('normal', 'System', 'none', 1, 'Welcome!', 'Enjoy your adventure!', NOW(), NULL, NOW(), FALSE, FALSE, 101, 'gold', 500, 201, 'item', 1, NULL, NULL, NULL),
('important', 'Admin', 'none', 1, 'Daily Reward', 'Here is your daily login reward.', NOW(), NULL, NOW(), FALSE, FALSE, 102, 'gem', 10, NULL, NULL, NULL, NULL, NULL, NULL),
('normal', 'System', 'advertise', 1, 'Ad Reward', 'Watch an ad to claim.', NOW(), NULL, NOW(), FALSE, FALSE, 103, 'gold', 100, 202, 'item', 2, NULL, NULL, NULL),
('normal', 'System', 'none', 1, 'Quest Complete', 'Quest reward inside.', NOW(), NULL, NOW(), FALSE, FALSE, 104, 'gold', 300, 203, 'item', 1, NULL, NULL, NULL),
('important', 'Admin', 'none', 1, 'Event Reward', 'Special event reward!', NOW(), NULL, NOW(), FALSE, FALSE, 105, 'gem', 20, 204, 'item', 3, NULL, NULL, NULL),
('normal', 'System', 'none', 1, 'Level Up!', 'Congrats on leveling up!', NOW(), NULL, NOW(), FALSE, FALSE, 106, 'gold', 200, NULL, NULL, NULL, NULL, NULL, NULL),
('normal', 'System', 'none', 1, 'Friend Gift', 'A friend sent you a gift.', NOW(), NULL, NOW(), FALSE, FALSE, 107, 'item', 5, NULL, NULL, NULL, NULL, NULL, NULL),
('important', 'Admin', 'none', 1, 'Maintenance Compensation', 'Sorry for the inconvenience.', NOW(), NULL, NOW(), FALSE, FALSE, 108, 'gem', 15, 205, 'item', 2, NULL, NULL, NULL),
('normal', 'System', 'none', 1, 'Anniversary', 'Happy anniversary!', NOW(), NULL, NOW(), FALSE, FALSE, 109, 'gold', 1000, NULL, NULL, NULL, NULL, NULL, NULL),
('normal', 'System', 'none', 1, 'Special Offer', 'Limited time offer!', NOW(), NULL, NOW(), FALSE, FALSE, 110, 'item', 10, NULL, NULL, NULL, NULL, NULL, NULL),
('important', 'Admin', 'none', 1, 'Bug Fix Compensation', 'Thank you for your patience.', NOW(), NULL, NOW(), FALSE, FALSE, 111, 'gem', 5, NULL, NULL, NULL, NULL, NULL, NULL),
('normal', 'System', 'none', 1, 'Weekly Bonus', 'Here is your weekly bonus.', NOW(), NULL, NOW(), FALSE, FALSE, 112, 'gold', 700, 206, 'item', 1, NULL, NULL, NULL);


DROP TABLE IF EXISTS user_currency;
CREATE TABLE user_currency (
    account_id BIGINT PRIMARY KEY, -- 사용자 계정 ID (기본 키)
    gold INT NOT NULL DEFAULT 0, -- 골드 수량
    gem INT NOT NULL DEFAULT 0, -- 젬 수량
    last_updated DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- 마지막 업데이트 시간
);

-- user 테이블 임시 데이터
INSERT INTO user (platform_id, platform_name, nickname) VALUES
(1, 'testuser@example.com"', 'testpassword'),
(2, 'hive', 'user2'),
(3, 'hive', 'user3'),
(4, 'hive', 'user4'),
(5, 'hive', 'user5');

-- user_currency 테이블 임시 데이터
INSERT INTO user_currency (account_id, gold, gem) VALUES
(1, 1000, 10),
(2, 2000, 20),
(3, 3000, 30),
(4, 4000, 40),
(5, 5000, 50);