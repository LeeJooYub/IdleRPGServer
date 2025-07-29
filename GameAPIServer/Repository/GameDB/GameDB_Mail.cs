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
using GameAPIServer.Models.MasterDB;
using System.Linq;


namespace GameAPIServer.Repository;

public partial class GameDb : IGameDb
{

    public async Task<Mail> GetMailAsync(Int64 mailId)
    {
        // 특정 mail_id에 대한 메일 정보를 가져오기
        var mail = await _queryFactory.Query("mail")
            .Where("mail_id", mailId)
            .FirstOrDefaultAsync<Mail>();

        return mail;
    }

    // 메일 리스트 가져오기 (커서 사용)
    public async Task<(List<Mail>, DateTime?)> GetMailListAsync(Int64 accountId, DateTime? cursor, int limit)
    {
        // 쿼리 작성: 특정 계정의 메일을 가져오고, receive_dt 기준으로 정렬
        var query = _queryFactory.Query("mail")
            .Where("account_uid", accountId)
            .Where("reward_receive_yn", "!=", "Y") // receive_yn이 'Y'가 아닌 상태
            .OrderByDesc("create_dt") // create_dt 기준으로 내림차순 정렬
            .Limit(limit);

        // 쿼리 실행 및 결과 반환
        var mails = (await query.GetAsync<Mail>()).ToList();
        DateTime? nextCursor = null;

        if (mails.Count > 0)
        {
            nextCursor = mails.Last().receive_dt; // 마지막 메일의 receive_dt를 커서로 사용
        }

        return (mails, nextCursor);
    }


    // 메일 삭제: 특정 mail_id의 메일을 삭제
    public async Task<int> DeleteMailAsync(Int64 mailId)
    {
        var query = _queryFactory.Query("mail").Where("mail_id", mailId);
        var affectedRows = await query.DeleteAsync();

        return affectedRows;
    }

    // 메일 보상 조회: 특정 mail 가져오기
    public async Task<Mail> GetMailRewardAsync(Int64 mailId)
    {
        // 특정 mail_id에 대한 보상 정보를 가져오기
        var mail = (await _queryFactory.Query("mail")
            .Where("mail_id", mailId)
            .FirstOrDefaultAsync<Mail>());

        return mail;
    }

    public async Task<List<Mail>> GetAllMailsRewardAsync(Int64 accountUid)
    {
        // 특정 accountUid에 대한 모든 메일 정보를 가져오기
        var mails = (await _queryFactory.Query("mail")
            .Where("account_uid", accountUid)
            .GetAsync<Mail>()).ToList();

        return mails;
    }


    // 메일 상태 업데이트: 특정 mail_id의 보상 상태를 업데이트
    public async Task<int> UpdateMailRewardStatusAsync(Int64 mailId)
    {
        // 메일의 보상을 추출하고 사용자에게 지급
        var affectedRows = await _queryFactory.Query("mail")
            .Where("mail_id", mailId)
            .UpdateAsync(new { reward_receive_yn = 'Y', receive_dt = DateTime.UtcNow });

        // 사용자에게 보상 지급
        return affectedRows;
    }


    public async Task<int> UpdateAllMailRewardStatusAsync(Int64 account_id , DateTime? now)
    {
        var affectedRows = await _queryFactory.Query("mail")
            .Where("account_uid", account_id)
            .Where("expire_dt", ">=", now)
            .UpdateAsync(new { reward_receive_yn = 'Y', receive_dt = DateTime.UtcNow });

        return affectedRows;
    }
 

}

