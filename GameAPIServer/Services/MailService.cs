using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using ZLogger;

using GameAPIServer.Models.GameDB;
using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Services.Interfaces;
using GameAPIServer.DTO.ExternelAPI;
using GameAPIServer.DTO.ControllerDTO;
using GameAPIServer.DTO.ServiceDTO;

namespace GameAPIServer.Services
{
    public class MailService : IMailService
    {
        private readonly ILogger<MailService> _logger;
        private readonly IGameDb _gameDb;

        public MailService(
            ILogger<MailService> logger,
            IGameDb gameDb)
        {
            _logger = logger;
            _gameDb = gameDb;
        }


        public async Task<MailListResult> GetMailListAsync(MailListCommand command)
        {
            MailListResult result = new MailListResult();

            if (!command.Cursor.HasValue)
            {
                command.Cursor = DateTime.UtcNow; // 기본값으로 현재 시간을 설정
            }


            try
            {
                (result.Mails, result.NextCursor) = await _gameDb.GetMailListAsync(command.AccountId, command.Cursor, command.Limit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching mail list");
                result.ErrorCode = ErrorCode.DatabaseError;
            }


            return result;
        }

        public async Task<ClaimMailResult> ClaimMailRewardAsync(ClaimMailCommand command)
        {
            ClaimMailResult result = new ClaimMailResult();
            ErrorCode errorCode;
            List<MailRewardDto> rewards = new List<MailRewardDto>();
            try
            {
                (errorCode, rewards) = await _gameDb.ClaimMailRewardAsync(command.MailId);
                result.ErrorCode = errorCode;
                result.Rewards = rewards;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error claiming mail reward for MailId: {MailId}", command.MailId);
                result.ErrorCode = ErrorCode.DatabaseError;
            }
            if (result.ErrorCode != ErrorCode.None)
            {
                return result; // 에러가 발생한 경우 바로 반환
            }

            // TODO: 보상 추출 후 사용자 보상 업데이트
            // try
            // {
            //     errorCode = await _gameDb.UpdateUserRewardsAsync(command.AccountId, result.Rewards);
            // }
            // catch (Exception ex)
            // {
            //     _logger.LogError(ex, "Error claiming mail reward for MailId: {MailId}", command.MailId);
            //     result.ErrorCode = ErrorCode.DatabaseError;
            // }

            try
            {
                errorCode = await _gameDb.UpdateMailClaimStatusAsync(command.MailId);
                result.ErrorCode = errorCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error claiming mail reward for MailId: {MailId}", command.MailId);
                result.ErrorCode = ErrorCode.DatabaseError;
            }
            if (result.ErrorCode != ErrorCode.None)
            {
                return result; // 에러가 발생한 경우 바로 반환
            }


            return result;
        }

        public async Task<DeleteMailResult> DeleteMailAsync(DeleteMailCommand command)
        {
            var result = new DeleteMailResult();
            try
            {
                result.ErrorCode = await _gameDb.DeleteMailAsync(command.MailId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting mail with MailId: {MailId}", command.MailId);
                result.ErrorCode = ErrorCode.DatabaseError;
            }

            return result;
        }

        //TODO : 전체 수령
        // public async Task<ClaimAllMailsResult> ClaimAllMailRewardsAsync(ClaimAllMailsCommand command)
        // {
        //     var result = new ClaimAllMailsResult
        //     {
        //         TotalClaimed = 0,
        //         Rewards = new List<string>()
        //     };

        //     var rewards = await _gameDb.GetRewardsToClaim(command.AccountId);
        //     if (!rewards.Any())
        //     {
        //         result.ErrorCode = ErrorCode.MailReceiveFailMailNotExist;
        //         return result;
        //     }

        //     var updateResult = await _gameDb.UpdateUserRewardsAllAsync(command.AccountId, rewards);
        //     if (updateResult != ErrorCode.None)
        //     {
        //         result.ErrorCode = updateResult;
        //         return result;
        //     }

        //     await _gameDb.MarkAllMailsClaimed(command.AccountId);

        //     result.TotalClaimed = rewards.Count;
        //     result.Rewards = rewards.Select(r => $"{r.reward_type} x {r.reward_qty}").ToList();
        //     result.ErrorCode = ErrorCode.None;

        //     return result;
        // }
    }



}
