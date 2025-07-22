using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using GameAPIServer.DTO.Auth;
using GameAPIServer.Services.Interfaces;
using GameAPIServer.Repository.Interfaces;

using ZLogger;

namespace GameAPIServer.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly IMemoryDb _memoryDb;

    public LoginController(
        ILogger<LoginController> logger,
        IAuthService authService,
        IUserService userService,
        IMemoryDb memoryDb)
    {
        _logger = logger;
        _authService = authService;
        _userService = userService;
        _memoryDb = memoryDb;
    }

    /// <summary>
    /// 로그인 API<br/>
    /// 하이브 토큰을 검증하고, 유저가 없다면 생성, 토큰 발급, 로그인 시간 업데이트, 유저 데이터 로드를 합니다.
    /// </summary>
    [HttpPost("")]
    public async Task<LoginResponse> LoginAndLoadData(LoginRequest request)
    {
        var response = new LoginResponse();

        // 하이브 토큰 체크
        var errorCode = await _authService.VerifyTokenToHive(request.PlayerId, request.HiveToken);
        if (errorCode != ErrorCode.None)
        {
            response.Result = errorCode;
            return response;
        }

        // 유저 있는지 확인
        (errorCode, var uid) = await _authService.VerifyUser(request.PlayerId);

        // TODO: 유저 데이터 생성
        // if (errorCode == ErrorCode.LoginFailUserNotExist)
        // {
        //     (errorCode, uid) = await _gameService.InitNewUserGameData(request.PlayerId, request.Nickname);
        // }
        // if (errorCode != ErrorCode.None)
        // {
        //     response.Result = errorCode;
        //     return response;
        // }

        response.Uid = uid;

        // 토큰 발급
        (errorCode, var token) = await _authService.RegisterToken(uid);
        if (errorCode != ErrorCode.None)
        {
            response.Result = errorCode;
            return response;
        }
        response.Token = token;

        // TODO: 유저 데이터 불러오기
        // (errorCode, response.userData) = await _dataLoadService.LoadUserData(uid);
        // if (errorCode != ErrorCode.None)
        // {
        //     response.Result = errorCode;
        //     return response;
        // }

        _logger.ZLogInformation($"[Login] Uid : {uid}, Token : {token}, PlayerId : {request.PlayerId}");
        return response;
    }
}



