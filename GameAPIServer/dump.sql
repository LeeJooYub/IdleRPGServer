use game_db;

CREATE TABLE user (
    account_id BIGINT PRIMARY KEY AUTO_INCREMENT,
    platform_id BIGINT NOT NULL,
    platform_name VARCHAR(255) NOT NULL DEFAULT 'hive',
    nickname VARCHAR(255) NOT NULL DEFAULT 'default_nickname',
    created_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE mail (
    mail_id BIGINT PRIMARY KEY AUTO_INCREMENT, -- 메일 ID (자동 증가)
    mail_type VARCHAR(50) NOT NULL DEFAULT 'normal', -- 메일 타입 (예: normal, important)
    sender VARCHAR(100) NOT NULL DEFAULT 'System', -- 발신자
    receive_condition VARCHAR(50) NOT NULL DEFAULT 'none', -- 메일 조건 (예: none, advertise)
    account_id BIGINT NOT NULL, -- 계정 ID
    subject VARCHAR(255) NOT NULL DEFAULT 'No Subject', -- 메일 제목
    content TEXT NOT NULL DEFAULT 'No Content', -- 메일 내용
    create_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- 생성 날짜
    expire_dt DATETIME NULL, -- 만료 날짜
    receive_dt DATETIME NULL, -- 수신 날짜
    is_read BOOLEAN NOT NULL DEFAULT FALSE, -- 읽음 여부
    is_claimed BOOLEAN NOT NULL DEFAULT FALSE -- 보상 수령 여부
);

CREATE TABLE reward_in_mail (
    mail_id BIGINT NOT NULL, -- 메일 ID (mail 테이블과 연관)
    account_id BIGINT NOT NULL, -- 계정 ID
    reward_id INT NOT NULL, -- 보상 ID
    reward_qty INT NOT NULL DEFAULT 0, -- 보상 수량
    reward_type VARCHAR(50) NOT NULL DEFAULT 'normal', -- 보상 타입 (예: gold, item, etc.)
    PRIMARY KEY (mail_id, reward_id), -- mail_id와 reward_id를 복합 키로 설정
    FOREIGN KEY (mail_id) REFERENCES mail(mail_id) ON DELETE CASCADE -- mail 테이블과 외래 키 관계
);

CREATE TABLE user_currency (
    account_id BIGINT PRIMARY KEY, -- 사용자 계정 ID (기본 키)
    gold INT NOT NULL DEFAULT 0, -- 골드 수량
    gem INT NOT NULL DEFAULT 0, -- 젬 수량
    last_updated DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP -- 마지막 업데이트 시간
);