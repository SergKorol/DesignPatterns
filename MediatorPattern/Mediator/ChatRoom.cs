namespace MediatorPattern.Mediator;

public class ChatRoom : IChatMediator
{
    private readonly List<User> _users = new List<User>();

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public void SendMessage(string message, User sender)
    {
        foreach (var user in _users)
        {
            // Don't send message to the sender themselves
            if (user != sender)
            {
                user.ReceiveMessage(message, sender);
            }
        }
    }
}