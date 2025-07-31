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
[Route("attendance")]
public class AttendanceController : ControllerBase
{
    private readonly ILogger<AttendanceController> _logger;
    private readonly IAttendanceService _attendanceService;
    private readonly IMemoryDb _memoryDb;

    public AttendanceController(
        ILogger<AttendanceController> logger,
        IAttendanceService attendanceService,
        IMemoryDb memoryDb)
    {
        _logger = logger;
        _attendanceService = attendanceService;
        _memoryDb = memoryDb;
    }


    [HttpPost("check-attendance")]
    public async Task<CheckTodayResponse> CheckTodayAttendance([FromBody] CheckTodayRequest request)
    {    
        var userInfo = HttpContext.Items["userinfo"] as RdbAuthUserData;
        var utcNow = DateTime.UtcNow; // 공정성 & 보안을 위해, 서버 기준의 현재시간을 기준으로 출석체크 유효성을 검증한다.
        var checkTodayInput = new CheckTodayInput
        {
            PlayerUid = userInfo.PlayerUid,
            AttendanceBookId = request.AttendanceBookId,
            CheckNthDay = request.CheckNthDay,
            Now = utcNow
        };

        var checkTodayOutput = await _attendanceService.CheckTodayAsync(checkTodayInput);
        var response = new CheckTodayResponse
        {
            ErrorCode = checkTodayOutput.ErrorCode,
            Reward = checkTodayOutput.Reward
        };

        return response;
    }



}


