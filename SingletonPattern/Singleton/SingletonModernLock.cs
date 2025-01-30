namespace SingletonPattern.Singleton;

public sealed class SingletonModernLock
{
    private static SingletonModernLock? _instance;
    private static readonly Lock Padlock = new ();

    public static SingletonModernLock Instance
    {
        get
        {
            Logger.Info("Instance called.");
            using (Padlock.EnterScope())
            {
                return _instance ??= new SingletonModernLock();
            }
        }
    }

    private SingletonModernLock()
    {
        Logger.Info("Constructor invoked.");
    }
}