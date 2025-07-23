using System;

namespace GameAPIServer.Models.GameDB
{
    public class AccountInfo
    {
        public Int64 account_id { get; set; } = 0;
        public Int64 platform_id { get; set; }
        public string platform_name { get; set; } = "hive";
        public string nickname { get; set; } = "default_nickname";
        public string create_dt { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
    }
}

