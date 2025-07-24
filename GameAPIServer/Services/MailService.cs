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
                result.Mails = await _gameDb.GetMailListAsync(command.AccountId, command.Cursor, command.Limit);
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
            var result = new ClaimMailResult();

            try
            {
                // 1. 메일 보상 조회 및 상태 업데이트
                var (errorCode, reward) = await _gameDb.ClaimMailRewardAsync(command.MailId);

                if (errorCode != ErrorCode.None)
                {
                    result.ErrorCode = errorCode;
                    return result;
                }

                // 2. 사용자 정보 업데이트
                errorCode = await _gameDb.UpdateUserRewardsAsync(command.AccountId, reward);

                if (errorCode != ErrorCode.None)
                {
                    result.ErrorCode = errorCode;
                    return result;
                }

                // 3. 성공적으로 보상 반환
                result.ErrorCode = ErrorCode.None;
                result.Reward = reward;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error claiming mail reward for MailId: {MailId}", command.MailId);
                result.ErrorCode = ErrorCode.DatabaseError;
            }

            return result;
        }

        // TODO : Implement DeleteMailAsync
        // public async Task<DeleteMailResult> DeleteMailAsync(DeleteMailCommand command)
        // {
        //     var result = new DeleteMailResult();

        //     try
        //     {
        //         // 메일 삭제 로직 구현
        //         result.ErrorCode = ErrorCode.None; // 성공 시 에러 코드 없음
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Error deleting mail");
        //         result.ErrorCode = ErrorCode.DatabaseError; // 데이터베이스 에러로 설정
        //     }

        //     return result;
        // }
        public async Task<ClaimAllMailsResult> ClaimAllMailRewardsAsync(ClaimAllMailsCommand command)
        {
            var result = new ClaimAllMailsResult
            {
                TotalClaimed = 0,
                Rewards = new List<string>()
            };

            var rewards = await _gameDb.GetRewardsToClaim(command.AccountId);
            if (!rewards.Any())
            {
                result.ErrorCode = ErrorCode.MailReceiveFailMailNotExist;
                return result;
            }

            var updateResult = await _gameDb.UpdateUserRewardsAllAsync(command.AccountId, rewards);
            if (updateResult != ErrorCode.None)
            {
                result.ErrorCode = updateResult;
                return result;
            }

            await _gameDb.MarkAllMailsClaimed(command.AccountId);

            result.TotalClaimed = rewards.Count;
            result.Rewards = rewards.Select(r => $"{r.reward_type} x {r.reward_qty}").ToList();
            result.ErrorCode = ErrorCode.None;

            return result;
        }
    }
}
