use master_db;

use master_db;

CREATE TABLE version (
    app_version VARCHAR(50) NOT NULL PRIMARY KEY, -- 애플리케이션 버전
    master_data_version VARCHAR(50) NOT NULL -- 마스터 데이터 버전
);