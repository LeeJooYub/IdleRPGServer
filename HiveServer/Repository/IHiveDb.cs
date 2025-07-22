using System;
using System.Threading.Tasks;

using HiveServer.Model.Entity;

namespace HiveServer.Repository.Interfaces
{
    public interface IHiveDb : IDisposable
    {
        Task<int> InsertAccountAsync(AccountInfo accountInfo);
        Task<AccountInfo?> GetAccountByEmailAsync(string email);



        // Task<int> InsertCategoryAsync(TestCategory category);

        // Task<TestCategory> GetCategoryByIdNoAliasAsync(int id);
    }
}
