using System;

namespace GameAPIServer.Models.GameDB;
public class User
{
    public Int64 player_uid { get; set; } = 0;
    public string create_dt { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
    public string last_login_dt { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
}


