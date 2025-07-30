using System;

// 1000 ~ 19999
public enum ErrorCode : UInt16
{
    None = 0,

    // Common 1000 ~
    TokenDoesNotExist = 1004,
    UidDoesNotExist = 1005,
    AuthTokenFailWrongAuthToken = 1006,
    Hive_Fail_InvalidResponse = 1010,
    InValidAppVersion = 1011,
    InvalidMasterDataVersion = 1012,

    // Auth 2000 ~
    CreateUserFailException = 2001,
    LoginFailUserNotExist = 2005,
    AuthTokenKeyNotFound = 2011,
    AuthTokenFailSetNx = 2013,
    LoginFailAddRedis = 2017,
    LogoutRedisDelFailException= 2022,
    
    // Friend 2100
    

    // Game 2200
    

    // Item 3000 ~



    //GameDb 4000~ 
  

    // MasterDb 5000 ~
    

    // User

    UserMoneyInfoFailException = 6002,
    UserInventoryFailException = 6003,

    InvetoryEmptyFailException = 6004, // 인벤토리 비어있음

    //User Currency & Item

    NoSuchCurrency = 7005, // 화폐가 존재하지 않음
    ItemInsertFailed = 7006,

    // Mail

    MailReceiveFailAlreadyReceived = 8003,
    MailNotFound = 8004, // 메일이 존재하지 않는 경우
    MailReceiveFailNotFound = 8005, // 메일 수령 실패: 메
    RewardNotExist = 8013, // 보상이 존재하지 않는 경우
    MailAlreadyClaimed = 8014, // 메일이 이미 수령된 경우

    // Reward
    InvalidRewardType = 9001,

    // Attendance
    GameDbGetAttendanceBookError = 10001, // 출석부 조회 실패
    MasterDbGetAttendanceBookError = 10002, // 출석부 조회 실패
    MasterDbGetRewardInfoInAttendanceBookError = 10003, // 출석부 리워드 정보 조회 실패
    GameDbCheckInAttendanceBookError = 10004, // 출석 체크 실패
    GameDbUpdateUserFromRewardError = 10005, // 리워드 업데이트 실패
    GameDbAlreadyCheckInAttendanceError = 10006, // 이미 출석한 경우
    GameDbTryToCheckInFutureError = 10007, // 미래의 출석
    GameDbRefreshTimeNotReached = 10008, // 출석 갱신 시간에 도달하지 않음

    // Basic Errors 100 ~
    DatabaseError = 101, // Generic database error
    NotFoundError = 104, // Resource not found

    // Basic Errors 200 ~
    ValidationError = 203, // Input validation error

    // Basic Errors 300 ~
    
}