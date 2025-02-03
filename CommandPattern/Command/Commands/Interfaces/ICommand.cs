namespace CommandPattern.Command.Commands.Interfaces;

public interface ICommand
{
    void Execute();
    void Undo();
}