using System;
using HiveServer.Model.DTO;
using HiveServer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using HiveServer.Security;
using Microsoft.Extensions.Configuration;
using ZLogger;


namespace HiveServer.Controllers;

[ApiController]
[Route("[controller]")]
public class VerifyToken : ControllerBase
{
    readonly string _saltValue;
    readonly ILogger<VerifyToken> _logger;
    readonly IHiveDb _hiveDb;

    public VerifyToken(ILogger<VerifyToken> logger, IHiveDb hiveDb, IConfiguration config)
    {
        _saltValue = config.GetSection("TokenSaltValue").Value;
        _logger = logger;
        _hiveDb = hiveDb;
    }

    [HttpPost]
    public VerifyTokenResponse Verify([FromBody] VerifyTokenBody request) {
        VerifyTokenResponse response = new();

        if (EncryptionService.MakeHashingToken(_saltValue, request.PlayerId)!=request.HiveToken)
        {
            _logger.ZLogDebug(
                $"[AccoutDb.CreateAccount] ErrorCode: {ErrorCode.VerifyTokenFail}");
            response.Result =  ErrorCode.VerifyTokenFail;
        }
        return response;
    }
}
