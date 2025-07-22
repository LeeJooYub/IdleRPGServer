using System;
using System.Threading.Tasks;

using GameAPIServer.Models.GameDB;

namespace GameAPIServer.Repository.Interfaces;

public interface IGameDb
{
    public Task<GdbUserInfo> GetUserByPlayerId(Int64 playerId);
}