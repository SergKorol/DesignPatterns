namespace BridgePattern.Bridge;

public class BridgeTelevision : IDevice
{
    public void TurnOn() => Console.WriteLine("TV is turned ON");
    public void TurnOff() => Console.WriteLine("TV is turned OFF");
    public void SetVolume(int volume) => Console.WriteLine($"TV volume set to {volume}");
}