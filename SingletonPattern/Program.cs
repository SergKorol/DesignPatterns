using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;
using Settings;
using SingletonPattern.Singleton;

public class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<Program>(new BenchmarkConfig());
    }

    [Benchmark]
    public void RunClassic()
    {
        var tasks = new List<string>() { "one", "two", "three" };
        Parallel.ForEach(tasks,_ =>
        {
            new List<SingletonClassic>().Add(SingletonClassic.Instance!);
        });
    }
    
    [Benchmark]
    public void RunOldLock()
    {
        var tasks = new List<string>() { "one", "two", "three" };
        Parallel.ForEach(tasks,_ =>
        {
            new List<SingletonOldLock>().Add(SingletonOldLock.Instance!);
        });
    }
    
    [Benchmark]
    public void RunModernLock()
    {
        var tasks = new List<string>() { "one", "two", "three" };
        Parallel.ForEach(tasks,_ =>
        {
            new List<SingletonModernLock>().Add(SingletonModernLock.Instance!);
        });
    }
    
    [Benchmark]
    public void RunDoubleCheck()
    {
        var tasks = new List<string>() { "one", "two", "three" };
        Parallel.ForEach(tasks,_ =>
        {
            new List<SingletonDoubleCheck>().Add(SingletonDoubleCheck.Instance!);
        });
    }
    
    [Benchmark]
    public void RunEager()
    {
        var tasks = new List<string>() { "one", "two", "three" };
        Parallel.ForEach(tasks,_ =>
        {
            new List<SingletonEager>().Add(SingletonEager.Instance!);
        });
    }
    
    [Benchmark]
    public void RunLazy()
    {
        var tasks = new List<string>() { "one", "two", "three" };
        Parallel.ForEach(tasks,_ =>
        {
            new List<SingletonLazy>().Add(SingletonLazy.Instance!);
        });
    }
    
    [Benchmark]
    public void RunVolatile()
    {
        var tasks = new List<string>() { "one", "two", "three" };
        Parallel.ForEach(tasks,_ =>
        {
            new List<SingletonVolatile>().Add(SingletonVolatile.Instance!);
        });
    }
    
    [Benchmark]
    public void RunInjection()
    {
        var services = new ServiceCollection();
        services.AddSingleton<SingletonInjection>();
        var serviceProvider = services.BuildServiceProvider();
        var tasks = new List<string>() { "one", "two", "three" };
        var singleton = serviceProvider.GetRequiredService<SingletonInjection>();
        Parallel.ForEach(tasks,_ =>
        {
            new List<SingletonInjection>().Add(singleton?.Instance!);
        });
    }
}

