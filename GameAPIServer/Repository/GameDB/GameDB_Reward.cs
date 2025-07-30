using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SqlKata.Execution;

using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;
using GameAPIServer.Models;

using GameAPIServer.Repository.Interfaces;

namespace GameAPIServer.Repository
{
    public partial class GameDb : IGameDb
    {
        // 리워드 처리 함수 (ErrorCode 반환)
        public async Task<ErrorCode> UpdateUserFromRewardAsync(Int64 accountUid, RewardData reward)
        {
            try
            {
                if (reward.reward_type_cd == "01") // 화폐 리워드
                {
                    var affectedRows = await _queryFactory.Query("user_currency")
                        .Where("account_uid", accountUid)
                        .Where("currency_cd", reward.reward_id.Value)
                        .IncrementAsync("amount", reward.reward_qty.Value);

                    if (affectedRows == 0)
                        return ErrorCode.NoSuchCurrency; // 화폐가 존재하지 않음
                }
                else if (reward.reward_type_cd == "02") // 아이템 리워드
                {
                    var affectedRows = await _queryFactory.Query("user_inventory")
                        .Where("account_uid", accountUid)
                        .Where("item_id", reward.reward_id.Value)
                        .IncrementAsync("item_qty", reward.reward_qty.Value);

                    if (affectedRows == 0)
                    {
                        var insertRows = await _queryFactory.Query("user_inventory")
                            .InsertAsync(new
                            {
                                account_uid = accountUid,
                                item_id = reward.reward_id.Value,
                                item_qty = reward.reward_qty.Value,
                                acquire_dt = DateTime.UtcNow,
                                last_update_dt = DateTime.UtcNow
                            });
                        if (insertRows == 0)
                            return ErrorCode.ItemInsertFailed; // 아이템 추가 실패
                    }
                }
                else
                {
                    return ErrorCode.InvalidRewardType;
                }
                return ErrorCode.None;
            }
            catch (Exception ex)
            {
                // 예외 발생 시 에러코드 반환 (로그는 상위에서 처리)
                return ErrorCode.DatabaseError;
            }
        }
    }
}