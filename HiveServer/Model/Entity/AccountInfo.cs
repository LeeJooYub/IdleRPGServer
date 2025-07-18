using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiveServer.Model.Entity;

public class AccountInfo
{
    public Int64 player_id { get; set; }
    public string email { get; set; }
    public string pw { get; set; }
    public string salt_value { get; set; }
    public string create_dt { get; set; }
}
