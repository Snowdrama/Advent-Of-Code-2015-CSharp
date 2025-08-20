namespace AdventOfCode2015
{
    internal partial class Day7
    {
        //the signal class is only provides an output
        public class Signal : ITickable
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
}
