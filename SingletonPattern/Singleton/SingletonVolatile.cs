namespace SingletonPattern.Singleton;


public class SingletonVolatile
{
    private static volatile SingletonVolatile? _instance;

    public static SingletonVolatile Instance
    {
        get
        {
            Logger.Info("Instance called.");
            if (_instance != null) return _instance;
            lock (typeof(SingletonVolatile))
            {
                _instance ??= new SingletonVolatile();
            }
            return _instance;
        }
    }
    
    private SingletonVolatile()
    {
        Logger.Info("Constructor invoked.");
    }
}