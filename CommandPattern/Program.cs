using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CommandPattern;
using CommandPattern.Command.Commands;
using CommandPattern.Command.Commands.Interfaces;
using CommandPattern.NonCommand;
using Settings;

public class Program
{
    static void Main()
    {
        BenchmarkRunner.Run<Program>(new BenchmarkConfig());
    }

    [Benchmark]
    public void RunCommand()
    {
        Light light = new Light();
        CommandRemoteControl commandRemote = new CommandRemoteControl();
        ICommand lightOnCommand = new LightOnCommand(light);
        ICommand lightOffCommand = new LightOffCommand(light);
        ICommand? lastCommand = null;
        int[] choices = { 1, 2, 3, 0 };
        Console.WriteLine("Choose light action: 1. On, 2. Off, 3. Undo");
        foreach (var choice in choices)
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine($"Chosen action: On");
                    commandRemote.Invoke(lightOnCommand);
                    lastCommand = lightOnCommand;
                    commandRemote.PressButton();
                    break;
                case 2:
                    Console.WriteLine($"Chosen action: Off");
                    commandRemote.Invoke(lightOffCommand);
                    lastCommand = lightOffCommand;
                    commandRemote.PressButton();
                    break;
                case 3:
                    Console.WriteLine($"Chosen action: Undo");
                    commandRemote.PressUndo();
                    commandRemote.Invoke(lastCommand);
                    break;
                default:
                    Console.WriteLine("Chosen action: Unknown");
                    commandRemote.Invoke(null);
                    commandRemote.PressButton();
                    break;
            }
        }
    }
    
    [Benchmark]
    public void RunNonCommand()
    {
        Light light = new Light();
        NonCommandRemoteControl nonCommandRemote = new NonCommandRemoteControl(light);
        
        int lastCommand = 0;
        int[] choices = { 1, 2, 3, 0 };

        Console.WriteLine("Choose light action: 1. On, 2. Off, 3. Undo");
        foreach (var choice in choices)
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine($"Chosen action: On");
                    lastCommand = choice;
                    nonCommandRemote.PressButton(choice);
                    break;
                case 2:
                    Console.WriteLine($"Chosen action: Off");
                    lastCommand = choice;
                    nonCommandRemote.PressButton(choice);
                    break;
                case 3:
                    Console.WriteLine($"Chosen action: Undo");
                    nonCommandRemote.PressUndo(lastCommand);
                    break;
                default:
                    Console.WriteLine("Chosen action: Unknown");
                    nonCommandRemote.PressButton(0);
                    break;
            }
        }
    }
}