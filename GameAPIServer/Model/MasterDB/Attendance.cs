using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameAPIServer.Models.MasterDB;

namespace GameAPIServer.Model.MasterDB
{

    public class AttendanceBook
    {
        public Int64 attendance_book_id { get; set; } // 출석부 ID
        public DateTime start_dt { get; set; } // 출석 생성 날짜
        public DateTime end_dt { get; set; } // 출석 종료 날짜
        public DateTime create_dt { get; set; } = DateTime.UtcNow; // 생성일시
    }

    public class DayInAttendance : RewardData
    {
        public Int64 attendance_book_id { get; set; } // 출석 ID
        public int attendance_day { get; set; } // 출석 일수 (1부터 시작. 한달 출석부 기준으로 1~30)
    }
}