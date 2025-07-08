namespace MediatorPattern.Mediator;

public class User
{
    private readonly IChatMediator _mediator;
    private string Name { get; }

    public User(string name, IChatMediator mediator)
    {
        Name = name;
        _mediator = mediator;
        _mediator.AddUser(this);
    }

    public void Send(string message)
    {
        Console.WriteLine($"{Name} sends: {message}");
        _mediator.SendMessage(message, this);
    }

    public void ReceiveMessage(string message, User sender)
    {
        Console.WriteLine($"{Name} receives from {sender.Name}: {message}");
    }
}