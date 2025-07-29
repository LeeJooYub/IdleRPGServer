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
using GameAPIServer.Models.MasterDB;

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

        public async Task<MailListOutput> GetMailListAsync(MailListInput input)
        {
            var result = new MailListOutput();

            // 커서가 없으면 현재 시간을 기본값으로 설정
            if (!input.Cursor.HasValue)
            {
                input.Cursor = DateTime.UtcNow; // 기본값으로 현재 시간을 설정
            }

    
            try
            {
                (result.Mails, result.NextCursor) = await _gameDb.GetMailListAsync(input.AccountUid, input.Cursor, input.Limit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching mail list");
                result.ErrorCode = ErrorCode.DatabaseError;
            }
            return result;
        }


        //TODO : 트랜잭션으로 묶기?
        public async Task<ReceiveMailOutput> ReceiveMailRewardAsync(ReceiveMailInput input)
        {
            var result = new ReceiveMailOutput();
            var reward = new RewardData();

            // 메일 정보 조회
            var mail = await _gameDb.GetMailAsync(input.MailId);
            if (mail == null)
            {
                _logger.LogError("Mail not found for MailId: {MailId}", input.MailId);
                result.ErrorCode = ErrorCode.MailNotFound;
                return result;
            }
            else if (mail.reward_receive_yn == 'Y') // 이미 수령한 메일
            {
                _logger.LogWarning("Mail already claimed for MailId: {MailId}", input.MailId);
                result.ErrorCode = ErrorCode.MailAlreadyClaimed;
                return result;
            }


            //리워드 받기
            try
            {
                reward = await _gameDb.GetMailRewardAsync(input.MailId);
                result.Reward = reward; // 보상 데이터 설정
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching mail reward for MailId: {MailId}", input.MailId);
                result.ErrorCode = ErrorCode.DatabaseError;
                return result;
            }


            // 유저 상태 (돈,화폐) 업데이트
            try
            {
                if (reward.reward_type_cd == "01") // Assuming '01' is the currency type code
                {
                    // 화폐 리워드 처리
                    await _gameDb.UpdateUserCurrencyAsync(input.AccountUid, reward.reward_id.Value, reward.reward_qty.Value);
                }
                else if (reward.reward_type_cd == "02") // Assuming '02' is the item type code
                {
                    // 아이템 리워드 처리
                    await _gameDb.UpdateUserInventoryItemAsync(input.AccountUid, reward.reward_id.Value, reward.reward_qty.Value);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user state for AccountUid: {AccountUid}", input.AccountUid);
                result.ErrorCode = ErrorCode.DatabaseError;
                return result;
            }

            // 메일 수령 상태 업데이트
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


        public async Task<ReceiveAllMailOutput> ReceiveAllMailRewardAsync(ReceiveAllMailInput input)
        {
            var result = new ReceiveAllMailOutput();
            var mails = new List<Mail>();
            var rewards = new List<RewardData>();

            try
            {
                mails = await _gameDb.GetAllMailsRewardAsync(input.AccountUid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error claiming all mail rewards for AccountUid: {AccountUid}", input.AccountUid);
                result.ErrorCode = ErrorCode.DatabaseError;
                return result;
            }

            // TODO : 하나하나 업데이트? -> 한번에 업데이트
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
                    // 유저 상태 업데이트
                    if (reward.reward_type_cd == "01") // 화폐
                    {
                        await _gameDb.UpdateUserCurrencyAsync(input.AccountUid, reward.reward_id.Value, reward.reward_qty.Value);
                    }
                    else if (reward.reward_type_cd == "02") // 아이템
                    {
                        await _gameDb.UpdateUserInventoryItemAsync(input.AccountUid, reward.reward_id.Value, reward.reward_qty.Value);
                    }
                    rewards.Add(reward);
                }

                await _gameDb.UpdateAllMailRewardStatusAsync(input.AccountUid, input.Now);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error claiming all mail rewards for AccountUid: {AccountUid}", input.AccountUid);
                result.ErrorCode = ErrorCode.DatabaseError;
                return result;
            }

            return result;
        }



        public async Task<DeleteMailOutput> DeleteMailAsync(DeleteMailInput input)
        {
            var affectedRows = 0;
            var result = new DeleteMailOutput();

            try
            {
                affectedRows = await _gameDb.DeleteMailAsync(input.MailId);
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
