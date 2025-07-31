using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameAPIServer.DTO.Service;
using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Services.Interfaces;
using Microsoft.Extensions.Logging;

using GameAPIServer.Models.GameDB;
using GameAPIServer.Models.MasterDB;


namespace GameAPIServer.Services
{
    public class StageService : IStageService
    {
        private readonly IMasterDb _masterDb;
        private readonly IGameDb _gameDb;
        private readonly ILogger<StageService> _logger;

        public StageService(IMasterDb masterDb, IGameDb gameDb, ILogger<StageService> logger)
        {
            _masterDb = masterDb;
            _gameDb = gameDb;
            _logger = logger;
        }

        public async Task<ClearStageOutput> ClearStageAsync(ClearStageInput input)
        {
            // 비즈니스 로직 처리 및 레포지토리 호출
            ClearStageOutput output = new ClearStageOutput();
            try
            {
                // gameDb에서 유저의 진행상태를 조회
                var UserCharacterProgress = await _gameDb.GetUserCharacterProgressAsync(input.PlayerUid);

                // 이미 진행한 스테이지인지 확인
                if (UserCharacterProgress.current_stage_id != input.StageId)
                {
                    _logger.LogError("User character progress not found for PlayerUid: {PlayerUid}, StageId: {StageId}", input.PlayerUid, input.StageId);
                    output.ErrorCode = ErrorCode.StageNotYetReachableOrCleared; // 에러 코드 설정
                    return output;
                }

                // 해당 스테이지 클리어 상품을 조회
                var curr_stage = await _masterDb.GetStageAsync(input.StageId);
                var stageClearReward = new Models.RewardData
                {
                    reward_id = curr_stage.reward_id,
                    reward_type_cd = curr_stage.reward_type_cd,
                    reward_qty = curr_stage.reward_qty
                };
                var nextstage = await GetNextStageAsync(input.StageId);
                output.Reward = stageClearReward;
                output.ClearedStage = curr_stage;
                output.NextStage = nextstage;

                // gameDb에서 유저 현재 스테이지 상태를 업데이트
                output.ErrorCode = await _gameDb.UpdateUserCharacterStageProgressAsync(input.PlayerUid, input.StageId + 1);

                // 해당 스테이지 클리어 리워드를 유저에게 지급 (업데이트)
                output.ErrorCode = await _gameDb.UpdateUserFromRewardAsync(input.PlayerUid, stageClearReward);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing stage");
                if (output.ErrorCode == ErrorCode.None)
                {
                    output.ErrorCode = ErrorCode.DatabaseError; // 에러 코드 설정
                }
                return output;
            }

            return output;
        }
        
        public async Task<Stage> GetNextStageAsync(int currentStageId)
        {
            // 1. 우선 다음 번호로 시도
            int nextStageId = currentStageId + 1;
            var nextStage = await _masterDb.GetStageAsync(nextStageId);
            if (nextStage != null)
                return nextStage;

            // 2. 다음 챕터의 첫 스테이지로 시도
            int chapter = currentStageId / 100;
            int nextChapterFirstStageId = (chapter + 1) * 100 + 1;
            nextStage = await _masterDb.GetStageAsync(nextChapterFirstStageId);
            if (nextStage != null)
                return nextStage;

            // 3. 더 이상 없음 (마지막 스테이지)
            return null;
        }
    }
}
