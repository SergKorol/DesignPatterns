using CommandPattern.Command.Commands.Interfaces;

namespace CommandPattern.Command.Commands;

public class CommandRemoteControl
{
    private ICommand? _command;

    public void Invoke(ICommand? command)
    {
        _command = command;
    }

    public void PressButton()
    {
        if (_command != null)
        {
            _command.Execute();
        }
        else
        {
            Console.WriteLine("No command set.");
        }
    }
    
    public void PressUndo()
    {
        if (_command != null)
        {
            _command.Undo();
        }
        else
        {
            Console.WriteLine("No command set to undo.");
        }
    }
}