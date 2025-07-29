use hive;

DROP TABLE IF EXISTS account_info;
CREATE TABLE account_info (
    account_uid BIGINT PRIMARY KEY AUTO_INCREMENT,
    email VARCHAR(255) NOT NULL,
    pwd VARCHAR(255) NOT NULL,
    salt VARCHAR(255) NOT NULL,
    create_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

임시 데이터 5개
INSERT INTO account_info (email, pwd, salt) VALUES
-- ('user1@email.com', 'pwd1', 'salt1'),
-- ('user2@email.com', 'pwd2', 'salt2'),
-- ('user3@email.com', 'pwd3', 'salt3'),
-- ('user4@email.com', 'pwd4', 'salt4'),
-- ('user5@email.com', 'pwd5', 'salt5');

