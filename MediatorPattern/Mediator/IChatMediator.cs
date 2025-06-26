namespace MediatorPattern.Mediator;

public interface IChatMediator
{
    void SendMessage(string message, User sender);
    void AddUser(User user);
}