using System.Threading.Tasks;
using HiveServer.Model.DTO;

namespace HiveServer.Services.Interfaces
{
    public interface ICreateHiveAccountService
    {
        Task<ErrorCode> CreateAccountAsync(CreateHiveAccountCommand command);


    }
}