using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPIServer.Models.GameDB
{
    public class UserCharacter
    {
        public Int64 account_id { get; set; } // 사용자 계정 ID
        public int character_id { get; set; } // 캐릭터 ID
        public string character_name { get; set; } // 캐릭터 이름
        public int level { get; set; } = 1; // 캐릭터 레벨
        public DateTime last_updated { get; set; } = DateTime.UtcNow; // 마지막 업데이트 시간
        public DateTime create_dt { get; set; } = DateTime.UtcNow; // 캐릭터 생성 날짜
        public DateTime last_login { get; set; } = DateTime.UtcNow; // 마지막 로그인 시간
        public int health { get; set; } = 100; // 캐릭터 체력
        public int mana { get; set; } = 50; // 캐릭터 마나
        public int attack { get; set; } = 10; // 캐릭터 공격력
        public int defense { get; set; } = 5; // 캐릭터 방어력
        public string character_class { get; set; } = "warrior"; // 캐릭터 클래스 (예: warrior, mage 등)
    }
}