using System;

// 1000 ~ 19999
public enum ErrorCode : UInt16
{
    // ====== 기본 ======
    None = 0,

    // ====== 공통(Common) 100~199 ======
    TokenDoesNotExist = 100,
    UidDoesNotExist = 101,
    AuthTokenFailWrongAuthToken = 102,
    Hive_Fail_InvalidResponse = 103,
    InValidAppVersion = 104,
    InvalidMasterDataVersion = 105,
    DatabaseError = 110,
    NotFoundError = 120,

    // ====== 인증(Auth) 200~299 ======
    CreateUserFailException = 200,
    LoginFailUserNotExist = 201,
    AuthTokenKeyNotFound = 202,
    AuthTokenFailSetNx = 203,
    LoginFailAddRedis = 204,
    LogoutRedisDelFailException = 205,
    ValidationError = 210,

    // ====== 화폐/아이템(Currency/Item) 300~ ======
    NoSuchCurrency = 300,
    ItemInsertFailed = 301,

    // ====== 메일(Mail) 400~499 ======
    MailReceiveFailAlreadyReceived = 400,
    MailNotFound = 401,
    MailReceiveFailNotFound = 402,
    RewardNotExist = 403,
    MailAlreadyClaimed = 404,

    // ====== 출석부(Attendance) 500~599 ======
    GameDbGetAttendanceBookError = 500,
    MasterDbGetAttendanceBookError = 501,
    MasterDbGetRewardInfoInAttendanceBookError = 502,
    GameDbCheckInAttendanceBookError = 503,
    GameDbUpdateUserFromRewardError = 504,
    GameDbAlreadyCheckInAttendanceError = 505,
    GameDbTryToCheckInFutureError = 506,
    GameDbRefreshTimeNotReached = 507,

    // ====== 스테이지(Stage) 600~699 ======
    StageNotFound = 600,
    StageClearRewardNotFound = 601,
    StageClearUpdateFailed = 602,
    StageClearRewardUpdateFailed = 603,
    StageAlreadyCleared = 604,
    StageNotCleared = 605,
    StageAlreadyInProgress = 606,
    StageNotInProgress = 607,
    StageClearFailed = 608,
    StageNotYetReachableOrCleared = 609,

    // ====== 미션(Mission) 700~799 ======
    GuideMissionNotFound = 700,
    GuideMissionNotYetReachableOrAlreadyCleared = 701,

    // ====== 캐릭터(Character) 700~799 ======




    // ====== 리워드(Reward) 1000~1199 ======
    InvalidRewardType = 1000,

    // ====== 기타(Reserved) ======
    // (추가 에러코드 작성)
}