namespace BridgePattern.Bridge;

public class BridgeRadio : IDevice
{
    public void TurnOn() => Console.WriteLine("Radio is turned ON");
    public void TurnOff() => Console.WriteLine("Radio is turned OFF");
    public void SetVolume(int volume) => Console.WriteLine($"Radio volume set to {volume}");
}