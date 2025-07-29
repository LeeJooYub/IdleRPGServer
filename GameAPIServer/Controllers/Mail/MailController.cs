using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using GameAPIServer.DTO.ControllerDTO;
using GameAPIServer.DTO.ServiceDTO;
using GameAPIServer.Services.Interfaces;
using GameAPIServer.Repository.Interfaces;

using ZLogger;

namespace GameAPIServer.Controllers.Mail;



[ApiController]
[Route("[controller]")]
public class MailController
{
    private readonly ILogger<MailController> _logger;
    private readonly IMailService _mailService; 

    public MailController(
        ILogger<MailController> logger,
        IMailService mailService)
    {
        _logger = logger;
        _mailService = mailService;
    }

    /// <summary>
    /// 메일 목록 조회 API
    /// 사용자의 메일함에서 메일 목록을 조회하고, 읽지 않은 메일을 표시합니다.
    /// </summary>
    /// <param name="request">메일 목록 요청 데이터</param>
    /// <returns>메일 목록 응답 데이터</returns>
    [HttpPost("list")]
    public async Task<MailListResponse> GetMailListAsync([FromBody] MailListRequest request)
    {
        var input = new MailListInput
        {
            AccountId = request.AccountId,
            Cursor = request.Cursor, // Updated to ensure DateTime cursor is passed
            Limit = request.Limit
        };

        var result = await _mailService.GetMailListAsync(input);

        var response = new MailListResponse
        {
            Mails = result.Mails,
            ErrorCode = result.ErrorCode, // 에러 코드 추가
            NextCursor = result.NextCursor // 다음 페이지를 위한 커서 값 추가
        };

        return response;
    }


    /// <summary>
    /// 메일 보상 수령 API
    /// 특정 메일의 보상을 수령합니다.
    /// </summary>
    /// <param name="request">메일 보상 수령 요청 데이터</param>
    /// <returns>메일 보상 수령 응답 데이터</returns>
    [HttpPost("claim")]
    public async Task<ClaimMailResponse> ClaimMailRewardAsync([FromBody] ClaimMailRequest request)
    {
        var input = new ClaimMailInput
        {
            MailId = request.MailId,
            AccountId = request.AccountId
        };
        var result = await _mailService.ClaimMailRewardAsync(input);

        var response = new ClaimMailResponse
        {
            ErrorCode = result.ErrorCode,
            Rewards = result.Rewards // 보상 목록 추가
        };

        return response;
    }

    /// <summary>
    /// 메일 삭제 API
    /// 특정 메일을 삭제합니다.
    /// </summary>
    /// <param name="request">메일 삭제 요청 데이터</param>
    /// <returns>메일 삭제 응답 데이터</returns>
    [HttpPost("delete")]
    public async Task<DeleteMailResponse> DeleteMailAsync([FromBody] DeleteMailRequest request)
    {
        // 메일 삭제
        var input = new DeleteMailInput
        {
            MailId = request.MailId,
            AccountId = request.AccountId
        };
        var result = await _mailService.DeleteMailAsync(input);



        var response = new DeleteMailResponse
        {
            ErrorCode = result.ErrorCode
        };
        return response;
    }

    /// <summary>
    /// 모든 메일 보상 일괄 수령 API
    /// 사용자의 메일함에 있는 모든 메일의 보상을 일괄 수령합니다.
    /// </summary>
    /// <param name="request">모든 메일 보상 수령 요청 데이터</param>
    /// <returns>모든 메일 보상 수령 응답 데이터</returns>
    // [HttpPost("claimAll")]
    // public async Task<ClaimAllMailsResponse> ClaimAllMailRewardsAsync([FromBody] ClaimAllMailsRequest request)
    // {
    //     // 모든 메일 보상 일괄 수령
    //     ClaimAllMailsCommand command = new ClaimAllMailsCommand
        // 메일 삭제
        // var command = new DeleteMailCommand
        // {
        //     MailId = request.MailId,
        //     AccountId = request.AccountId
        // };
        // var result = await _mailService.DeleteMailAsync(command);

        // var response = new DeleteMailResponse
        // {
        //     ErrorCode = result.ErrorCode
        // };
        // return response;
    // }

}
