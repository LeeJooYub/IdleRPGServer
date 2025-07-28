using System;

namespace GameAPIServer.Models;

//RedisDB의 객체는 객체 이름 앞에 Rdb를 붙인다.

public class RdbAuthUserData
{
    public Int64 AccountId { get; set; } = 0;
    public string SessionKey { get; set; } = "";
}
