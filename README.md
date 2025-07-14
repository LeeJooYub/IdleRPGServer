# IdleRPGServer

C#과 .NET을 활용하여 개발한 방치형 액션 RPG 서버 모작 프로젝트입니다.  
'소울 스트라이크'의 전투 및 성장 시스템을 참고하여, 개인 학습 목적으로 제작하였습니다.

---

## 🛠️ 사용 기술

- **언어**: C#
- **프레임워크**: ASP.NET Core Web API
- **DB 연동**: Entity Framework Core, SQLite (또는 원하는 DB로 교체 가능)
- **기타**: RESTful API 설계, DI(의존성 주입), 상태 관리 등

---

## 🚀 실행 방법

1. 이 저장소를 클론합니다.
   ```bash
   git clone https://github.com/your-idle-rpg-server.git
   cd your-idle-rpg-server
2. .NET SDK 7.0 이상이 설치되어 있는지 확인합니다.

3. 개발용 DB 마이그레이션 (선택)
dotnet ef database update

4. 서버 실행
dotnet run

---

## 📌 구현 기능
기능 항목	설명
회원 가입 / 로그인	JWT 기반 인증 및 사용자 계정 생성
전투 시스템	일정 시간 간격으로 자동 전투 시뮬레이션
스테이지 진행	스테이지 별 몬스터 등장 및 보상 처리
장비 수집 및 강화	랜덤 드랍 장비, 강화 시스템 구현
성장 / 보상 시스템	경험치, 골드 획득 및 캐릭터 레벨업

---

## 📂 디렉토리 구조
IdleRPG-Server/
├── Controllers/       # API 엔드포인트
├── Services/          # 비즈니스 로직 처리
├── Models/            # 데이터 모델 정의
├── Data/              # DB 컨텍스트 및 마이그레이션
├── DTOs/              # 요청/응답 데이터 구조
├── Program.cs         # 서버 실행 엔트리포인트
├── appsettings.json   # 설정 파일

---

## 📎 참고 사항
이 프로젝트는 상업적 목적이 아닌 학습을 위한 모작입니다.

실제 상용 게임과는 구조나 로직이 다를 수 있으며, 일부 기능은 축약 또는 단순화되어 있습니다.
자유롭게 참고하실 수 있지만, PR은 받고있지 않습니다.

---