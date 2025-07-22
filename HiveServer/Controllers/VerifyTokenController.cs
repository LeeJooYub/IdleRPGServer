
//using System;

using HiveServer.Model.DTO;
using HiveServer.Repository.Interfaces;
using HiveServer.Services;


using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ZLogger;

namespace HiveServer.Controllers;

[ApiController]
[Route("")]
public class VerifyTokenController : ControllerBase
{
    private readonly string _saltValue;
    private readonly ILogger<VerifyTokenController> _logger;

    public VerifyTokenController(ILogger<VerifyTokenController> logger,IConfiguration config)
    {
        _saltValue = config.GetSection("TokenSaltValue").Value;
        _logger = logger;
    }

    [HttpPost("VerifyToken")]
    public VerifyTokenResponse Verify([FromBody] VerifyTokenRequest request)
    {
        VerifyTokenResponse response = new();
        
        if (Security.MakeHashingToken(_saltValue, request.PlayerId) != request.HiveToken)
        {
            _logger.ZLogDebug(
                $"[AccoutDb.CreateAccount] ErrorCode: {ErrorCode.VerifyTokenFail}");
            response.Result = ErrorCode.VerifyTokenFail;
        }

        return response;
    }
}
