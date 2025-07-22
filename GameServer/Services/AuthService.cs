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
        _hiveServerAPIAddress = configuration.GetSection("HiveServerAddress").Value + "/verifytoken";
    }

    public async Task<ErrorCode> VerifyTokenToHive(long playerId, string token)
    {
        try
        {
            using var client = new HttpClient();
            var hiveResponse = await client.PostAsJsonAsync(_hiveServerAPIAddress, new { PlayerId = playerId, HiveToken = token });

            if (hiveResponse == null || !ValidateHiveResponse(hiveResponse))
            {
                _logger.ZLogDebug($"[VerifyTokenToHive Service] ErrorCode:{ErrorCode.Hive_Fail_InvalidResponse}, PlayerID = {playerId}, Token = {token}, StatusCode = {hiveResponse?.StatusCode}");
                return ErrorCode.Hive_Fail_InvalidResponse;
            }

            var authResult = await hiveResponse.Content.ReadFromJsonAsync<ErrorCodeDTO>();
            if (!ValidateHiveAuthErrorCode(authResult))
            {
                return ErrorCode.Hive_Fail_InvalidResponse;
            }

            return ErrorCode.None;
        }
        catch
        {
            _logger.ZLogDebug($"[VerifyTokenToHive Service] ErrorCode:{ErrorCode.Hive_Fail_InvalidResponse}, PlayerID = {playerId}, Token = {token}");
            return ErrorCode.Hive_Fail_InvalidResponse;
        }
    }

    public async Task<(ErrorCode, int)> VerifyUser(long playerId)
    {
        try
        {
            // playerId로 userInfo 조회
            var userInfo = await _gameDb.GetUserByPlayerId(playerId);
            if (userInfo is null)
            {
                return (ErrorCode.LoginFailUserNotExist, 0);
            }
            return (ErrorCode.None, userInfo.uid);
        }
        catch (Exception e)
        {
            _logger.ZLogError(e, $"[VerifyUser] ErrorCode: {ErrorCode.LoginFailException}, PlayerId: {playerId}");
            return (ErrorCode.LoginFailException, 0);
        }
    }

    public bool ValidateHiveResponse(HttpResponseMessage? response)
    {
        return response?.StatusCode == System.Net.HttpStatusCode.OK;
    }

    private bool ValidateHiveAuthErrorCode(ErrorCodeDTO? authResult)
    {
        return authResult != null && authResult.Result == ErrorCode.None;
    }

    public async Task<(ErrorCode, string)> RegisterToken(int uid)
    {
        var token = Security.CreateAuthToken();
        return (await _memoryDb.RegistUserAsync(token, uid), token);
    }
}
