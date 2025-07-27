use master_db;

use master_db;

CREATE TABLE version (
    app_version VARCHAR(50) NOT NULL PRIMARY KEY, -- 애플리케이션 버전
    master_data_version VARCHAR(50) NOT NULL -- 마스터 데이터 버전
);

-- version 테이블 임시 데이터
INSERT INTO version (app_version, master_data_version) VALUES
('1.0.0', '20250725'),
('1.0.1', '20250726'),
('1.1.0', '20250801'),
('2.0.0', '20250901'),
('2.1.0', '20251001');