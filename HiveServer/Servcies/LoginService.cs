using System;
using System.Threading.Tasks;

using HiveServer.Model.DTO;
using HiveServer.Repository.Interfaces;
using HiveServer.Services.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HiveServer.Services;

public class LoginService : ILoginService
{
    readonly string _saltValue;
    readonly ILogger<LoginService> _logger;
    readonly IHiveDb _hiveDb;

    public LoginService(ILogger<LoginService> logger, IHiveDb hiveDb, IConfiguration config)
    {
        _saltValue = config.GetSection("TokenSaltValue").Value;
        _logger = logger;
        _hiveDb = hiveDb;
    }

    public async Task<LoginHiveResponse> Login(LoginHiveRequest request)
    {
        LoginHiveResponse response = new();
        (response.Result, response.PlayerId) = await VerifyUser(request);
        response.HiveToken = Security.MakeHashingToken(_saltValue, response.PlayerId);

        return response;
    }

    public async Task<(ErrorCode, Int64)> VerifyUser(LoginHiveRequest request)
    {
        var userInfo = await _hiveDb.GetAccountByEmailAsync(request.Email);
        if (userInfo is null)
            return (ErrorCode.LoginFailUserNotExist, 0);

        var hash = Security.MakeHashingPassword(userInfo.SaltValue, request.Password);
        if (userInfo.Pw != hash)
            return (ErrorCode.LoginFailPwNotMatch, 0);

        return (ErrorCode.None, userInfo.PlayerId);
    }

}
