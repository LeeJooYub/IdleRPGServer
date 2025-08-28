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

    /// <summary>
    /// WHY: 하이브 토큰 검증 및 유저 생성, Redis 토큰 저장까지 인증 전체 비즈니스 로직 처리
    /// </summary>
    public async Task<LoginOutput> Login(LoginInput input)
    {
        var output = new LoginOutput();

        // 하이브 토큰 체크 (Guard Clause)
        var errorCode = await VerifyTokenToHive(input.PlayerUid, input.Token);
        output.ErrorCode = errorCode;
        if (errorCode != ErrorCode.None)
            return output;

        // 유저 조회 및 없으면 생성
        var user = await _gameDb.FindUserByAccountId(input.PlayerUid);
        if (user is null)
        {
            try
            {
                await _gameDb.CreateUser(new User { player_uid = input.PlayerUid });
                user = await _gameDb.FindUserByAccountId(input.PlayerUid); // 생성 후 재조회
            }
            catch (Exception ex)
            {
                _logger.ZLogError($"[AuthService.Login] User creation failed: {ex.Message}");
                return output;
            }
        }

        _logger.ZLogDebug($"[AuthService.Login] After CreateUser, PlayerUid: {user.player_uid}");

        // Redis에 토큰 저장 (Guard Clause)
        try
        {
            errorCode = await _memoryDb.SetTokenAsync(input.Token, user.player_uid);
            if (errorCode != ErrorCode.None)
            {
                output.ErrorCode = ErrorCode.LoginFailAddRedis;
                return output;
            }
        }
        catch (Exception ex)
        {
            _logger.ZLogError($"[AuthService.Login] Error setting token in Redis: {ex.Message}");
            output.ErrorCode = ErrorCode.LoginFailAddRedis;
            return output;
        }

        return output;
    }

    /// <summary>
    /// WHY: 하이브 서버에 토큰 검증 요청 및 결과 처리
    /// </summary>
    public async Task<ErrorCode> VerifyTokenToHive(long playerUid, string token)
    {
        try
        {
            using var client = new HttpClient();
            var verifyTokenToHiveAddress = _hiveServerAPIAddress + "/verifytoken";
            var hiveResponse = await client.PostAsJsonAsync(verifyTokenToHiveAddress, new { AccountUid = playerUid, Token = token });
            if (hiveResponse is null || !ValidateHiveResponse(hiveResponse))
            {
                _logger.ZLogInformation($"[VerifyTokenToHive Service] ErrorCode:{ErrorCode.Hive_Fail_InvalidResponse}, PlayerID = {playerUid}, Token = {token}, StatusCode = {hiveResponse?.StatusCode}");
                return ErrorCode.Hive_Fail_InvalidResponse;
            }

            var authResult = await hiveResponse.Content.ReadFromJsonAsync<HiveVerifyTokenResponse>();
            _logger.ZLogInformation($"[VerifyTokenToHive Service] PlayerID = {playerUid}, Token = {token}, AuthResult = {authResult?.Result}");
            if (authResult is null || !authResult.Result.Equals(ErrorCode.None))
                return ErrorCode.Hive_Fail_InvalidResponse;

            return ErrorCode.None;
        }
        catch (Exception ex)
        {
            _logger.ZLogInformation($"[VerifyTokenToHive Service] ErrorCode:{ErrorCode.Hive_Fail_InvalidResponse}, PlayerID = {playerUid}, Token = {token}, Exception = {ex.Message}");
            return ErrorCode.Hive_Fail_InvalidResponse;
        }
    }

    /// <summary>
    /// WHY: 하이브 응답 유효성 검사
    /// </summary>
    public bool ValidateHiveResponse(HttpResponseMessage response)
        => response != null && response.StatusCode == System.Net.HttpStatusCode.OK;
}
