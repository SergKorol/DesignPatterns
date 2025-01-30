namespace SingletonPattern.Singleton;

public sealed class SingletonClassic
{
    private static SingletonClassic? _instance;

    public static SingletonClassic Instance
    {
        get
        {
            Logger.Info("Instance called.");
            return _instance ??= new SingletonClassic();
        }
    }

    private SingletonClassic()
    {
        Logger.Info("Constructor invoked.");
    }
}