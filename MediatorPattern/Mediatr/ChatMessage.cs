using MediatR;

namespace MediatorPattern.Mediatr;

public class ChatMessage : INotification
{
    public string Sender { get; }
    public string Message { get; }

    public ChatMessage(string sender, string message)
    {
        Sender = sender;
        Message = message;
    }
}