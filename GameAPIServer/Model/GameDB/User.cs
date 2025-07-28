using System;

namespace GameAPIServer.Models.GameDB
{
    public class AccountInfo
    {
        public Int64 account_uid { get; set; } = 0;
        // public Int64 platform_id { get; set; }
        // public string platform_name { get; set; } = "hive";
        public string create_dt { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
    }
}

