namespace SingletonPattern.Singleton;

public sealed class SingletonDoubleCheck
{
    private static SingletonDoubleCheck? _instance;
    private static readonly object Padlock = new();

    public static SingletonDoubleCheck? Instance
    {
        get
        {
            Logger.Info("Instance called.");
            if (_instance != null) return _instance;
            lock (Padlock)
            {
                _instance ??= new SingletonDoubleCheck();
            }
            return _instance;
        }
    }

    private SingletonDoubleCheck()
    {
        Logger.Info("Constructor invoked.");
    }
}