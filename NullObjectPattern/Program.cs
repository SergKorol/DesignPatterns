// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using NullObjectPattern.NonNullObject.Services;
using NullObjectPattern.NullObject.Services;
using Settings;

public class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<Program>(new BenchmarkConfig());
    }

    [Benchmark]
    public void RunGetNullObjectUser()
    {
        var userService = new NullObjectUserService();
        var user = userService.GetCurrentNullObjectUser(Guid.Parse("3d73ae77-229a-4f7b-a3c6-49a3e283b5ac"));
        if (user.Id == Guid.Empty)
        {
            Console.WriteLine("User not found");
        }
    }

    [Benchmark]
    public void RunGetNonNullObjectUser()
    {
        var userService = new NonNullObjectUserService();
        var user = userService.GetCurrentUser(Guid.Parse("3d73ae77-229a-4f7b-a3c6-49a3e283b5ac"));
        if (user.Id == Guid.Empty)
        {
            Console.WriteLine("User not found");
        }
    }
}