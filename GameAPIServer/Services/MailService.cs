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
using GameAPIServer.DTO.Controller;
using GameAPIServer.DTO.Service;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.Models;

namespace GameAPIServer.Services
{
    public class MailService : IMailService
    {
        private readonly ILogger<MailService> _logger;
        private readonly IGameDb _gameDb;

        public MailService(ILogger<MailService> logger, IGameDb gameDb)
        {
            _logger = logger;
            _gameDb = gameDb;
        }

        /// <summary>
        /// WHY: 메일 리스트 조회. 커서 없을 시 현재 시간으로 기본값 설정
        /// </summary>
        public async Task<MailListOutput> GetMailListAsync(MailListInput input)
        {
            var result = new MailListOutput();
            if (!input.Cursor.HasValue)
                input.Cursor = DateTime.UtcNow;

            try
            {
                (result.Mails, result.NextCursor) = await _gameDb.GetMailListAsync(input.PlayerUid, input.Cursor, input.Limit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching mail list");
                result.ErrorCode = ErrorCode.DatabaseError;
            }
            return result;
        }

        /// <summary>
        /// WHY: 메일 보상 수령. 이미 수령/존재하지 않는 메일 예외 처리, 보상 지급, 상태 업데이트
        /// </summary>
        public async Task<ReceiveMailOutput> ReceiveMailRewardAsync(ReceiveMailInput input)
        {
            var result = new ReceiveMailOutput();

            var mail = await _gameDb.GetMailAsync(input.MailId);
            if (mail is null)
            {
                _logger.LogError("Mail not found for MailId: {MailId}", input.MailId);
                result.ErrorCode = ErrorCode.MailNotFound;
                return result;
            }
            if (mail.reward_receive_yn == 'Y')
            {
                _logger.LogWarning("Mail already claimed for MailId: {MailId}", input.MailId);
                result.ErrorCode = ErrorCode.MailAlreadyClaimed;
                return result;
            }

            try
            {
                var reward = await _gameDb.GetMailRewardAsync(input.MailId);
                result.Reward = reward;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching mail reward for MailId: {MailId}", input.MailId);
                result.ErrorCode = ErrorCode.DatabaseError;
                return result;
            }

            try
            {
                result.ErrorCode = await _gameDb.UpdateUserFromRewardAsync(input.PlayerUid, result.Reward);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user state for PlayerUid: {PlayerUid}", input.PlayerUid);
                result.ErrorCode = ErrorCode.DatabaseError;
                return result;
            }

            try
            {
                await _gameDb.UpdateMailRewardStatusAsync(input.MailId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error claiming mail reward for MailId: {MailId}", input.MailId);
                result.ErrorCode = ErrorCode.DatabaseError;
                return result;
            }

            return result;
        }

        /// <summary>
        /// WHY: 모든 메일 보상 일괄 수령. 각 보상별 유저 상태 업데이트, 메일 상태 일괄 업데이트
        /// </summary>
        public async Task<ReceiveAllMailOutput> ReceiveAllMailRewardAsync(ReceiveAllMailInput input)
        {
            var result = new ReceiveAllMailOutput();
            List<Mail> mails;
            try
            {
                mails = await _gameDb.GetAllMailsRewardAsync(input.PlayerUid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error claiming all mail rewards for PlayerUid: {PlayerUid}", input.PlayerUid);
                result.ErrorCode = ErrorCode.DatabaseError;
                return result;
            }

            try
            {
                foreach (var mail in mails)
                {
                    var reward = new RewardData
                    {
                        reward_id = mail.reward_id,
                        reward_type_cd = mail.reward_type_cd,
                        reward_qty = mail.reward_qty
                    };
                    if (reward.reward_type_cd == "01")
                        await _gameDb.UpdateUserCurrencyAsync(input.PlayerUid, reward.reward_id.Value, reward.reward_qty.Value);
                    if (reward.reward_type_cd == "02")
                        await _gameDb.UpdateUserInventoryItemAsync(input.PlayerUid, reward.reward_id.Value, reward.reward_qty.Value);
                }
                await _gameDb.UpdateAllMailRewardStatusAsync(input.PlayerUid, input.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error claiming all mail rewards for PlayerUid: {PlayerUid}", input.PlayerUid);
                result.ErrorCode = ErrorCode.DatabaseError;
                return result;
            }

            return result;
        }

        /// <summary>
        /// WHY: 메일 삭제. 삭제 결과에 따라 에러코드 반환
        /// </summary>
        public async Task<DeleteMailOutput> DeleteMailAsync(DeleteMailInput input)
        {
            var result = new DeleteMailOutput();
            try
            {
                var affectedRows = await _gameDb.DeleteMailAsync(input.MailId);
                result.ErrorCode = affectedRows == 1 ? ErrorCode.None : ErrorCode.DatabaseError;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting mail with MailId: {MailId}", input.MailId);
                result.ErrorCode = ErrorCode.DatabaseError;
                return result;
            }
            return result;
        }
    }
}
