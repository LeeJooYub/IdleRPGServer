# IdleRPG Game API Server

모바일 Idle RPG 게임의 백엔드 API 서버입니다.

## 기술 스택

- **.NET 9.0** - ASP.NET Core Web API
- **MySQL** - 게임 데이터 및 마스터 데이터 저장
- **Redis** - 세션 관리 및 캐싱
- **ZLogger** - 구조화된 로깅

## 아키텍처

### 3-Tier 아키텍처
```
Controllers → Services → Repository
```

### 데이터베이스 구조
- **GameDB** - 유저 진행 데이터 (게임 상태, 인벤토리, 메일 등)
- **MasterDB** - 게임 마스터 데이터 (스테이지, 아이템, 미션 정보)
- **Redis** - 인증 토큰 및 세션 캐시

## 주요 기능

### 🔐 인증 시스템
- 토큰 기반 인증
- Hive 플랫폼 연동
- Redis 기반 세션 관리

### 🎮 게임 시스템
- **스테이지** - 스테이지 클리어 및 보상
- **미션** - 가이드 미션 진행
- **출석체크** - 일일 출석 보상
- **성장** - 캐릭터 능력치 업그레이드

### 💰 경제 시스템
- 다중 화폐 관리 (골드, 프리미엄)
- 인벤토리 시스템
- 메일 보상 시스템

### 🛡️ 보안
- 앱 버전 검증
- 마스터 데이터 버전 체크
- Redis 락을 통한 동시성 제어

## API 엔드포인트

| 컨트롤러 | 기능 | 주요 액션 |
|---------|------|-----------|
| **Auth** | 인증 | 로그인, 로그아웃 |
| **User** | 유저 관리 | 데이터 로드 |
| **Stage** | 스테이지 | 클리어, 보상 수령 |
| **Mission** | 미션 | 가이드 미션 완료 |
| **Mail** | 메일 | 조회, 보상 수령 |
| **Attendance** | 출석 | 출석체크 |
| **Growth** | 성장 | 능력치 업그레이드 |

## 설정

### appsettings.json
```json
{
  "ServerAddress": "http://localhost:11500",
  "HiveServerAddress": "http://localhost:11501",
  "logdir": "./log/",
  "DbConfig": {
    "Redis": "localhost",
    "GameDb": "Server=localhost;Port=3306;...",
    "MasterDb": "Server=localhost;Port=3306;..."
  }
}
```

## 시작하기

### 필수 요구사항
- .NET 9.0 SDK
- MySQL Server
- Redis Server

### 실행 순서
1. 데이터베이스 설정
2. 마스터 데이터 로드
3. API 서버 시작

```bash
dotnet run
```

### 미들웨어 파이프라인
1. **VersionCheckMiddleware** - 앱/마스터 데이터 버전 검증
2. **CheckUserAuthMiddleware** - 사용자 인증
3. **LockRedisMiddleware** - Redis 락 관리

## 데이터베이스 스키마

### GameDB 주요 테이블
- `user` - 사용자 기본 정보
- `user_character_status` - 캐릭터 스탯
- `user_character_progress` - 진행 상황
- `user_inventory` - 인벤토리
- `user_mail` - 메일 시스템
- `user_attendance` - 출석 정보
- `user_currency` - 화폐 정보

### MasterDB 주요 테이블
- `version` - 버전 정보
- `stage` - 스테이지 데이터
- `item` - 아이템 정보
- `guide_mission` - 가이드 미션
- `attendance_book` - 출석 보상
- `ability` - 능력치 데이터

## 개발 가이드

### 의존성 주입
서비스는 `Program.cs`에서 등록:
```csharp
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddSingleton<IMemoryDb, MemoryDb>();
```

### 로깅
ZLogger를 사용한 구조화된 로깅:
- 콘솔 출력 (개발 환경)
- 파일 저장 (일별 로테이션)

### 에러 코드
`ErrorCode.cs`에서 게임별 에러 코드 정의

## 패키지 의존성

- **CloudStructures** (3.3.0) - Redis 클라이언트
- **MySqlConnector** (2.3.7) - MySQL 드라이버
- **SqlKata** (2.4.0) - 쿼리 빌더
- **ZLogger** (2.4.1) - 고성능 로깅

## 보안 고려사항

- 사용자 인증은 Redis 토큰 기반
- 동시성 제어를 위한 Redis 락
- 버전 불일치 시 접근 차단
- 패스워드 정보는 설정 파일에서 분리 필요

---

*이 문서는 Idle RPG Game API Server의 기술 개요를 제공합니다.*
