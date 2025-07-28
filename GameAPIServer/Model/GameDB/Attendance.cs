using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPIServer.Models.GameDB
{
    public class Attendance
    {
        public Int64 account_uid { get; set; } // 사용자 계정 ID
        public Int64 attendance_book_id { get; set; } // 출석 ID
        public DateTime last_attendance_dt { get; set; } = DateTime.UtcNow; // 마지막 출석 날짜
        public int attendance_continue_cnt { get; set; } = 0; // 연속 출석 일수
    }
}