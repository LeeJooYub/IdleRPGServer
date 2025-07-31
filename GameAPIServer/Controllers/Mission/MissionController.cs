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
[Route("mission")]
public class MissionController : ControllerBase
{
    private readonly ILogger<MissionController> _logger;
    private readonly IMissionService _missionService;
    private readonly IMemoryDb _memoryDb;

    public MissionController(
        ILogger<MissionController> logger,
        IMissionService missionService,
        IMemoryDb memoryDb)
    {
        _logger = logger;
        _missionService = missionService;
        _memoryDb = memoryDb;
    }

    // 해당 가이드 미션에 대한 보상을 받고, 다음 가이드 미션으로 넘어가는 API
    [HttpPost("clearguidemission")]
    public async Task<GuideMissionClearResponse> ClearGuideMission([FromBody] GuideMissionClearRequest request)
    {
        var userInfo = HttpContext.Items["userinfo"] as RdbAuthUserData;
        var clearGuideMissionOutput = await _missionService.ClearGuideMissionAsync(new ClearGuideMissionInput
        {
            PlayerUid = userInfo.PlayerUid,
            GuideMissionSeq = request.GuideMissionSeq,
        });

        var response = new GuideMissionClearResponse
        {
            ErrorCode = clearGuideMissionOutput.ErrorCode,
            Reward = clearGuideMissionOutput.Reward,
            NextGuideMission = clearGuideMissionOutput.NextGuideMission // 다음 가이드 미션 정보 추가
        };

        return response;
    }
}

