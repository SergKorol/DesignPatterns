namespace BridgePattern.Bridge;

public interface IDevice
{
    void TurnOn();
    void TurnOff();
    void SetVolume(int volume);
}