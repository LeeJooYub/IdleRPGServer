using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameAPIServer.DTO.Service;
using GameAPIServer.Repository.Interfaces;
using GameAPIServer.Services.Interfaces;
using Microsoft.Extensions.Logging;

using GameAPIServer.Models.GameDB;


namespace GameAPIServer.Services
{
    public class MissionService : IMissionService
    {
        private readonly IMasterDb _masterDb;
        private readonly IGameDb _gameDb;
        private readonly ILogger<MissionService> _logger;

        public MissionService(IMasterDb masterDb, IGameDb gameDb, ILogger<MissionService> logger)
        {
            _masterDb = masterDb;
            _gameDb = gameDb;
            _logger = logger;
        }


        public async Task<ClearGuideMissionOutput> ClearGuideMissionAsync(ClearGuideMissionInput input)
        {
            ClearGuideMissionOutput output = new ClearGuideMissionOutput();
            try
            {

                // gameDb에서 유저의 진행상태를 조회
                var UserCharacterProgress = await _gameDb.GetUserCharacterProgressAsync(input.PlayerUid);

                // 이미 진행한 가이드 미션인지 확인
                if (UserCharacterProgress.current_guide_mission_seq != input.GuideMissionSeq)
                {
                    _logger.LogError("User character progress not found for PlayerUid: {PlayerUid}, GuideMissionSeq: {GuideMissionSeq}", input.PlayerUid, input.GuideMissionSeq);
                    output.ErrorCode = ErrorCode.GuideMissionNotYetReachableOrAlreadyCleared; // 에러 코드 설정
                    return output;
                }

                // 해당 가이드 미션을 조회
                var guideMission = await _masterDb.GetGuideMissionAsync(input.GuideMissionSeq);
                if (guideMission == null)
                {
                    output.ErrorCode = ErrorCode.GuideMissionNotFound;
                    return output;
                }
                var guideMissionReward = new Models.RewardData
                {
                    reward_id = guideMission.reward_id,
                    reward_type_cd = guideMission.reward_type_cd,
                    reward_qty = guideMission.reward_qty
                };

                var nextGuideMission = await _masterDb.GetGuideMissionAsync(input.GuideMissionSeq + 1);
                output.NextGuideMission = nextGuideMission;

                // gameDb에서 현재 진행중인 가이드 미션 상태를 업데이트
                output.ErrorCode = await _gameDb.UpdateUserCharacterGuideMissionProgressAsync(input.PlayerUid, input.GuideMissionSeq + 1);

                // 해당 가이드 미션 클리어 리워드를 유저에게 지급 (업데이트)
                output.ErrorCode = await _gameDb.UpdateUserFromRewardAsync(input.PlayerUid, guideMissionReward);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing guide mission");
                if(output.ErrorCode == ErrorCode.None)
                {
                    output.ErrorCode = ErrorCode.DatabaseError; // 에러 코드 설정
                }
            }

            // 비즈니스 로직 처리 및 레포지토리 호출
            return output;
        }
    }
}
