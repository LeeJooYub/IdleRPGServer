using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MySqlConnector;
using SqlKata.Execution;

using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Models.GameDB;


namespace GameAPIServer.Repository;

public partial class GameDb : IGameDb
{

    public async Task<AccountInfo> FindUserByAccountId(Int64 AccountId)
    {
        var user = await _queryFactory.Query("user")
            .Where("account_uid", AccountId)
            .FirstOrDefaultAsync<AccountInfo>(); // ★ 제네릭 타입 명시!

        return user;
    }

    public async Task<int> CreateUser(AccountInfo userInfo)
    {
        var result = await _queryFactory.Query("user").InsertAsync(userInfo);

        return result;
    }

}
