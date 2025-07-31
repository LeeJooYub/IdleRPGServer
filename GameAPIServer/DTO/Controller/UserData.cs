using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;


namespace GameAPIServer.DTO.Controller;


public class UserDataCurrenyLoadRequest
{
    [Required] public string Token { get; set; } // 인증 토큰
}
public class UserDataCurrenyLoadResponse : ErrorCodeDTO
{
    public List<UserCurrency> CurrencyList { get; set; } = new List<UserCurrency>();// 화폐 데이터
}

public class UserDataInventoryLoadRequest
{
    [Required] public string Token { get; set; } // 인증 토큰
}

public class UserDataInventoryLoadResponse : ErrorCodeDTO
{
    public List<UserInventoryItem> InventoryItems { get; set; } = new List<UserInventoryItem>();
}

public class UserCharacterInfoLoadRequest
{
    [Required] public string Token { get; set; } // 인증 토큰
}

public class UserCharacterInfoLoadResponse : ErrorCodeDTO
{
    public UserCharacter Characters { get; set; } = new UserCharacter(); // 캐릭터 정보
}
