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
        public async Task<ErrorCode> UpdateUserFromRewardAsync(Int64 PlayerUid, RewardData reward)
        {

            if (reward.reward_type_cd == "01") // 화폐 리워드
            {
                var affectedRows = await _queryFactory.Query("user_currency")
                    .Where("player_uid", PlayerUid)
                    .Where("currency_id", reward.reward_id.Value)
                    .IncrementAsync("amount", reward.reward_qty.Value);

                if (affectedRows == 0)
                    return ErrorCode.NoSuchCurrency; // 화폐가 존재하지 않음
            }
            else if (reward.reward_type_cd == "02") // 아이템 리워드
            {
                var affectedRows = await _queryFactory.Query("user_inventory")
                    .Where("player_uid", PlayerUid)
                    .Where("item_id", reward.reward_id.Value)
                    .IncrementAsync("item_qty", reward.reward_qty.Value);

                if (affectedRows == 0)
                {
                    var insertRows = await _queryFactory.Query("user_inventory")
                        .InsertAsync(new
                        {
                            player_uid = PlayerUid,
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
    }
}