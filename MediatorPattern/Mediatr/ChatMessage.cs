using MediatR;

namespace MediatorPattern.Mediatr;

public class ChatMessage(string sender, string message) : INotification
{
    public string Sender { get; } = sender;
    public string Message { get; } = message;
}