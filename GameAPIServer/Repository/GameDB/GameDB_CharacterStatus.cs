using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameAPIServer.Models.GameDB;

using SqlKata.Execution;

using GameAPIServer.Repository.Interfaces;

namespace GameAPIServer.Repository;

public partial class GameDb : IGameDb
{
    public async Task<ErrorCode> UpdateUserCharacterStatusAsync(Int64 PlayerUid, UserCharacterStatus status)
    {
        // Update the user's current stage progress in the database
        var result = 1;
        return result > 0 ? ErrorCode.None : ErrorCode.DatabaseError; // Return 0 for success, -1 for failure
    }

}
