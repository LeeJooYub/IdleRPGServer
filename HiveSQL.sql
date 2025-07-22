use hive;

CREATE TABLE account_info (
    player_id   BIGINT PRIMARY KEY AUTO_INCREMENT,
    email       VARCHAR(100) NOT NULL,
    pw          VARCHAR(100) NOT NULL,
    salt_value  VARCHAR(100) NOT NULL,
    create_dt   DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);