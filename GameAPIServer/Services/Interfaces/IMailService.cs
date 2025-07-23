using System.Collections.Generic;
using System.Threading.Tasks;
using GameAPIServer.DTO.ServiceDTO;

namespace GameAPIServer.Services.Interfaces
{
    public interface IMailService
    {
        Task<MailListResult> GetMailListAsync(MailListCommand command);
        Task<MailDetailResult> GetMailDetailAsync(MailDetailCommand command);
        Task<ClaimMailResult> ClaimMailRewardAsync(ClaimMailCommand command);
        Task<DeleteMailResult> DeleteMailAsync(DeleteMailCommand command);
        Task<ClaimAllMailsResult> ClaimAllMailRewardsAsync(ClaimAllMailsCommand command);
    }
}
