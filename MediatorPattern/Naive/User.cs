namespace MediatorPattern.Naive;

public class User
{
    private string Name { get; }
    private readonly ChatRoom _chatRoom;

    public User(string name, ChatRoom chatRoom)
    {
        Name = name;
        _chatRoom = chatRoom;
        _chatRoom.RegisterUser(this);
    }

    public void Send(string message)
    {
        Console.WriteLine($"{Name} sends: {message}");
        _chatRoom.Broadcast(message, this);
    }

    public void ReceiveMessage(string message, User sender)
    {
        Console.WriteLine($"{Name} received from {sender.Name}: {message}");
    }
}