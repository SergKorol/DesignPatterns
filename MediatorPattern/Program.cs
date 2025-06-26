using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MediatorPattern.Mediator;
using MediatorPattern.Mediatr;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Settings;

namespace MediatorPattern;

public class Program
{
    public static async Task Main()
    {
        BenchmarkRunner.Run<Program>(new BenchmarkConfig());
        
        // MediatorApproach();

        // await MediatrApproach();

        // NaiveApproach();
    }

    [Benchmark]
    public void NaiveApproach()
    {
        var chatRoom = new MediatorPattern.Naive.ChatRoom();

        var alice = new MediatorPattern.Naive.User("Alice", chatRoom);
        var bob = new MediatorPattern.Naive.User("Bob", chatRoom);
        var charlie = new MediatorPattern.Naive.User("Charlie", chatRoom);

        alice.Send("Hello everyone!");
        bob.Send("Hi Alice!");
        charlie.Send("Hey folks!");
    }

    [Benchmark]
    public async Task MediatrApproach()
    {
        var services = new ServiceCollection();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

        var provider = services.BuildServiceProvider();
        var mediator = provider.GetRequiredService<IMediator>();

        await mediator.Publish(new ChatMessage("Alice", "Hello everyone!"));
        await mediator.Publish(new ChatMessage("Bob", "Hi Alice!"));
        await mediator.Publish(new ChatMessage("Charlie", "Hey folks!"));
    }

    [Benchmark]
    public void MediatorApproach()
    {
        IChatMediator chatRoom = new ChatRoom();

        var user1 = new User("Alice", chatRoom);
        var user2 = new User("Bob", chatRoom);
        var user3 = new User("Charlie", chatRoom);

        user1.Send("Hello everyone!");
        user2.Send("Hi Alice!");
        user3.Send("Hey folks!");
    }
}