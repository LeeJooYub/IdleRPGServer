using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using GameAPIServer.DTO.ControllerDTO;
using GameAPIServer.DTO.ServiceDTO;
using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.Services.Interfaces;
using GameAPIServer.Repository.Interfaces;

using ZLogger;

namespace GameAPIServer.Controllers.User;

[ApiController]
[Route("loaduserdata")]
public class UserDataLoadController : ControllerBase
{
    private readonly ILogger<UserDataLoadController> _logger;
    private readonly IGameDb _gameDb;

    private readonly IUserDataLoadService _userDataLoadService;

    public UserDataLoadController(
        ILogger<UserDataLoadController> logger,
        IGameDb gameDb,
        IUserDataLoadService userDataLoadService)
    {
        _logger = logger;
        _gameDb = gameDb;
        _userDataLoadService = userDataLoadService;
    }

    /// <summary>
    /// 유저 데이터 로드 API
    /// 게임에 필요한 유저 정보(유저의 정보(점수,재화), 출석 정보)를 조회합니다.
    /// </summary>
    [HttpPost("currency")]
    public async Task<UserDataCurrenyLoadResponse> LoadUserCurrencyData(UserDataCurrenyLoadRequest request)
    {
        var input = new GetUserCurrencyInput
        {
            AccountId = request.Uid
        };
        var result = await _userDataLoadService.GetUserCurrencyAsync(input);


        var response = new UserDataCurrenyLoadResponse
        {
            ErrorCode = (ErrorCode)result.ErrorCode,
            CurrencyList = result.CurrencyList
        };

        return response;
    }


    [HttpPost("inventory")]
    public async Task<UserDataInventoryLoadResponse> LoadUserInventoryData(UserDataCurrenyLoadRequest request)
    {
        var input = new GetUserInventoryInput
        {
            AccountId = request.Uid
        };
        var result = await _userDataLoadService.GetUserInventoryAsync(input);


        var response = new UserDataInventoryLoadResponse
        {
            ErrorCode = (ErrorCode)result.ErrorCode,
            InventoryItems = result.InventoryItems,
        };

        return response;
    }
    

    // [HttpPost("usercharacterinfo")]
    // public async Task<UserCharacterInfoLoadResponse> LoadCharacterInfoData(UserCharacterInfoLoadRequest request)
    // {
    //     UserCharacterInfoLoadResponse response = new();
        
    //     return response;
    // }


}