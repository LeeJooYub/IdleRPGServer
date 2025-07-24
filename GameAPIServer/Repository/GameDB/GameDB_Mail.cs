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
using System.Linq;


namespace GameAPIServer.Repository;

public partial class GameDb : IGameDb
{

    // 메일 관련 데이터베이스 작업을 위한 메서드들을 여기에 정의합니다.
    // 예: 메일 목록 조회, 메일 상세 조회, 메일 보상 수령, 메일 삭제 등

    // 예시: 메일 목록 조회
    public async Task<List<Mail>> GetMailListAsync(int accountId, int pageNumber, int pageSize)
    {
        int offset = (pageNumber - 1) * pageSize;
        SqlKata.Query query = _queryFactory.Query("mail")
            .Where("account_id", accountId)
            .OrderByDesc("created_at")
            .Limit(pageSize)
            .Offset(offset);
            
        List<Mail> mails = (await query.GetAsync<Mail>()).ToList();

        return mails;
    }

    public async Task<int> GetUnreadMailCountAsync(int accountId)
    {
        SqlKata.Query query = _queryFactory.Query("mail")
            .Where("account_id", accountId)
            .Where("is_read", false);

        int unreadCount = (await query.CountAsync<int>());

        return unreadCount;
    }

    public async Task<int> GetTotalMailCountAsync(int accountId)
    {
       SqlKata.Query query = _queryFactory.Query("mail")
            .Where("account_id", accountId);

        int totalCount = (await query.CountAsync<int>());

        return totalCount;
    }

}

