using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using GameAPIServer.DTO.ControllerDTO;
using GameAPIServer.DTO.ServiceDTO;
using GameAPIServer.Services.Interfaces;
using GameAPIServer.Repository.Interfaces;

using ZLogger;

namespace GameAPIServer.Controllers.Attendance;

[ApiController]
[Route("attendance")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;
    private readonly IMemoryDb _memoryDb;

    public AuthController(
        ILogger<AuthController> logger,
        IAuthService authService,
        IMemoryDb memoryDb)
    {
        _logger = logger;
        _authService = authService;
        _memoryDb = memoryDb;
    }

    /// <summary>
    /// 로그인 API<br/>
    /// (로그인 정보가 클라이언트 캐시에 있는 상태) 자동 로그인을 시작합니다. 우선 플랫폼 ID와 플랫폼 토큰을 플랫폼에 보내 검증 후, 게임 ID와 게임 토큰을 발급합니다.
    /// </summary>
    [HttpPost("Login")]
    public async Task<LoginResponse> Login([FromBody] LoginRequest request)
    {
        var result = new LoginServiceOutput();
        result = await _authService.Login(new LoginServiceInput
        {
            AccountId = request.AccountId,
            HiveToken = request.HiveToken
        });


        var response = new LoginResponse
        {
            ErrorCode = result.ErrorCode,
            AccountId = result.AccountId,
            SessionKey = result.GameServerToken,
            // Nickname = request.Nickname // Nickname은 현재 사용하지 않음
        };

        return response;
    }


    /// <summary>
    /// 로그아웃 API </br>
    /// 해당 유저의 토큰을 Redis에서 삭제합니다.
    /// </summary>
    [HttpPost("Logout")]
    public async Task<ErrorCode> DeleteUserToken([FromBody] LogoutRequest request)
    {
        var errorCode = await _memoryDb.DelUserAuthAsync(request.AccountId);

        return errorCode;
    }
    


}


