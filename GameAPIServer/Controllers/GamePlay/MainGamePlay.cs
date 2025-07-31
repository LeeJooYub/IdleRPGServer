using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using GameAPIServer.DTO.Controller;
using GameAPIServer.DTO.Service;
using GameAPIServer.Services.Interfaces;
using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Models;

using ZLogger;

namespace GameAPIServer.Controllers.GamePlay;

[ApiController]
[Route("main-game-play")]
public class MainGamePlayController : ControllerBase
{
    private readonly ILogger<MainGamePlayController> _logger;
    private readonly IMainGamePlayService _mainGamePlayService;
    private readonly IMemoryDb _memoryDb;

    public MainGamePlayController(
        ILogger<MainGamePlayController> logger,
        IMainGamePlayService mainGamePlayService,
        IMemoryDb memoryDb)
    {
        _logger = logger;
        _mainGamePlayService = mainGamePlayService;
        _memoryDb = memoryDb;
    }


    [HttpPost("stageclear")]
    public async Task<StageClearResponse> StageClear([FromBody] StageClearRequest request)
    {
        var userInfo = HttpContext.Items["userinfo"] as RdbAuthUserData;
        var stageClearOutput = await _mainGamePlayService.StageClear(new StageClearInput
        {
            AccountUid = userInfo.AccountUid,
            StageId = request.StageId,
        });

        var response = new StageClearResponse
        {
            ErrorCode = stageClearOutput.ErrorCode,
            Reward = stageClearOutput.Reward,
        };

        return response;
    }


    [HttpPost("guide-mission-clear")]
    public async Task<GuideMissionClearResponse> GuideMission([FromBody] GuideMissionClearRequest request)
    {
        var errorCode = await _mainGamePlayService.ClearGuideMission(new ClearGuideMissionInput
        {
            AccountUid = request.AccountUid,
            MissionId = request.MissionId,
        });

        return errorCode;
    }
}

