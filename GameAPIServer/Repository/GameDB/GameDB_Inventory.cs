using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using SqlKata.Execution;


using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Models.GameDB;
using System.Linq;



namespace GameAPIServer.Repository;

public partial class GameDb : IGameDb
{
    public async Task<List<UserInventoryItem>> GetUserInventoryItemListAsync(Int64 accountId)
    {
        var query = _queryFactory.Query("user_inventory")
            .Where("account_uid", accountId);
        var userInventory = await query.GetAsync<UserInventoryItem>();
        var userInventoryList = userInventory.ToList();

        return userInventoryList;
    }

    public async Task<UserInventoryItem> GetUserInventoryItemAsync(Int64 accountId, int itemId)
    {
        var query = _queryFactory.Query("user_inventory")
            .Where("account_uid", accountId)
            .Where("item_id", itemId)
            .FirstOrDefaultAsync<UserInventoryItem>();
        var userInventoryItem = await query;

        return userInventoryItem;
    }


    
    public async Task<bool> UpdateUserInventoryItemAsync(Int64 accountId, int itemId, int deltaAmount)
    {
        var query = _queryFactory.Query("user_inventory")
            .Where("account_uid", accountId)
            .Where("item_id", itemId);

        var result = await query.FirstOrDefaultAsync<UserInventoryItem>();
        if(result == null)
        {
            // 아이템이 존재하지 않는 경우, 새로 추가
            var newItem = new UserInventoryItem
            {
                account_uid = accountId,
                item_id = itemId,
                item_qty = deltaAmount,
                acquire_dt = DateTime.UtcNow,
                last_update_dt = DateTime.UtcNow
            };
            await _queryFactory.Query("user_inventory").InsertAsync(newItem);
            return true;
        }
        else if (result.item_qty + deltaAmount < 0)
        {
            // 아이템 수량이 음수가 되는 경우, 업데이트 실패
            return false;
        }
        else if (result.item_qty + deltaAmount == 0)
        {
            // 아이템 수량이 0이 되는 경우, 아이템 삭제
            await query.DeleteAsync();
            return true;
        }
        else
        {
            // 아이템 수량 업데이트
            result.item_qty += deltaAmount;
            result.last_update_dt = DateTime.UtcNow;
            await query.UpdateAsync(result);
            return true;
        }
    }   

}
