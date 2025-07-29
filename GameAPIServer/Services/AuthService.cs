using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using ZLogger;

using GameAPIServer.Models.GameDB;
using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Services.Interfaces;
using GameAPIServer.DTO.ExternelAPI;
using GameAPIServer.DTO.ServiceDTO;

namespace GameAPIServer.Services;

public class AuthService : IAuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly IGameDb _gameDb;
    private readonly IMemoryDb _memoryDb;
    private readonly string _hiveServerAPIAddress;

    public AuthService(
        ILogger<AuthService> logger,
        IConfiguration configuration,
        IGameDb gameDb,
        IMemoryDb memoryDb)
    {
        _logger = logger;
        _gameDb = gameDb;
        _memoryDb = memoryDb;
        _hiveServerAPIAddress = configuration.GetSection("HiveServerAddress").Value;
    }

    //TODO : 다른 플랫폼에 확장 가능하게 정리. 현재는 하이브만 상정하고 만든 기능.
    public async Task<LoginOutput> Login(LoginInput input)
    {
        var loginResult = new LoginOutput();

        // 하이브 토큰 체크
        var errorCode = await VerifyTokenToHive(input.AccountId, input.Token);
        loginResult.ErrorCode = errorCode;

        if (errorCode != ErrorCode.None)
        {
            return loginResult;
        }

        // 유저 있는지 확인
        (errorCode, Int64 accountId) = await _gameDb.FindUserByPlatformId(input.AccountId);
        loginResult.ErrorCode = errorCode;
        
        // 유저가 없으면 생성
        if (errorCode == ErrorCode.LoginFailUserNotExist)
        {

            (errorCode, accountId) = await _gameDb.CreateUser(new AccountInfo
            {
                account_uid = input.AccountId,
            });
            loginResult.ErrorCode = errorCode;
            if (errorCode != ErrorCode.None)
            {
                _logger.ZLogError($"[AuthService.Login] ErrorCode: {errorCode}, PlayerId: {input.AccountId}");
                return loginResult;
            }
        }
        loginResult.AccountId = accountId;

        _logger.ZLogDebug($"[AuthService.Login] After CreateUser, AccountId: {accountId}");

        // 게임 세션 토큰 발급
        var token = Security.CreateAuthToken();
        errorCode = await _memoryDb.ActivateGameTokenAsync(token, accountId);
        
        loginResult.GameServerToken = token;
        if (errorCode != ErrorCode.None)
        {
            return loginResult;
        }


        return loginResult;
    }

    public async Task<ErrorCode> VerifyTokenToHive(Int64 PlatformId, string token)
    {
        try
        {
            using var client = new HttpClient();

            var verifyTokenToHiveAddress = _hiveServerAPIAddress + "/verifytoken";
            var hiveResponse = await client.PostAsJsonAsync(verifyTokenToHiveAddress, new { AccountId = PlatformId, HiveToken = token });
            if (hiveResponse == null || !ValidateHiveResponse(hiveResponse))
            {
                _logger.ZLogDebug($"[VerifyTokenToHive Service] ErrorCode:{ErrorCode.Hive_Fail_InvalidResponse}, PlayerID = {PlatformId}, Token = {token}, StatusCode = {hiveResponse?.StatusCode}");
                return ErrorCode.Hive_Fail_InvalidResponse;
            }


            var authResult = await hiveResponse.Content.ReadFromJsonAsync<HiveVerifyTokenResponse>();
            if (!authResult.ErrorCode.Equals(ErrorCode.None))
            {
                return ErrorCode.Hive_Fail_InvalidResponse;
            }

            //문제 없음 (유효한 토큰)
            return ErrorCode.None;
        }
        catch
        {
            _logger.ZLogDebug($"[VerifyTokenToHive Service] ErrorCode:{ErrorCode.Hive_Fail_InvalidResponse}, PlayerID = {PlatformId}, Token = {token}");
            return ErrorCode.Hive_Fail_InvalidResponse;
        }
    }


    public bool ValidateHiveResponse(HttpResponseMessage? response)
    {
        return response?.StatusCode == System.Net.HttpStatusCode.OK;
    }
}
