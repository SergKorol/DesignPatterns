namespace BridgePattern.Bridge;

public abstract class Remote(IDevice device)
{
    protected readonly IDevice Device = device;
    
    public virtual void PowerButton()
    {
        Console.WriteLine("Basic Remote: Power Button Pressed.");
        Device.TurnOn();
    }

    public virtual void VolumeUp()
    {
        Console.WriteLine("Basic Remote: Increasing Volume.");
        Device.SetVolume(10);
    }

    public virtual void VolumeDown()
    {
        Console.WriteLine("Basic Remote: Decreasing Volume.");
        Device.SetVolume(5);
    }
}