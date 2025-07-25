using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using GameAPIServer.DTO.ControllerDTO;
using GameAPIServer.DTO.ServiceDTO;
using GameAPIServer.Services.Interfaces;
using GameAPIServer.Repository.Interfaces;

using ZLogger;

namespace GameAPIServer.Controllers.User;

[ApiController]
[Route("user")]
public class UserInfoController
{
    private readonly ILogger<UserInfoController> _logger;
    private readonly IGameDb _gameDb;

    public UserInfoController(
        ILogger<UserInfoController> logger,
        IGameDb gameDb)
    {
        _logger = logger;
        _gameDb = gameDb;
    }

    /// <summary>
    /// 사용자 정보를 생성하는 API
    /// </summary>
    /// <param name="request">사용자 정보 요청 데이터</param>
    /// <returns>사용자 생성 결과</returns>
    // [HttpPost("getUserInfo")]
    // public async Task<GetUserInfoResponse> GetUserInfo([FromBody] GetUserInfoRequest request)
    // {
    //     GetUserInfoCommand command = new GetUserInfoCommand
    //     {

    //     };
    //     GetUserInfoResult result = await _gameDb.GetUserInfoAsync(command);

    //     GetUserInfoResponse response = new GetUserInfoResponse
    //     {

    //     };
    //     return response;
    // }


    // [HttpPost("getUserCurrency")]
    // public async Task<GetUserCurrencyResponse> GetUserCurrency([FromBody] GetUserCurrencyRequest request)
    // {
    //     GetUserCurrencyCommand command = new GetUserCurrencyCommand
    //     {

    //     };
    //     GetUserCurrencyResult result = await _gameDb.GetUserCurrencyAsync(command);

    //     GetUserCurrencyResponse response = new GetUserCurrencyResponse
    //     {

    //     };
    //     return response;
    // }

    // [HttpPost("getUserInventory")]
    // public async Task<GetUserInventoryResponse> GetUserInventory([FromBody] GetUserInventoryRequest request)
    // {
    //     GetUserInventoryCommand command = new GetUserInventoryCommand
    //     {
    //         AccountId = request.AccountId
    //     };
    //     GetUserInventoryResult result = await _gameDb.GetUserInventoryAsync(command);

    //     GetUserInventoryResponse response = new GetUserInventoryResponse
    //     {
    //         InventoryItems = result.InventoryItems,
    //         ErrorCode = result.ErrorCode // 에러 코드 추가
    //     };
    //     return response;
    // }

    // [HttpPost("getUserCharacter")]
    // public async Task<GetUserCharacterResponse> GetUserCharacter([FromBody] GetUserCharacterRequest request)
    // {
    //     GetUserCharacterCommand command = new GetUserCharacterCommand
    //     {
    //         AccountId = request.AccountId
    //     };
    //     GetUserCharacterResult result = await _gameDb.GetUserCharacterAsync(command);

    //     GetUserCharacterResponse response = new GetUserCharacterResponse
    //     {
    //         CharacterInfo = result.CharacterInfo,
    //         ErrorCode = result.ErrorCode // 에러 코드 추가
    //     };
    //     return response;
    // }

}