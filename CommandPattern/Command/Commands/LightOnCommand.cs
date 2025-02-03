using CommandPattern.Command.Commands.Interfaces;

namespace CommandPattern.Command.Commands;

public record LightOnCommand(Light Light) : ICommand
{
    public void Execute()
    {
        Light.TurnOn();
    }

    public void Undo()
    {
        Light.TurnOff();
    }
}