using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;

namespace GameAPIServer.DTO.ServiceDTO
{

    public class GetUserInfoServiceInput
    {
        public Int64 AccountId { get; set; }
    }

    public class GetUserInfoServiceOutput
    {
        public Int64 AccountId { get; set; }
        public string Nickname { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public ErrorCode ErrorCode { get; set; }
    }

    public class GetUserCurrencyServiceInput
    {
        public Int64 AccountId { get; set; }
    }

    public class GetUserCurrencyServiceOutput
    {
        public Int64 AccountId { get; set; }
        public List<Currency> CurrencyList { get; set; } = new List<Currency>();
        public ErrorCode ErrorCode { get; set; }
    }


    public class GetUserInventoryServiceInput
    {
        public Int64 AccountId { get; set; }
    }


    public class GetUserInventoryServiceOutput
    {
        public Int64 AccountId { get; set; }
        public List<UserInventory> InventoryItems { get; set; } = new List<UserInventory>();
        public ErrorCode ErrorCode { get; set; }
    }

    public class GetUserCharacterServiceInput
    {
        public Int64 AccountId { get; set; }
    }

    public class GetUserCharacterInfoServiceOutput
    {
        public Int64 AccountId { get; set; }
        public UserCharacter Character { get; set; } = new UserCharacter();
        public ErrorCode ErrorCode { get; set; }
    }


}