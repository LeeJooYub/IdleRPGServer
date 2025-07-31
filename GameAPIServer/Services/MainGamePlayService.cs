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
    public class MainGamePlayService : IMainGamePlayService
    {
        private readonly IMasterDb _masterDb;
        private readonly IGameDb _gameDb;
        private readonly ILogger<MainGamePlayService> _logger;

        public MainGamePlayService(IMasterDb masterDb, IGameDb gameDb, ILogger<MainGamePlayService> logger)
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
                // gameDb에서 유저 현재 스테이지 상태를 업데이트
                output.ErrorCode = await _gameDb.UpdateUserCharacterStageProgressAsync(input.PlayerUid, input.StageId+1);


                // 해당 스테이지 클리어 상품을 조회
                var stageClearReward = await _masterDb.GetStageClearRewardAsync(input.StageId);


                // 해당 스테이지 클리어 리워드를 유저에게 지급 (업데이트)
                output.ErrorCode = await _gameDb.UpdateUserFromRewardAsync(input.PlayerUid, stageClearReward);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing stage");
                if(output.ErrorCode == ErrorCode.None)
                {
                    output.ErrorCode = ErrorCode.DatabaseError; // 에러 코드 설정
                }
                return output;
            }

            return output;
        }

        public async Task<ClearGuideMissionOutput> ClearGuideMissionAsync(ClearGuideMissionInput input)
        {
            ClearGuideMissionOutput output = new ClearGuideMissionOutput();
            try
            {
                // gameDb에서 현재 진행중인 가이드 미션 상태를 업데이트
                await _gameDb.UpdateUserCharacterGuideMissionProgressAsync(input.PlayerUid, input.GuideMissionSeq+1);

                // 해당 가이드 미션 클리어 상품을 조회
                var guideMissionReward = await _masterDb.GetGuideMissionRewardAsync(input.GuideMissionSeq);

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
