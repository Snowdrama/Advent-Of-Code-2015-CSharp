namespace AdventOfCode2015
{
    internal partial class Day7
    {
        public class Wire
        {
            string id;
            ushort value;

            ushort? overrideValue = null;
            List<WireConnection> outputWireConnections = new List<WireConnection>();
            List<Wire> wires = new List<Wire>();
            public Wire(string id)
            {
                this.id = id;
            }

            public string GetId()
            {
                return id;
            }


            //a wire can connect to multiple wires and gates
            public void AddWire(Wire wire)
            {
                wires.Add(wire);
            }
            public void AddGate(Gate gate, ConnectionType connection)
            {
                outputWireConnections.Add(new WireConnection()
                {
                    Gate = gate,
                    ConnectionType = connection
                });
            }

            public void ResetWire()
            {
                this.value = 0;
            }
            public void OverrideValue(ushort value)
            {
                this.overrideValue = value;
            }

            //when provided a signal, pass it to all connected wires and gates
            public void PassSignal(ushort value)
            {
                this.value = value;

                if (overrideValue != null)
                {
                    ConsoleEx.Red($"Overriding value! overriding value with {overrideValue}");
                    value = (ushort)overrideValue;
                    this.value = (ushort)overrideValue;
                }

                foreach (var wire in wires)
                {
                    wire.PassSignal(value);
                }

                foreach (var connection in outputWireConnections)
                {
                    switch (connection.ConnectionType)
                    {
                        case ConnectionType.Left:
                            connection.Gate.SetInputLeft(value);
                            break;
                        case ConnectionType.Right:
                            connection.Gate.SetInputRight(value);
                            break;
                        default:
                            break;
                    }
                }
            }

            public ushort GetSignal()
            {
                return value;
            }

            public override string ToString()
            {
                return $"Wire {id} Power: {value}";
            }
        }
    }
}
