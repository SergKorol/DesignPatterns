namespace SingletonPattern.Singleton;

public sealed class SingletonLazy
{
    private static readonly Lazy<SingletonLazy> Lazy = new(() => new SingletonLazy());
    public static SingletonLazy Instance
    {
        get
        {
            Logger.Info("Instance called.");
            return Lazy.Value;
        }
    }

    private SingletonLazy()
    {
        Logger.Info("Constructor invoked.");
    }
}