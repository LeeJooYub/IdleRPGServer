using System;

namespace GameAPIServer.Models.GameDB
{
    public class GdbUserInfo
    {
        public int uid { get; set; }
        public string player_id { get; set; }
        public string nickname { get; set; }
        public DateTime created_dt { get; set; }
    }
}

