using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BridgePattern.Bridge;
using BridgePattern.NonBridge;
using Settings;

namespace BridgePattern;

public class Program
{
    static void Main()
    {
        BenchmarkRunner.Run<Program>(new BenchmarkConfig());
    }

    [Benchmark]
    public void RunNonBridge()
    {
        NonBridgeTelevision tv = new NonBridgeTelevision();
        NonBridgeRemoteControlForTelevision tvRemote = new NonBridgeRemoteControlForTelevision(tv);
        tvRemote.PowerButton();
        tvRemote.VolumeUp();

        Console.WriteLine();

        NonBridgeRadio radio = new NonBridgeRadio();
        NonBridgeRemoteControlForRadio radioRemote = new NonBridgeRemoteControlForRadio(radio);
        radioRemote.PowerButton();
        radioRemote.Mute();
    }

    [Benchmark]
    public void RunBridge()
    {
        IDevice tv = new BridgeTelevision();
        Remote basicRemote = new BridgeRemoteControl(tv);
        basicRemote.PowerButton();
        basicRemote.VolumeUp();

        Console.WriteLine();

        IDevice radio = new BridgeRadio();
        ExtendedBridgeRemoteControl extendedBridgeRemoteControl = new ExtendedBridgeRemoteControl(radio);
        extendedBridgeRemoteControl.PowerButton();
        extendedBridgeRemoteControl.Mute();
    }
}