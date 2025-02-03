namespace CommandPattern.NonCommand;

public class NonCommandRemoteControl(Light? light)
{
    public void PressButton(int command)
    {
        switch (command)
        {
            case 1:
                light?.TurnOn();
                break;
            case 2:
                light?.TurnOff();
                break;
            default:
                Console.WriteLine("Incorrect Command.");
                break;
        }
    }
    
    public void PressUndo(int lastCommand)
    {
        switch (lastCommand)
        {
            case 1:
                light?.TurnOff();
                break;
            case 2:
                light?.TurnOn();
                break;
            default:
                Console.WriteLine("No previous action to undo.");
                break;
        }
    }
}