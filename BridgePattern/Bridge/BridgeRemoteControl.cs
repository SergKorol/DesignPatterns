namespace BridgePattern.Bridge;

public class BridgeRemoteControl(IDevice device) : Remote(device)
{
    public override void PowerButton()
    {
        Console.WriteLine("Override Remote: Power Button Pressed.");
        Device.TurnOn();
    }
    
    public override void VolumeUp()
    {
        Console.WriteLine("Override Remote: Increasing Volume.");
        Device.SetVolume(10);
    }
    
    public override void VolumeDown()
    {
        Console.WriteLine("Override Remote: Decreasing Volume.");
        Device.SetVolume(5);
    }
}