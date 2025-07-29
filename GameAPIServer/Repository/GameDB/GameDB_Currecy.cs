using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using SqlKata.Execution;


using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Models.GameDB;
using System.Linq;

namespace GameAPIServer.Repository
{
    public partial class GameDb : IGameDb
    {

        public async Task<List<UserCurrency>> GetUserCurrencyListAsync(Int64 accountId)
        {
            var query = _queryFactory.Query("user_currency")
                .Where("account_uid", accountId);
            var userCurrency = await query.GetAsync<UserCurrency>();
            var userCurrencyList = userCurrency.ToList();

            return userCurrencyList;
        }



        public async Task<UserCurrency> GetUserCurrencyAsync(Int64 accountId, int currencyId)
        {
            var query = _queryFactory.Query("user_currency")
                .Where("account_uid", accountId)
                .Where("currency_id", currencyId)
                .FirstOrDefaultAsync<UserCurrency>();
            var userCurrency = await query;

            return userCurrency;
        }
        public async Task<bool> UpdateUserCurrencyAsync(Int64 accountId, int currencyId, int deltaAmount)
        {
            var query = _queryFactory.Query("user_currency")
                .Where("account_uid", accountId)
                .Where("currency_cd", currencyId);

            var affectedRows = await query.IncrementAsync("amount", deltaAmount);
            var result = affectedRows > 0;

            return result;
        }   
        
    }
}