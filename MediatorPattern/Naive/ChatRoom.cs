namespace MediatorPattern.Naive;

public class ChatRoom
{
    private readonly List<User> _users = [];

    public void RegisterUser(User user)
    {
        _users.Add(user);
    }

    public void Broadcast(string message, User sender)
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