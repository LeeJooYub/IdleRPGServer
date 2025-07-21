using GameAPIServer.Models.GameDB;
using System;
using System.Threading.Tasks;

namespace GameAPIServer.Repository.Interfaces;

public interface IGameDb
{
    public Task<GdbUserInfo> GetUserByPlayerId(Int64 playerId);
}