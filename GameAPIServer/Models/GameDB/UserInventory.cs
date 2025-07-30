using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPIServer.Models.GameDB
{
    public class UserInventoryItem
    {
        public Int64 account_uid { get; set; } // 사용자 계정 ID
        public int item_id { get; set; } // 아이템 ID
        public int item_qty { get; set; } = 0; // 아이템 수량
        public DateTime acquire_dt { get; set; } = DateTime.UtcNow; // 아이템 획득 시간
        public DateTime last_update_dt { get; set; } = DateTime.UtcNow; // 마지막 업데이트 시간
    }
}