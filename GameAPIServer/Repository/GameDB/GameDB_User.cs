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

    public async Task<(ErrorCode, Int64)> FindUserByPlatformId(Int64 platformId)
    {
        AccountInfo? result = await _queryFactory.Query("user")
            .Where("platform_id", platformId)
            .FirstOrDefaultAsync<AccountInfo>(); // ★ 제네릭 타입 명시!

        if (result == null)
        {
            return (ErrorCode.LoginFailUserNotExist, 0);
        }

        return (ErrorCode.None, result.account_id);
    }

    public async Task<(ErrorCode, Int64)> CreateUser(AccountInfo userInfo)
    {
        Int64 result = await _queryFactory.Query("user").InsertAsync(userInfo);
        if (result == 0)
        {
            return (ErrorCode.CreateUserFailException, 0);
        }

        return (ErrorCode.None, result);
    }


}
