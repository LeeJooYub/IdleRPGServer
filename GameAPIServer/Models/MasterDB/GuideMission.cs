using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPIServer.Models.MasterDB;

public class GuideMission : RewardData
{
    public int guide_mission_seq { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public DateTime update_dt { get; set; } = DateTime.UtcNow; // 수정일시
}
