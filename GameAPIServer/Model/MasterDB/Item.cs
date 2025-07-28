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
        public string item_icon { get; set; } // 아이템 아이콘
        public string item_type { get; set; } // 아이템 종류 (예: consumable, valuable, ingredient, box 등)
        public DateTime create_dt { get; set; } = DateTime.UtcNow; // 생성일시
    }



    // public class consumbable_item
    // {
    //     public int item_id { get; set; }
    //     public string item_name { get; set; }
    //     public string item_description { get; set; }
    //     public string item_icon { get; set; }
    //     public int effect_value { get; set; } // 효과 값 (예: HP 회복량)
    //     public DateTime create_dt { get; set; } // 생성일시
    // }

    // public class valuable_item
    // {
    //     public int item_id { get; set; }
    //     public string item_name { get; set; }
    //     public string item_description { get; set; }
    //     public string item_icon { get; set; }
    //     public int effect_value { get; set; } // 효과 값 (예: HP 회복량)
    //     public DateTime create_dt { get; set; } // 생성일시
    // }

    // public class ingredient_item
    // {
    //     public int item_id { get; set; }
    //     public string item_name { get; set; }
    //     public string item_description { get; set; }
    //     public string item_icon { get; set; }
    //     public DateTime create_dt { get; set; } // 생성일시
    // }

    public class box
    {
        public int box_id { get; set; }
        public string box_name { get; set; }
        public string box_description { get; set; }
        public string box_icon { get; set; }
        public DateTime create_dt { get; set; } = DateTime.UtcNow; // 생성일시
    }

}