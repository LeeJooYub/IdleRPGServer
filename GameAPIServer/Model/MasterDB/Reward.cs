using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPIServer.Models.MasterDB
{
    public class RewardData
    {
        public int? reward_id { get; set; }
        public string? reward_type_cd { get; set; } //  reward_type_cd: "GD" = Gold, "IT" = Item)
        public int? reward_qty { get; set; }
    }
}


// reward_type_cd: "GD" = Gold, "IT" = Item)
// 보상 타입 (예: "GD" : gold, "IT" : item)