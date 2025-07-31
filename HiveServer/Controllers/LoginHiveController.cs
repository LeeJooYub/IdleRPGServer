using System.Threading.Tasks;

using HiveServer.Model.DTO;
using HiveServer.Repository.Interfaces;
using HiveServer.Services;
using HiveServer.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using ZLogger;

namespace HiveServer.Controllers;

// 잘 사용하지 않는 컨트롤러
// 게임 서버에서 해당 컨트롤러를 사용하지 않음
// 게임 서버에서 주로 호출하는 것은 VerifyTokenController
[ApiController]
[Route("[controller]")]
public class LoginHiveController : ControllerBase
{
    readonly string _saltValue;
    readonly ILogger<LoginHiveController> _logger;
    readonly IHiveDb _hiveDb;
    readonly ILoginService _loginService;

    public LoginHiveController(ILogger<LoginHiveController> logger, IHiveDb hiveDb,
    IConfiguration config, ILoginService loginService)
    {
        _saltValue = config.GetSection("TokenSaltValue").Value;
        _logger = logger;
        _hiveDb = hiveDb;
        _loginService = loginService;
    }

    [HttpPost("")]
    public async Task<LoginHiveResponse> Login([FromBody] LoginHiveRequest request)
    {
        _logger.ZLogDebug($"[LoginHiveController] Called");
        var response = new LoginHiveResponse();
        (response.Result, response.AccountUid) = await _loginService.Login(request);
        if (response.Result != ErrorCode.None)
            return response;
        response.Token = Security.MakeHashingToken(_saltValue, response.AccountUid);

        return response;

    }
}
