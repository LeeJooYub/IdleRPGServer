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
using GameAPIServer.DTO.Service;

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
        var loginOutput = new LoginOutput();

        // 하이브 토큰 체크
        var errorCode = await VerifyTokenToHive(input.PlayerUid, input.Token);
        loginOutput.ErrorCode = errorCode;

        if (errorCode != ErrorCode.None)
        {
            return loginOutput;
        }

        // 유저 있는지 확인
        var user = await _gameDb.FindUserByAccountId(input.PlayerUid);
        
        // 유저가 없으면 생성
        if (user == null)
        {
            try
            {
                await _gameDb.CreateUser(new User
                {
                    player_uid = input.PlayerUid,
                });
            }
            catch (Exception ex)
            {
                _logger.ZLogError($"[AuthService.Login] User creation failed: {ex.Message}");
                return loginOutput;
            }
        }
        _logger.ZLogDebug($"[AuthService.Login] After CreateUser, PlayerUid: {user.player_uid}");

        // 토큰 Redis에 저장
        try{
            errorCode = await _memoryDb.SetTokenAsync(input.Token, user.player_uid);
        }
        catch (Exception ex)
        {
            _logger.ZLogError($"[AuthService.Login] Error setting token in Redis: {ex.Message}");
            loginOutput.ErrorCode = ErrorCode.LoginFailAddRedis;
            return loginOutput;
        }   

        return loginOutput;
    }


    public async Task<ErrorCode> VerifyTokenToHive(Int64 PlayerUid, string token)
    {
        try
        {
            using var client = new HttpClient();

            var verifyTokenToHiveAddress = _hiveServerAPIAddress + "/verifytoken";
            var hiveResponse = await client.PostAsJsonAsync(verifyTokenToHiveAddress, new { AccountUid = PlayerUid, Token = token });
            if (hiveResponse == null || !ValidateHiveResponse(hiveResponse))
            {
                _logger.ZLogInformation($"[VerifyTokenToHive Service] ErrorCode:{ErrorCode.Hive_Fail_InvalidResponse}, PlayerID = {PlayerUid}, Token = {token}, StatusCode = {hiveResponse?.StatusCode}");
                return ErrorCode.Hive_Fail_InvalidResponse;
            }


            var authResult = await hiveResponse.Content.ReadFromJsonAsync<HiveVerifyTokenResponse>();
            _logger.ZLogInformation($"[VerifyTokenToHive Service] PlayerID = {PlayerUid}, Token = {token}, AuthResult = {authResult?.Result}");
            if (!authResult.Result.Equals(ErrorCode.None))
            {
                return ErrorCode.Hive_Fail_InvalidResponse;
            }

            //문제 없음 (유효한 토큰)
            return ErrorCode.None;
        }
        catch
        {
            _logger.ZLogInformation($"[VerifyTokenToHive Service] ErrorCode:{ErrorCode.Hive_Fail_InvalidResponse}, PlayerID = {PlayerUid}, Token = {token}");
            return ErrorCode.Hive_Fail_InvalidResponse;
        }
    }




    public bool ValidateHiveResponse(HttpResponseMessage? response)
    {
        return response?.StatusCode == System.Net.HttpStatusCode.OK;
    }
}
