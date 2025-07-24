using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using GameAPIServer.Models.GameDB;

namespace GameAPIServer.Repository.Interfaces;

public interface IGameDb
{
    public Task<(ErrorCode, Int64)> CreateUser(AccountInfo userInfo);
    public Task<(ErrorCode, Int64)> FindUserByPlatformId(Int64 platformId);



    public Task<List<Mail>> GetMailListAsync(int accountId, int pageNumber, int pageSize);
    public Task<int> GetUnreadMailCountAsync(int accountId);
    public Task<int> GetTotalMailCountAsync(int accountId);


    //public IDbConnection GDbConnection();
}