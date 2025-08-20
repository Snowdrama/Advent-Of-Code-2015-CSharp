using static AdventOfCode2015.Day7.Day7;

namespace AdventOfCode2015.Day7
{
    public class Gate
    {
        string _id;
        Wire _outputWire;

        ushort? _signalLeft;
        ushort? _signalRight;

        ushort? _overrideSignalLeft;
        ushort? _overrideSignalRight;

        GateType type;
        public Gate(string id, Wire output, GateType type, ushort? signalLeft = null, ushort? signalRight = null)
        {
            this._id = id;
            this._outputWire = output;
            this._overrideSignalLeft = signalLeft;
            this._overrideSignalRight = signalRight;
            this.type = type;
        }

        public void SetInputLeft(ushort? signal)
        {
            _signalLeft = signal;
            ProcessGate();
        }

        public void SetInputRight(ushort? signal)
        {
            _signalRight = signal;
            ProcessGate();
        }
        public void SetOverrideLeft(ushort? signal)
        {
            _overrideSignalLeft = signal;
        }
        public void SetOverrideRight(ushort? signal)
        {
            _overrideSignalRight = signal;
        }
        public void ResetGate()
        {
            _signalLeft = null;
            _signalRight = null;
        }
        private void ProcessGate()
        {
            Console.WriteLine($"Processing Gate {_id} type: {type}");

            //overrides are used to set the signal directly
            //so gates that have fixed values can be reset
            //without having to change anything about the wiring
            if (_overrideSignalLeft != null)
            {
                _signalLeft = _overrideSignalLeft;
            }

            if (_overrideSignalRight != null)
            {
                _signalRight = _overrideSignalRight;
            }


            //when we process a gate, if we have both inputs set
            //we pass the signal to the associated output wire
            switch (type)
            {
                case GateType.NOT:
                    if (_signalLeft != null)
                    {
                        _outputWire.PassSignal((ushort)(~_signalLeft));
                    }
                    break;
                case GateType.AND:
                    if (_signalLeft != null && _signalRight != null)
                    {
                        _outputWire.PassSignal((ushort)(_signalLeft & _signalRight));
                    }
                    break;
                case GateType.OR:
                    if (_signalLeft != null && _signalRight != null)
                    {
                        _outputWire.PassSignal((ushort)(_signalLeft | _signalRight));
                    }
                    break;
                case GateType.NOR:
                    if (_signalLeft != null && _signalRight != null)
                    {
                        _outputWire.PassSignal((ushort)(~(_signalLeft | _signalRight)));
                    }
                    break;
                case GateType.XOR:
                    if (_signalLeft != null && _signalRight != null)
                    {
                        _outputWire.PassSignal((ushort)(_signalLeft ^ _signalRight));
                    }
                    break;
                case GateType.LSHIFT:
                    if (_signalLeft != null && _signalRight != null)
                    {
                        _outputWire.PassSignal((ushort)(_signalLeft << (int)_signalRight));
                    }
                    break;
                case GateType.RSHIFT:
                    if (_signalLeft != null && _signalRight != null)
                    {
                        _outputWire.PassSignal((ushort)(_signalLeft >> (int)_signalRight));
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
