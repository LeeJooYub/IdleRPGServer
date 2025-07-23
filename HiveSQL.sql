use hive;

-- CREATE TABLE account_info (
--     player_id   BIGINT PRIMARY KEY AUTO_INCREMENT,
--     email       VARCHAR(100) NOT NULL,
--     pw          VARCHAR(100) NOT NULL,
--     salt_value  VARCHAR(100) NOT NULL,
--     create_dt   DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
-- );


CREATE TABLE category (
    id INT AUTO_INCREMENT PRIMARY KEY,
    slug VARCHAR(100) NOT NULL,
    title VARCHAR(200) NOT NULL,
    description TEXT,
    image VARCHAR(255),
    sortid INT DEFAULT 0,
    display INT DEFAULT 1,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    deleted_at DATETIME NULL
);