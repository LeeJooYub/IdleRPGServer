using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;

namespace GameAPIServer.DTO.ServiceDTO
{

    public class GetUserInfoCommand
    {
        public Int64 AccountId { get; set; }
    }

    public class GetUserInfoResult
    {
        public Int64 AccountId { get; set; }
        public string Nickname { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public ErrorCode ErrorCode { get; set; }
    }

    public class GetUserCurrencyCommand
    {
        public Int64 AccountId { get; set; }
    }

    public class GetUserCurrencyResult
    {
        public Int64 AccountId { get; set; }
        public List<Currency> CurrencyList { get; set; } = new List<Currency>();
        public ErrorCode ErrorCode { get; set; }
    }


    public class GetUserInventoryCommand
    {
        public Int64 AccountId { get; set; }
    }


    public class GetUserInventoryResult
    {
        public Int64 AccountId { get; set; }
        public List<UserInventory> InventoryItems { get; set; } = new List<UserInventory>();
        public ErrorCode ErrorCode { get; set; }
    }

    public class GetUserCharacterInfoCommand
    {
        public Int64 AccountId { get; set; }
    }

    public class GetUserCharacterInfoResult
    {
        public Int64 AccountId { get; set; }
        public UserCharacter Character { get; set; } = new UserCharacter();
        public ErrorCode ErrorCode { get; set; }
    }


}