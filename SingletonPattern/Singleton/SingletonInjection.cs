namespace SingletonPattern.Singleton;

public class SingletonInjection
{

    public SingletonInjection Instance
    {
        get
        {
            Logger.Info("Instance called.");
            return this;
        }
    }

    public SingletonInjection()
    {
        Logger.Info("Constructor invoked.");
    }
}