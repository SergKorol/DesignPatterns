using MediatR;

namespace MediatorPattern.Mediatr;

public class ChatRoomHandler : INotificationHandler<ChatMessage>
{
    private static readonly List<string> Users = new() { "Alice", "Bob", "Charlie" };

    public Task Handle(ChatMessage notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{notification.Sender} sends: {notification.Message}");
        foreach (var user in Users)
        {
            if (user != notification.Sender)
            {
                Console.WriteLine($"{user} receives from {notification.Sender}: {notification.Message}");
            }
        }

        return Task.CompletedTask;
    }
}