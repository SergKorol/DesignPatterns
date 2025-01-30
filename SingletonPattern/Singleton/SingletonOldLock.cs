namespace SingletonPattern.Singleton;

public sealed class SingletonOldLock
{
    private static SingletonOldLock? _instance;
    private static readonly object padlock = new ();

    public static SingletonOldLock? Instance
    {
        get
        {
            Logger.Info("Instance called.");
            lock (padlock)
            {
                return _instance ??= new SingletonOldLock();
            }
        }
    }

    private SingletonOldLock()
    {
        Logger.Info("Constructor invoked.");
    }
}