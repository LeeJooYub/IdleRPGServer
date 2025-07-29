using System.Collections.Generic;
using System.Threading.Tasks;
using GameAPIServer.DTO.ServiceDTO;
using GameAPIServer.DTO.ControllerDTO;

namespace GameAPIServer.Services.Interfaces
{
    public interface IMailService
    {
        Task<MailListOutput> GetMailListAsync(MailListInput input); // Updated to include error code handling
        Task<ClaimMailOutput> ClaimMailRewardAsync(ClaimMailInput input); // Updated to include error handling
        Task<DeleteMailOutput> DeleteMailAsync(DeleteMailInput input);

        //TODO : 전체 수령
        //Task<ClaimAllMailsResult> ClaimAllMailRewardsAsync(ClaimAllMailsCommand command);

    }
}
