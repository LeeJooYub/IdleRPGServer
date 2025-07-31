using System.Collections.Generic;
using System.Threading.Tasks;
using GameAPIServer.DTO.Service;
using GameAPIServer.DTO.Controller;

namespace GameAPIServer.Services.Interfaces
{
    public interface IMailService
    {
        Task<MailListOutput> GetMailListAsync(MailListInput input); // Updated to include error code handling
        Task<ReceiveMailOutput> ReceiveMailRewardAsync(ReceiveMailInput input); // Updated to include error handling
        Task<ReceiveAllMailOutput> ReceiveAllMailRewardAsync(ReceiveAllMailInput input); // Updated to include error handling
        Task<DeleteMailOutput> DeleteMailAsync(DeleteMailInput input);

        //TODO : 전체 수령
        //Task<ClaimAllMailsResult> ClaimAllMailRewardsAsync(ClaimAllMailsCommand command);

    }
}
