using System;

namespace HiveServer.Model.Entity;

public class AccountInfo
{
    public Int64 account_id { get; set; } = 0;
    public string email { get; set; }
    public string pwd { get; set; }
    public string salt { get; set; }
    public string create_dt { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
}
