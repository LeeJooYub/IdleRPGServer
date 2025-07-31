using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using GameAPIServer.Services.Interfaces;
using GameAPIServer.Repository.Interfaces;

using ZLogger;
using GameAPIServer.DTO.Controller;
using GameAPIServer.DTO.Service;
using GameAPIServer.Models;

namespace GameAPIServer.Controllers.Attendance;

[ApiController]
[Route("growth")]
public class GrowthController : ControllerBase
{
    private readonly ILogger<GrowthController> _logger;
    private readonly IGrowthService _growthService;
    private readonly IMemoryDb _memoryDb;

    public GrowthController(
        ILogger<GrowthController> logger,
        IGrowthService growthService,
        IMemoryDb memoryDb)
    {
        _logger = logger;
        _growthService = growthService;
        _memoryDb = memoryDb;
    }


    [HttpPost("upgrade-ability")]
    public async Task<AbilityUpgradeResponse> UpgradeAbility([FromBody] AbilityUpgradeRequest request)
    {
        var userInfo = HttpContext.Items["userinfo"] as RdbAuthUserData;
        var abilityUpgradeInput = new AbilityUpgradeInput
        {
            PlayerUid = userInfo.PlayerUid,
            AbilityId = request.AbilityId,
            Delta = request.Delta,
        };
        var errorCode = await _growthService.UpgradeAbilityAsync(abilityUpgradeInput);
        var response = new AbilityUpgradeResponse
        {
            ErrorCode = errorCode
        };
        return response;
    }




}
