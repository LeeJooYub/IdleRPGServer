using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;

namespace GameAPIServer.DTO.Service;

public class GetUserInfoInput
{
    public Int64 PlayerUid { get; set; }
}

public class GetUserInfoOutput
{
    public Int64 PlayerUid { get; set; }
    public string Nickname { get; set; }
    public int Level { get; set; }
    public int Exp { get; set; }
    public ErrorCode ErrorCode { get; set; }
}

public class GetUserCurrencyInput
{
    public Int64 PlayerUid { get; set; }
}

public class GetUserCurrencyOutput
{
    public Int64 PlayerUid { get; set; }
    public List<UserCurrency> CurrencyList { get; set; } = new List<UserCurrency>();
    public ErrorCode ErrorCode { get; set; }
}


public class GetUserInventoryInput
{
    public Int64 PlayerUid { get; set; }
}


public class GetUserInventoryOutput
{
    public Int64 PlayerUid { get; set; }
    public List<UserInventoryItem> InventoryItems { get; set; } = new List<UserInventoryItem>();
    public ErrorCode ErrorCode { get; set; }
}

public class GetUserCharacterStatusInput
{
    public Int64 PlayerUid { get; set; }
}

public class GetUserCharacterStatusInfoOutput
{
    public Int64 PlayerUid { get; set; }
    public UserCharacterStatus Character { get; set; } = new UserCharacterStatus();
    public ErrorCode ErrorCode { get; set; }
}


