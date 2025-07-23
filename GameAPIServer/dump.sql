use game_db;

CREATE TABLE user (
    account_id BIGINT PRIMARY KEY AUTO_INCREMENT,
    platform_id BIGINT NOT NULL,
    platform_name VARCHAR(255) NOT NULL DEFAULT 'hive',
    nickname VARCHAR(255) NOT NULL DEFAULT 'default_nickname',
    created_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);