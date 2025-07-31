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
[Route("stage")]
public class StageController : ControllerBase
{
    private readonly ILogger<StageController> _logger;
    private readonly IMainGamePlayService _mainGamePlayService;
    private readonly IMemoryDb _memoryDb;

    public StageController(
        ILogger<StageController> logger,
        IMainGamePlayService mainGamePlayService,
        IMemoryDb memoryDb)
    {
        _logger = logger;
        _mainGamePlayService = mainGamePlayService;
        _memoryDb = memoryDb;
    }

    // 해당 스테이지에 대한 보상을 받고, 다음 스테이지로 넘거가는 API
    [HttpPost("clearstage")]
    public async Task<StageClearResponse> StageClear([FromBody] StageClearRequest request)
    {
        var userInfo = HttpContext.Items["userinfo"] as RdbAuthUserData;
        var clearStageOutput = await _mainGamePlayService.ClearStageAsync(new ClearStageInput
        {
            PlayerUid = userInfo.PlayerUid,
            StageId = request.StageId,
        });

        var response = new StageClearResponse
        {
            ErrorCode = clearStageOutput.ErrorCode,
            Reward = clearStageOutput.Reward,
        };

        return response;
    }
}

