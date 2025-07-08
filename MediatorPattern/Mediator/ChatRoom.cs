namespace MediatorPattern.Mediator;

public class ChatRoom : IChatMediator
{
    private readonly List<User> _users = new();

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public void SendMessage(string message, User sender)
    {
        foreach (var user in _users)
        {
            if (user != sender)
            {
                user.ReceiveMessage(message, sender);
            }
        }
    }
}