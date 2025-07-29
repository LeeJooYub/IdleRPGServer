using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPIServer.Models.MasterDB
{
    public class RewardData
    {
        public int? reward_id { get; set; }
        public string? reward_type_cd { get; set; } //  e.g., 01: "gold", 02: "item"
        public int? reward_qty { get; set; }
    }
}