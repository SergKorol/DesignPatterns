namespace SingletonPattern.Singleton;

public sealed class SingletonEager
{
    private static readonly SingletonEager _instance = new();

    public static SingletonEager Instance
    {
        get
        {
            Logger.Info("Instance called.");
            return _instance;
        }
    }

    private SingletonEager()
    {
        Logger.Info("Constructor invoked.");
    }
}