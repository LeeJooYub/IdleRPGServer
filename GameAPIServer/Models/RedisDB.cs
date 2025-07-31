using System;

namespace GameAPIServer.Models;

//RedisDB의 객체는 객체 이름 앞에 Rdb를 붙인다.

public class RdbAuthUserData
{
    public Int64 PlayerUid { get; set; } = 0;
    public string TokenKey { get; set; } = "";
}
