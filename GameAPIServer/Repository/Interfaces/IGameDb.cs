using System;
using System.Threading.Tasks;

using GameAPIServer.Models.GameDB;

namespace GameAPIServer.Repository.Interfaces;

public interface IGameDb
{

    public Task<(ErrorCode, Int64)> CreateUser(AccountInfo userInfo);

    public Task<(ErrorCode, Int64)> FindUserByPlatformId(Int64 platformId);
}