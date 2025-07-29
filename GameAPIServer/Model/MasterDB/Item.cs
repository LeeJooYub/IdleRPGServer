using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPIServer.Models.MasterDB
{
    public class Item
    {
        public int item_id { get; set; } // 아이템 ID
        public string item_name { get; set; } // 아이템 이름
        public string item_description { get; set; } // 아이템 설명
        public string item_type_cd { get; set; } // 아이템 종류 코드 (예: '01': consumable, '02': valuable, '03': ingredient, '04': box)
        public DateTime create_dt { get; set; } = DateTime.UtcNow; // 생성일시
    }

}