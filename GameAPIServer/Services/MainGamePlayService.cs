using System.Threading.Tasks;
using GameAPIServer.DTO.ServiceDTO;
using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace GameAPIServer.Services
{
    public class MainGamePlayService : IMainGamePlayService
    {
        private readonly IMainGamePlayRepository _mainGamePlayRepository;
        private readonly ILogger<MainGamePlayService> _logger;

        public MainGamePlayService(IMainGamePlayRepository mainGamePlayRepository, ILogger<MainGamePlayService> logger)
        {
            _mainGamePlayRepository = mainGamePlayRepository;
            _logger = logger;
        }

        public async Task<StageClearOutput> StageClear(StageClearInput input)
        {
            // 비즈니스 로직 처리 및 레포지토리 호출
            return await _mainGamePlayRepository.StageClearAsync(input);
        }

        public async Task<ErrorCode> ClearGuideMission(ClearGuideMissionInput input)
        {
            // 비즈니스 로직 처리 및 레포지토리 호출
            return await _mainGamePlayRepository.ClearGuideMissionAsync(input);
        }
    }
}
