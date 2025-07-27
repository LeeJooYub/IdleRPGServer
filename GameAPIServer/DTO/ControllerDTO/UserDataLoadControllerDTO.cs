using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;


namespace GameAPIServer.DTO.ControllerDTO
{
    // public class UserAccountInfoLoadRequest
    // {
    //     public Int64 Uid { get; set; } // 유저 ID
    //     public string Token { get; set; } // 인증 토큰
    // }

    // public class UserAccountInfoLoadResponse
    // {
    //     public ErrorCode Result { get; set; } // 결과 코드
    //     public UserData UserData { get; set; } // 유저 데이터
    // }


    public class UserDataCurrenyLoadRequest
    {
        [Required] public Int64 Uid { get; set; } // 유저 ID
        [Required] public string Token { get; set; } // 인증 토큰
    }
    public class UserDataCurrenyLoadResponse
    {
        public ErrorCode Result { get; set; } // 결과 코드
        public List<Currency> CurrencyList { get; set; } = new List<Currency>();// 화폐 데이터
    }

    public class UserDataInventoryLoadRequest
    {
        [Required] public Int64 Uid { get; set; } // 유저 ID
        [Required] public string Token { get; set; } // 인증 토큰
    }

    public class UserDataInventoryLoadResponse
    {
        public ErrorCode Result { get; set; } // 결과 코드
        public List<UserInventory> InventoryItems { get; set; } = new List<UserInventory>();
    }

    public class UserCharacterInfoLoadRequest
    {
        [Required] public Int64 Uid { get; set; } // 유저 ID
        [Required] public string Token { get; set; } // 인증 토큰
    }

    public class UserCharacterInfoLoadResponse
    {
        public ErrorCode Result { get; set; } // 결과 코드
        public UserCharacter Characters { get; set; } = new UserCharacter(); // 캐릭터 정보
    }
}