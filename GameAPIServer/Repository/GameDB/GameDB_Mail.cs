using System;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MySqlConnector;
using SqlKata.Execution;
using SqlKata.Extensions;

using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Models.GameDB;
using GameAPIServer.DTO.ServiceDTO;
using GameAPIServer.DTO.ControllerDTO;
using System.Linq;


namespace GameAPIServer.Repository;

public partial class GameDb : IGameDb
{

    // 메일 관련 데이터베이스 작업을 위한 메서드들을 여기에 정의합니다.
    // 예: 메일 목록 조회, 메일 상세 조회, 메일 보상 수령, 메일 삭제 등

    // 예시: 메일 목록 조회
    public async Task<(List<Mail>, DateTime?)> GetMailListAsync(Int64 accountId, DateTime? cursor, int limit)
    {
        // 쿼리 작성: 특정 계정의 메일을 가져오고, receive_dt 기준으로 정렬
        SqlKata.Query query = _queryFactory.Query("mail")
            .Where("account_id", accountId)
            .OrderByDesc("receive_dt") // receive_dt 기준으로 내림차순 정렬
            .Limit(limit);

        // 커서 값이 있는 경우, receive_dt이 커서 값보다 작은 데이터만 가져옴
        if (cursor.HasValue)
        {
            query.Where("receive_dt", "<", cursor.Value);
        }

        // 쿼리 실행 및 결과 반환
        List<Mail> mails = (await query.GetAsync<Mail>()).ToList();
        DateTime? nextCursor = null;
        if (mails.Count > 0)
        {
            nextCursor = mails.Last().receive_dt; // 마지막 메일의 receive_dt를 커서로 사용
        }
        else
        {
            nextCursor = DateTime.UtcNow; // 메일이 없는 경우 현재 시간을 커서로 사용
        }


        return (mails, nextCursor);
    }

    public async Task<ErrorCode> DeleteMailAsync(Int64 mailId)
    {
        SqlKata.Query query = _queryFactory.Query("mail").Where("mail_id", mailId);
        int affectedRows = await query.DeleteAsync();

        if (affectedRows > 0)
        {
            return ErrorCode.None;
        }
        else
        {
            return ErrorCode.NotFoundError; // 메일이 존재하지 않는 경우
        }
    }


    // 보상 추출: Mail 객체에서 reward1~reward8 필드 직접 사


    public async Task<(ErrorCode, List<MailRewardDto>)> ClaimMailRewardAsync(Int64 mailId)
    {
        var mail = await _queryFactory.Query("mail").Where("mail_id", mailId).FirstOrDefaultAsync<Mail>();
        if (mail == null)
        {
            return (ErrorCode.NotFoundError, null);
        }
        else if (mail.is_claimed)
        {
            return (ErrorCode.MailReceiveFailAlreadyReceived, null);
        }

        // 보상 추출
        var rewards = new List<MailRewardDto>();
        if (mail.reward1_id.HasValue) rewards.Add(new MailRewardDto { RewardId = mail.reward1_id, RewardType = mail.reward1_type ?? "", RewardQty = mail.reward1_qty });
        if (mail.reward2_id.HasValue) rewards.Add(new MailRewardDto { RewardId = mail.reward2_id, RewardType = mail.reward2_type ?? "", RewardQty = mail.reward2_qty });
        if (mail.reward3_id.HasValue) rewards.Add(new MailRewardDto { RewardId = mail.reward3_id, RewardType = mail.reward3_type ?? "", RewardQty = mail.reward3_qty });
        if (mail.reward4_id.HasValue) rewards.Add(new MailRewardDto { RewardId = mail.reward4_id, RewardType = mail.reward4_type ?? "", RewardQty = mail.reward4_qty });
        if (mail.reward5_id.HasValue) rewards.Add(new MailRewardDto { RewardId = mail.reward5_id, RewardType = mail.reward5_type ?? "", RewardQty = mail.reward5_qty });
        if (mail.reward6_id.HasValue) rewards.Add(new MailRewardDto { RewardId = mail.reward6_id, RewardType = mail.reward6_type ?? "", RewardQty = mail.reward6_qty });    
        if (mail.reward7_id.HasValue) rewards.Add(new MailRewardDto { RewardId = mail.reward7_id, RewardType = mail.reward7_type ?? "", RewardQty = mail.reward7_qty });
        if (mail.reward8_id.HasValue) rewards.Add(new MailRewardDto { RewardId = mail.reward8_id, RewardType = mail.reward8_type ?? "", RewardQty = mail.reward8_qty });

        if (rewards.Count == 0)
        {
            return (ErrorCode.RewardNotExist, null); // 보상이 없는 경우
        }

        return (ErrorCode.None, rewards);
    }

