namespace GameAPIServer.Repository;

public class MemoryDbKeyMaker
{
    const string loginUID = "UID_";
    const string userLockKey = "ULock_";

    public static string UIDKey(string id)
    {
        return loginUID + id;
    }

    public static string UserLockKey(string id)
    {
        return userLockKey + id;
    }
}