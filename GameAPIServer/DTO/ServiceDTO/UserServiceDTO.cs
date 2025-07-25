using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPIServer.DTO.ServiceDTO
{
    public class GetUserCurrencyCommand
    {
        public long AccountId { get; set; }
    }

    public class GetUserCurrencyResult
    {
        public long AccountId { get; set; }
        public int Gold { get; set; }
        public int Gem { get; set; }
        public int ErrorCode { get; set; }
    }

    public class GetUserInfoCommand
    {
        public long AccountId { get; set; }
    }

    public class GetUserInfoResult
    {
        public long AccountId { get; set; }
        public string Nickname { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int ErrorCode { get; set; }
    }

}