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

    //TODO : 유저 정보 불러오기
    // [HttpPost("currency")]
    // public async Task<UserDataCurrenyLoadResponse> LoadUserCurrencyData(UserDataCurrenyLoadRequest request)
    // {
    //     var input = new GetUserCurrencyInput
    //     {
    //         AccountUid = request.AccountUid
    //     };
    //     var result = await _userDataLoadService.GetUserCurrencyAsync(input);


    //     var response = new UserDataCurrenyLoadResponse
    //     {
    //         ErrorCode = (ErrorCode)result.ErrorCode,
    //         CurrencyList = result.CurrencyList
    //     };

    //     return response;
    // }


    // [HttpPost("inventory")]
    // public async Task<UserDataInventoryLoadResponse> LoadUserInventoryData(UserDataCurrenyLoadRequest request)
    // {
    //     var input = new GetUserInventoryInput
    //     {
    //         AccountUid = request.AccountUid
    //     };
    //     var result = await _userDataLoadService.GetUserInventoryAsync(input);


    //     var response = new UserDataInventoryLoadResponse
    //     {
    //         ErrorCode = (ErrorCode)result.ErrorCode,
    //         InventoryItems = result.InventoryItems,
    //     };

    //     return response;
    // }
    



}