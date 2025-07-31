using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.Models;


namespace GameAPIServer.Models.MasterDB;
    
public class Ability
{
    public Int64 ability_id { get; set; } // 능력치 ID 1: ATK, 2: DEF, 3: hp, 4: mp 등
    public string name { get; set; } // 능력치 이름
    public string description { get; set; } // 능력치 설명
    public int required_character_level { get; set; } // 업그레이드에 필요한 최소 레벨
    public int max_level { get; set; } // 최대 레벨
    public int init_cost { get; set; } // 초기 업그레이드 비용
    public int cost_increment_delta { get; set; } // 업그레이드 비용 증가량
    public DateTime update_dt { get; set; } = DateTime.UtcNow; // 수정일시
}