using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPIServer.Model.MasterDB
{

    public class Attendance
    {
        public Int64 attendance_id { get; set; } // 출석 ID
        public DateTime start_dt { get; set; } // 출석 생성 날짜
        public DateTime end_dt { get; set; } // 출석 종료 날짜
        public DateTime create_dt { get; set; } = DateTime.UtcNow; // 생성일시
    }

    public class DayInAttendance
    {
        public Int64 attendance_id { get; set; } // 출석 ID
        public int attendance_day { get; set; } // 출석 일수 (1부터 시작)
        public int reward_id { get; set; } // 보상 ID
        public string reward_type_cd { get; set; } // 보상 타입 (예: "GD" : gold, "IT" : item)
        public int reward_qty { get; set; } // 보상 수량
    }
}