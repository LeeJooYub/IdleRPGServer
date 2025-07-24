using System.Collections.Generic;
using System.Threading.Tasks;
using GameAPIServer.DTO.ServiceDTO;
using GameAPIServer.DTO.ControllerDTO;

namespace GameAPIServer.Services.Interfaces
{
    public interface IMailService
    {
        Task<MailListResult> GetMailListAsync(MailListCommand command); // Updated to include error code handling
        Task<ClaimMailResult> ClaimMailRewardAsync(ClaimMailCommand command); // Updated to include error handling
        //TODO : Task<DeleteMailResult> DeleteMailAsync(DeleteMailCommand command);
        Task<ClaimAllMailsResult> ClaimAllMailRewardsAsync(ClaimAllMailsCommand command);

    }
}
