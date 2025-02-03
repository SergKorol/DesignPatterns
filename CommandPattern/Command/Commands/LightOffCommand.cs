using CommandPattern.Command.Commands.Interfaces;

namespace CommandPattern.Command.Commands;

public record LightOffCommand(Light light) : ICommand
{
    public void Execute()
    {
        light.TurnOff();
    }

    public void Undo()
    {
        light.TurnOn();
    }
}