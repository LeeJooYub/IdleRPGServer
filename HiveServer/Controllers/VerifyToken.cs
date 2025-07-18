using System;
using HiveServer.Model.DTO;
using HiveServer.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ZLogger;
using HiveServer.Servcies.Interfaces;
using HiveServer.Servcies;


namespace HiveServer.Controllers;

[ApiController]
[Route("")]
public class VerifyTokenController : ControllerBase
{
    readonly string _saltValue;
    readonly ILogger<VerifyTokenController> _logger;
    readonly IHiveDb _hiveDb;

    public VerifyTokenController(ILogger<VerifyTokenController> logger, IHiveDb hiveDb, IConfiguration config)
    {
        _saltValue = config.GetSection("TokenSaltValue").Value;
        _logger = logger;
        _hiveDb = hiveDb;
    }

    [HttpPost("VerifyToken")]
    public VerifyTokenResponse Verify([FromBody] VerifyTokenRequest request) {
        VerifyTokenResponse response = new();

        if (Security.MakeHashingToken(_saltValue, request.PlayerId)!=request.HiveToken)
        {
            _logger.ZLogDebug(
                $"[AccoutDb.CreateAccount] ErrorCode: {ErrorCode.VerifyTokenFail}");
            response.Result =  ErrorCode.VerifyTokenFail;
        }
        return response;
    }
}
