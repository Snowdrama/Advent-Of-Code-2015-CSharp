namespace AdventOfCode2015
{
    //I know they gave the hint that most languages have a thing for emulating this...
    //but I wanted to challenge myself to write it by hand lol
    internal class Day7
    {
        Dictionary<string, Wire> wires;
        public Day7(bool newRules = false, bool isTest = false)
        {
            string[] input;
            if (isTest)
            {
                input = System.IO.File.ReadAllLines("Data/Day7/day7_test.txt"); ;
            }
            else
            {
                input = System.IO.File.ReadAllLines("Data/Day7/day7.txt");
            }

            //manual example:
            Wire x = new Wire("x");
            Wire y = new Wire("y");
            Wire d = new Wire("d");
            Wire e = new Wire("e");
            Wire f = new Wire("f");
            Wire g = new Wire("g");
            Wire h = new Wire("h");
            Wire i = new Wire("i");

            Gate xyd = new Gate("xyd", d, GateType.AND);
            Gate xye = new Gate("xye", e, GateType.OR);
            Gate xlsf = new Gate("xlsf", f, GateType.LSHIFT, 2);
            Gate yrsg = new Gate("yrsg", g, GateType.RSHIFT, 2);
            Gate notxh = new Gate("notxh", h, GateType.NOT);
            Gate notyi = new Gate("notyi", i, GateType.NOT);



            x.AddGate(xyd, ConnectionType.Left);
            y.AddGate(xyd, ConnectionType.Right);

            x.AddGate(xye, ConnectionType.Left);
            y.AddGate(xye, ConnectionType.Right);

            x.AddGate(xlsf, ConnectionType.Left);
            y.AddGate(yrsg, ConnectionType.Left);

            x.AddGate(notxh, ConnectionType.Left);
            y.AddGate(notyi, ConnectionType.Left);

            Signal inputOne = new Signal(123, x);
            Signal inputTwo = new Signal(456, y);
            inputOne.Tick();
            inputTwo.Tick();

            Console.WriteLine(d.ToString()); //d: 72
            Console.WriteLine(e.ToString()); //e: 507
            Console.WriteLine(f.ToString()); //f: 492
            Console.WriteLine(g.ToString()); //g: 114
            Console.WriteLine(h.ToString()); //h: 65412
            Console.WriteLine(i.ToString()); //i: 65079
            Console.WriteLine(x.ToString()); //x: 123
            Console.WriteLine(y.ToString()); //y: 456
        }

        public enum ConnectionType
        {
            Left = 1,
            Right = 2,
        }

        public struct WireConnection
        {
            public Gate Gate { get; set; }
            public ConnectionType ConnectionType { get; set; }
        }
        public class Wire
        {
            string id;
            ushort value;
            List<WireConnection> outputWireConnections = new List<WireConnection>();

            public Wire(string id)
            {
                this.id = id;
            }

            public void AddGate(Gate gate, ConnectionType connection)
            {
                outputWireConnections.Add(new WireConnection()
                {
                    Gate = gate,
                    ConnectionType = connection
                });
            }

            public void PassSignal(ushort value)
            {
                this.value = value;
                foreach (var connection in outputWireConnections)
                {
                    switch (connection.ConnectionType)
                    {
                        case ConnectionType.Left:
                            connection.Gate.PassInputLeft(value);
                            break;
                        case ConnectionType.Right:
                            connection.Gate.PassInputRight(value);
                            break;
                        default:
                            break;
                    }
                }
            }

            public override string ToString()
            {
                return $"Wire {id} Power: {value}";
            }
        }

        public enum GateType
        {
            NOT = 0,
            AND = 1,
            OR = 2,
            NOR = 3,
            XOR = 4,
            LSHIFT = 5,
            RSHIFT = 6,
        }

        public class Gate
        {
            string _id;
            Wire _outputWire;

            ushort _signalLeft;
            ushort _signalRight;

            bool _leftSet;
            bool _rightSet;

            GateType type;
            public Gate(string id, Wire output, GateType type, ushort signalRight = 0, ushort signalLeft = 0)
            {
                this._id = id;
                this._outputWire = output;
                this._signalLeft = signalLeft;
                this._signalRight = signalRight;
                this.type = type;
            }

            public void PassInputLeft(ushort signal)
            {
                _signalLeft = signal;
                _leftSet = true;
                ProcessGate();
            }

            public void PassInputRight(ushort signal)
            {
                _signalRight = signal;
                _rightSet = true;
                ProcessGate();
            }

            private void ProcessGate()
            {
                Console.WriteLine($"Processing Gate {_id} type: {type}");
                switch (type)
                {
                    case GateType.NOT:
                        if (_leftSet)
                        {
                            _outputWire.PassSignal((ushort)(~_signalLeft));
                        }
                        break;
                    case GateType.AND:
                        if (_leftSet && _rightSet)
                        {
                            _outputWire.PassSignal((ushort)(_signalLeft & _signalRight));
                        }
                        break;
                    case GateType.OR:
                        if (_leftSet && _rightSet)
                        {
                            _outputWire.PassSignal((ushort)(_signalLeft | _signalRight));
                        }
                        break;
                    case GateType.NOR:
                        if (_leftSet && _rightSet)
                        {
                            _outputWire.PassSignal((ushort)(~(_signalLeft | _signalRight)));
                        }
                        break;
                    case GateType.XOR:
                        if (_leftSet && _rightSet)
                        {
                            _outputWire.PassSignal((ushort)(_signalLeft ^ _signalRight));
                        }
                        break;
                    case GateType.LSHIFT:
                        if (_leftSet)
                        {
                            _outputWire.PassSignal((ushort)(_signalLeft << (int)_signalRight));
                        }
                        break;
                    case GateType.RSHIFT:
                        if (_leftSet)
                        {
                            _outputWire.PassSignal((ushort)(_signalLeft >> (int)_signalRight));
                        }
                        break;
                    default:
                        break;
                }
            }
        }

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
                outputWire.PassSignal(value);
            }
        }
        public interface ITickable
        {
            public void Tick();
        }
    }
}