    public async Task<ErrorCode> UpdateMailClaimStatusAsync(Int64 mailId)
    {
        // 메일의 is_claimed 상태를 true로 업데이트
        int affectedRows = await _queryFactory.Query("mail")
            .Where("mail_id", mailId)
            .UpdateAsync(new { is_claimed = true });

        if (affectedRows > 0)
        {
            return ErrorCode.None;
        }
        else
        {
            return ErrorCode.NotFoundError; // 메일이 존재하지 않는 경우
        }
    }



    public async Task<ErrorCode> UpdateUserRewardsAsync(Int64 accountId, List<(int? RewardId, string RewardType, int? RewardQty)> rewards)
    {
        // reward_type에 따라 다른 테이블 업데이트
        foreach (var reward in rewards)
        {
            switch (reward.RewardType.ToLower())
            {
                case "currency":

                    switch (reward.RewardType.ToLower())
                    {
                        case "gold":
                            int goldUpdated = await _queryFactory.Query("user_currency")
                                .Where("account_id", accountId)
                                .IncrementAsync("gold", reward.RewardQty.Value);

                            if (goldUpdated == 0)
                            {
                                return ErrorCode.NotFoundError; // 사용자 정보가 없는 경우
                            }
                            break;

                        case "gem":
                            int gemUpdated = await _queryFactory.Query("user_currency")
                                .Where("account_id", accountId)
                                .IncrementAsync("gem", reward.RewardQty.Value);

                            if (gemUpdated == 0)
                            {
                                return ErrorCode.NotFoundError; // 사용자 정보가 없는 경우
                            }
                            break;

                        default:
                            return ErrorCode.ValidationError; // 잘못된 보상 타입
                    }
                    break;


                case "item":
                    // user_inventory 테이블에서 아이템 수량 증가
                    int itemUpdated = await _queryFactory.Query("user_inventory")
                        .Where("account_id", accountId)
                        .Where("item_id", reward.RewardId)
                        .IncrementAsync("quantity", reward.RewardQty.Value);

                    // 아이템이 없으면 새로 추가
                    if (itemUpdated == 0)
                    {
                        await _queryFactory.Query("user_inventory")
                            .InsertAsync(new
                            {
                                account_id = accountId,
                                item_id = reward.RewardId,
                                quantity = reward.RewardQty
                            });
                    }
                    break;

                default:
                    // 알 수 없는 reward_type 처리
                    return ErrorCode.ValidationError; // 잘못된 보상 타입
            }
        }


        return ErrorCode.None; // 성공
    }

    // public async Task<ErrorCode> UpdateUserRewardsAllAsync(Int64 accountId, List<RewardInMail> rewards)
    // {
    //     using var trx = _queryFactory.Connection.BeginTransaction();
    //     try
    //     {
    //         // 1. 그룹화된 보상 데이터 준비
    //         var goldSum = rewards.Where(r => r.reward_type == "gold").Sum(r => r.reward_qty);
    //         var gemSum = rewards.Where(r => r.reward_type == "gem").Sum(r => r.reward_qty);
    //         var groupedItems = rewards.Where(r => r.reward_type == "item")
    //                                   .GroupBy(r => r.reward_id)
    //                                   .Select(g => new { item_id = g.Key, quantity = g.Sum(r => r.reward_qty) });

    //         // 2. gold와 gem 업데이트
    //         // gold 업데이트
    //         if (goldSum > 0)
    //         {
    //             int updatedGold = await _queryFactory.Query("user_currency")
    //                 .Where("account_id", accountId)
    //                 .IncrementAsync("gold", goldSum, trx);

    //             if (updatedGold == 0)
    //             {
    //                 trx.Rollback();
    //                 return ErrorCode.NotFoundError;
    //             }
    //         }

    //         // gem 업데이트
    //         if (gemSum > 0)
    //         {
    //             int updatedGem = await _queryFactory.Query("user_currency")
    //                 .Where("account_id", accountId)
    //                 .IncrementAsync("gem", gemSum, trx);

    //             if (updatedGem == 0)
    //             {
    //                 trx.Rollback();
    //                 return ErrorCode.NotFoundError;
    //             }
    //         }

    //         // 3. item 업데이트
    //         foreach (var item in groupedItems)
    //         {
    //             int updatedRows = await _queryFactory.Query("user_inventory")
    //                 .Where("account_id", accountId)
    //                 .Where("item_id", item.item_id)
    //                 .IncrementAsync("quantity", item.quantity, trx);

    //             if (updatedRows == 0)
    //             {
    //                 await _queryFactory.Query("user_inventory")
    //                     .InsertAsync(new { account_id = accountId, item_id = item.item_id, quantity = item.quantity }, trx);
    //             }
    //         }

    //         // 4. 트랜잭션 커밋
    //         trx.Commit();
    //         return ErrorCode.None;
    //     }
    //     catch (Exception ex)
    //     {
    //         // 5. 트랜잭션 롤백 및 에러 로깅
    //         trx.Rollback();
    //         _logger.LogError(ex, "Reward batch update failed for accountId: {AccountId}", accountId);
    //         return ErrorCode.MailReceiveRewardsFailException;
    //     }
    // }

}

