using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPIServer.Model.GameDB
{
    public class UserCurrency
    {
        public Int64 account_id { get; set; } // 사용자 계정 ID
        public int gold { get; set; } = 0; // 골드
        public int gem { get; set; } = 0; // 젬
        public DateTime last_updated { get; set; } = DateTime.UtcNow; // 마지막 업데이트 시간
    }
}