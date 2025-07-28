using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPIServer.Models.GameDB
{
    public class UserInventory
    {
        public Int64 account_uid { get; set; } // 사용자 계정 ID
        public int item_id { get; set; } // 아이템 ID
        public string item_name { get; set; } // 아이템 이름
        public string item_description { get; set; } // 아이템 설명
        public int item_quantity { get; set; } = 0; // 아이템 수량
        public string item_icon { get; set; } // 아이템 아이콘
        public string item_type { get; set; } // 아이템 종류 (예: consumable, valuable, ingredient, box 등)
        public DateTime create_dt { get; set; } = DateTime.UtcNow; //
        public DateTime last_updated { get; set; } = DateTime.UtcNow; // 마지막 업데이트 시간
        // 추가적인 필드가 필요할 경우 여기에 정의
    }
}