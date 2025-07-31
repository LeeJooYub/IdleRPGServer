using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using GameAPIServer.Models.GameDB;
using GameAPIServer.DTO.Controller;
using GameAPIServer.Models;


namespace GameAPIServer.Repository.Interfaces;

public interface IGameDb
{
    // User related methods
    public Task<User> FindUserByAccountId(Int64 AccountId);
    public Task<int> CreateUser(User userInfo);

    // CharacterStatus related methods
    public Task<ErrorCode> UpdateUserCharacterStatusAsync(Int64 PlayerUid, UserCharacterStatus status);


    // CharacterProgress related methods
    public Task<ErrorCode> UpdateUserCharacterStageProgressAsync(Int64 PlayerUid, int stageId);
    public Task<ErrorCode> UpdateUserCharacterGuideMissionProgressAsync(Int64 PlayerUid, int guideMissionSeq);


    // user data update methods
    public Task<bool> UpdateUserCurrencyAsync(Int64 PlayerUid, int currencyId, int deltaAmount);
    public Task<bool> UpdateUserInventoryItemAsync(Int64 PlayerUid, int itemId, int deltaAmount);
    public Task<ErrorCode> UpdateUserFromRewardAsync(Int64 PlayerUid, RewardData reward);


    // Mail related methods
    public Task<Mail> GetMailAsync(Int64 mailId);
    public Task<(List<Mail>, DateTime?)> GetMailListAsync(Int64 accountId, DateTime? cursor, int limit);
    public Task<int> DeleteMailAsync(Int64 mailId);
    public Task<Mail> GetMailRewardAsync(Int64 mailId);
    public Task<List<Mail>> GetAllMailsRewardAsync(Int64 PlayerUid);
    public Task<int> UpdateMailRewardStatusAsync(Int64 mailId);
    public Task<int> UpdateAllMailRewardStatusAsync(Int64 account_id, DateTime? now);

    // Attendance related methods
    public Task<Attendance> GetAttendanceBookAsync(Int64 PlayerUid, Int64 attendanceBookId);
    public Task<int> InsertAttendanceBookAsync(Attendance attendance);
    public Task<bool> CheckInAttendanceBookAsync(Int64 PlayerUid, Int64 attendanceBookId);

    // User data load methods
    public Task<List<UserCurrency>> GetUserCurrencyListAsync(Int64 PlayerUid);
    public Task<List<UserInventoryItem>> GetUserInventoryItemListAsync(Int64 PlayerUid);
    public Task<UserCurrency> GetUserCurrencyAsync(Int64 PlayerUid, int currencyId);
    public Task<UserInventoryItem> GetUserInventoryItemAsync(Int64 PlayerUid, int itemId);




    //public Task<GetUserCharacterStatusInfoResult> GetUserCharacterStatusInfoAsync(Int64 accountId);
    //public IDbConnection GDbConnection();
}
