namespace GameAPIServer.Repository;

public class MemoryDbKeyMaker
{
    const string loginToken = "Token_";
    const string userLockKey = "ULock_";

    public static string TokenKey(string token)
    {
        return loginToken + token;
    }

    public static string UserLockKey(string id)
    {
        return userLockKey + id;
    }
}