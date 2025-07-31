using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPIServer.Models.GameDB;

public class UserCharacterStatus
{
    public Int64 player_uid { get; set; } // 사용자 계정 ID
    public string character_name { get; set; } // 캐릭터 이름
    public int level { get; set; } = 1; // 캐릭터 레벨
    public int character_hp { get; set; } = 100; // 캐릭터 체력
    public int character_mp { get; set; } = 50; // 캐릭터 마나
    public int character_atk { get; set; } = 10; // 캐릭터 공격력
    public int character_def { get; set; } = 5; // 캐릭터 방어력
    public string character_job_cd { get; set; } = "01"; // 캐릭터 직업 코드 (예: "01": Warrior, "02": Mage, "03": Archer); 
}


