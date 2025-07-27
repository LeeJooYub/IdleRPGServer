using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPIServer.Models.MasterDB
{
    public class consumbable_item
    {
        public int item_id { get; set; }
        public string item_name { get; set; }
        public string item_description { get; set; }
        public string item_icon { get; set; }
        public int effect_value { get; set; } // 효과 값 (예: HP 회복량)
        public DateTime create_dt { get; set; } // 생성일시
    }

    public class valuable_item
    {
        public int item_id { get; set; }
        public string item_name { get; set; }
        public string item_description { get; set; }
        public string item_icon { get; set; }
        public int effect_value { get; set; } // 효과 값 (예: HP 회복량)
        public DateTime create_dt { get; set; } // 생성일시
    }

    public class ingredient_item
    {
        public int item_id { get; set; }
        public string item_name { get; set; }
        public string item_description { get; set; }
        public string item_icon { get; set; }
        public DateTime create_dt { get; set; } // 생성일시
    }

    public class box
    {
        public int box_id { get; set; }
        public string box_name { get; set; }
        public string box_description { get; set; }
        public string box_icon { get; set; }
        public DateTime create_dt { get; set; } // 생성일시
    }
    
    public class normal_box : box
    {
        //보상 아이템 목록
        public string reward1_item_id { get; set; } // 보상 아이템 ID 1
        public string reward1_item_name { get; set; } // 보상 아이템 이름
        public int reward1_item_qty { get; set; } // 보상 아이템 수량 1

        public string reward2_item_id { get; set; } // 보상 아이템 ID 2
        public string reward2_item_name { get; set; } // 보상 아이템 이름
        public int reward2_item_qty { get; set; } // 보상 아이템 수량 2

        public string reward3_item_id { get; set; } // 보상 아이템 ID 3
        public string reward3_item_name { get; set; } // 보상 아이템 이름
        public int reward3_item_qty { get; set; } // 보상 아이템 수량 3

        public string reward4_item_id { get; set; } // 보상 아이템 ID 4
        public string reward4_item_name { get; set; } // 보상 아이템 이름
        public int reward4_item_qty { get; set; } // 보상 아이템 수량 4

        public string reward5_item_id { get; set; } // 보상 아이템 ID
        public string reward5_item_name { get; set; } // 보상 아이템 이름
        public int reward5_item_qty { get; set; } // 보상 아이템 수
    }

    public class selectable_box : box
    {
    }

    public class probability_box : box
    {
    }

}