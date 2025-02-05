namespace BridgePattern.Bridge;

public class ExtendedBridgeRemoteControl(IDevice device) : Remote(device)
{
    public void Mute()
    {
        Console.WriteLine("Advanced Remote: Muting Device.");
        Device.SetVolume(0);
    }
}