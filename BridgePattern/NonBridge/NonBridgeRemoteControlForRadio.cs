namespace BridgePattern.NonBridge;

public class NonBridgeRemoteControlForRadio(NonBridgeRadio radio)
{
    public void PowerButton()
    {
        Console.WriteLine("Advanced Remote: Power Button Pressed.");
        radio.TurnOn();
    }
    
    public void VolumeUp()
    {
        Console.WriteLine("Basic Remote: Increasing Volume.");
        radio.SetVolume(10);
    }

    public void VolumeDown()
    {
        Console.WriteLine("Basic Remote: Decreasing Volume.");
        radio.SetVolume(5);
    }

    public void Mute()
    {
        Console.WriteLine("Advanced Remote: Muting Device.");
        radio.SetVolume(0);
    }
}