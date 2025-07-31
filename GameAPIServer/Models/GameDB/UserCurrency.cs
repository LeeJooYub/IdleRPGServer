using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;

namespace GameAPIServer.Models.GameDB;
public class UserCurrency
{
    public Int64 player_uid { get; set; } // 사용자 계정 ID
    public int currency_id { get; set; } // 화폐 ID 0~10은 일반 화폐, 11~20은 강화재료
    public int amount { get; set; } = 0; // 화폐 수량
    public DateTime last_update_dt { get; set; } = DateTime.UtcNow; // 마지막 업데이트 시간
}
