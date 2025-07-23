namespace GameAPIServer.Repository;

public class MemoryDbKeyMaker
{
    const string loginUID = "UID_";

    public static string MakeUIDKey(string id)
    {
        return loginUID + id;
    }
}