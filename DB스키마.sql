use game_db;

DROP TABLE IF EXISTS user;
-- User 테이블 (예시)
CREATE TABLE user (
    player_uid BIGINT PRIMARY KEY,
    create_dt DATETIME NOT NULL,
    last_login_dt DATETIME NOT NULL
);

DROP TABLE IF EXISTS user_attendance;
-- Attendance 테이블
CREATE TABLE user_attendance (
    player_uid BIGINT NOT NULL,
    attendance_book_id BIGINT NOT NULL,
    last_attendance_dt DATETIME NOT NULL,
    attendance_continue_cnt INT NOT NULL,
    PRIMARY KEY (player_uid, attendance_book_id)
);

DROP TABLE IF EXISTS user_currency;
-- UserCurrency 테이블
CREATE TABLE user_currency (
    player_uid BIGINT NOT NULL,
    currency_id INT NOT NULL,
    amount INT NOT NULL DEFAULT 0,
    last_update_dt DATETIME NOT NULL,
    PRIMARY KEY (player_uid, currency_id)
);


-- UserCharacterStatus 테이블
DROP TABLE IF EXISTS user_character_status;
CREATE TABLE user_character_status (
    player_uid BIGINT NOT NULL, -- 사용자 계정 ID
    character_name VARCHAR(50) NOT NULL, -- 캐릭터 이름
    level INT NOT NULL DEFAULT 1, -- 캐릭터 레벨
    character_hp INT NOT NULL DEFAULT 100, -- 캐릭터 체력
    character_mp INT NOT NULL DEFAULT 50, -- 캐릭터 마나
    character_atk INT NOT NULL DEFAULT 10, -- 캐릭터 공격력
    character_def INT NOT NULL DEFAULT 5, -- 캐릭터 방어력
    character_job_cd VARCHAR(2) NOT NULL DEFAULT '01', -- 캐릭터 직업 코드 (예: '01': Warrior, '02': Mage, '03': Archer)
    PRIMARY KEY (player_uid, character_name)
);

DROP TABLE IF EXISTS user_character_progress;
-- UserCharacterProgress 테이블
CREATE TABLE user_character_progress (
    player_uid BIGINT NOT NULL, -- 사용자 계정 ID
    current_stage_id INT NOT NULL DEFAULT 0, -- 현재 스테이지 ID
    current_guide_mission_seq INT NOT NULL DEFAULT 0, -- 현재 가이드 미션 시퀀스
    PRIMARY KEY (player_uid)
);

DROP TABLE IF EXISTS user_inventory;
-- UserInventory 테이블
CREATE TABLE user_inventory (
    player_uid BIGINT NOT NULL,
    item_id INT NOT NULL,
    item_qty INT NOT NULL DEFAULT 0,
    acquire_dt DATETIME NOT NULL,
    last_update_dt DATETIME NOT NULL,
    PRIMARY KEY (player_uid, item_id)
);

-- UserMail 테이블
DROP TABLE IF EXISTS mail;
CREATE TABLE mail (
    mail_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    player_uid BIGINT NOT NULL,
    mail_type_cd VARCHAR(2) NOT NULL DEFAULT 'N', -- e.g., 'N': normal, 'I': important
    title VARCHAR(100) NOT NULL,
    content TEXT,
    create_dt DATETIME NOT NULL,
    expire_dt DATETIME NULL,
    receive_dt DATETIME NULL,
    reward_receive_yn CHAR(1) NOT NULL DEFAULT 'N',      -- 'Y' or 'N'
    reward_id INT NULL,
    reward_type_cd VARCHAR(2) NULL,               -- e.g., '01': gold, '02': item
    reward_qty INT NULL
);


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

DROP TABLE IF EXISTS ability;
CREATE TABLE ability (
    ability_id BIGINT PRIMARY KEY AUTO_INCREMENT,         -- 능력치 ID
    name VARCHAR(50) NOT NULL,                            -- 능력치 이름
    description VARCHAR(255) NOT NULL,                    -- 능력치 설명
    required_character_level INT NOT NULL,                -- 업그레이드에 필요한 최소 레벨
    max_level INT NOT NULL,                               -- 최대 레벨
    init_cost INT NOT NULL,                               -- 초기 업그레이드 비용
    cost_increment_delta INT NOT NULL,                    -- 업그레이드 비용 증가량
    update_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP -- 수정일시
);

use hive;

DROP TABLE IF EXISTS account_info;
CREATE TABLE account_info (
    account_uid BIGINT PRIMARY KEY AUTO_INCREMENT,
    email VARCHAR(255) NOT NULL,
    pwd VARCHAR(255) NOT NULL,
    salt VARCHAR(255) NOT NULL,
    create_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

