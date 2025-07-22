using System;

namespace HiveServer.Model.Entity;

public class AccountInfo
{
    public Int64 player_id { get; set; } = 0;
    public string email { get; set; }
    public string pw { get; set; }
    public string salt { get; set; }
    public string create_dt { get; set; } = string.Empty;
}
