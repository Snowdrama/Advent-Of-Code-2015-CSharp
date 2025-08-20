using static AdventOfCode2015.Day7.Day7;

namespace AdventOfCode2015.Day7
{
    //the signal class is only provides an output
    public class Signal
    {
        ushort value;
        Wire outputWire;
        public Signal(ushort value, Wire outputWire)
        {
            this.value = value;
            this.outputWire = outputWire;
        }

        public void Tick()
        {
            ConsoleEx.Blue($"Ticking Signal value {value} to: {outputWire.GetId()}");
            outputWire.PassSignal(value);
        }
    }
}
