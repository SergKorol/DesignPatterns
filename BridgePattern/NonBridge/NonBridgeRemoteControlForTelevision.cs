namespace BridgePattern.NonBridge;

public class NonBridgeRemoteControlForTelevision(NonBridgeTelevision tv)
{
    public void PowerButton()
    {
        Console.WriteLine("Basic Remote: Power Button Pressed.");
        tv.TurnOn();
    }

    public void VolumeUp()
    {
        Console.WriteLine("Basic Remote: Increasing Volume.");
        tv.SetVolume(10);
    }

    public void VolumeDown()
    {
        Console.WriteLine("Basic Remote: Decreasing Volume.");
        tv.SetVolume(5);
    }
}