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


    // Mail related methods
    public Task<Mail> GetMailAsync(Int64 mailId);
    public Task<(List<Mail>, DateTime?)> GetMailListAsync(Int64 accountId, DateTime? cursor, int limit);
    public Task<int> DeleteMailAsync(Int64 mailId);
    public Task<Mail> GetMailRewardAsync(Int64 mailId);
    public Task<List<Mail>> GetAllMailsRewardAsync(Int64 accountUid);
    public Task<int> UpdateMailRewardStatusAsync(Int64 mailId);
    public Task<int> UpdateAllMailRewardStatusAsync(Int64 account_id, DateTime? now);

    // Attendance related methods
    public Task<Attendance> GetAttendanceBookAsync(Int64 accountUid, Int64 attendanceBookId);
    public Task<int> InsertAttendanceBookAsync(Attendance attendance);
    public Task<bool> CheckInAttendanceBookAsync(Int64 accountUid, Int64 attendanceBookId);

    // User data load methods
    public Task<List<UserCurrency>> GetUserCurrencyListAsync(Int64 accountUid);
    public Task<List<UserInventoryItem>> GetUserInventoryItemListAsync(Int64 accountUid);
    public Task<UserCurrency> GetUserCurrencyAsync(Int64 accountUid, int currencyId);
    public Task<UserInventoryItem> GetUserInventoryItemAsync(Int64 accountUid, int itemId);


    // user data update methods
    public Task<bool> UpdateUserCurrencyAsync(Int64 accountUid, int currencyId, int deltaAmount);
    public Task<bool> UpdateUserInventoryItemAsync(Int64 accountUid, int itemId, int deltaAmount);
    public Task<ErrorCode> UpdateUserFromRewardAsync(Int64 accountUid, RewardData reward);




    //public Task<GetUserCharacterInfoResult> GetUserCharacterInfoAsync(Int64 accountId);
    //public IDbConnection GDbConnection();
}
