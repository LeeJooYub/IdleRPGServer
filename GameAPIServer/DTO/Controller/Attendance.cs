using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameAPIServer.Models.MasterDB;

namespace GameAPIServer.DTO.Controller.DTO
{
    public class CheckTodayRequest
    {
        public long AccountId { get; set; }
        public int AttendanceBookId { get; set; }
    }

    public class CheckTodayResponse : ErrorCodeDTO
    {
        public RewardData Reward { get; set; } // 보상 데이터
    }

}