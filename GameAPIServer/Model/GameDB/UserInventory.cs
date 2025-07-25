using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPIServer.Models.GameDB
{
    public class UserInventory
    {
        public Int64 account_id { get; set; } // 사용자 계정 ID
        public int item_id { get; set; } // 아이템 ID
        public int quantity { get; set; } = 0; // 아이템 수량
        public DateTime last_updated { get; set; } = DateTime.UtcNow; // 마지막 업데이트 시간
        // 추가적인 필드가 필요할 경우 여기에 정의
    }
}