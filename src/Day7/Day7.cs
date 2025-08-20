namespace AdventOfCode2015
{
    //I know they gave the hint that most languages have a thing for emulating this...
    //but I wanted to challenge myself to write it by hand lol
    internal partial class Day7
    {
        List<Signal> signalList = new List<Signal>();
        Dictionary<string, Gate> gateList = new Dictionary<string, Gate>();
        Dictionary<string, Wire> wireList = new Dictionary<string, Wire>();
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


            for (int i = 0; i < input.Length; i++)
            {
                var line = input[i];
                if (line.Contains("NOT"))
                {
                    if (ParseHelper.TryParse(line, "NOT {left} -> {wire}", out Dictionary<string, string> output))
                    {
                        var leftVal = output["left"];
                        var wireVal = output["wire"];
                        var gateKey = $"NOT_{leftVal}_{wireVal}";
                        var leftWire = SetupWire(leftVal);
                        var outputWire = SetupWire(wireVal);
                        var gate = SetupGate(gateKey, outputWire, GateType.NOT);

                        //check if the leftVal is a number or wire by parsing
                        if (ushort.TryParse(leftVal, out ushort num))
                        {
                            //it's a number, so we can set the left value of the gate directly
                            gate.SetInputLeft(num);
                        }
                        else
                        {
                            //it's a wire, so we need to connect the wire to the gate
                            leftWire.AddGate(gate, ConnectionType.Left);
                        }



                    }
                }
                else if (
                    line.Contains("AND") ||
                    line.Contains("OR") ||
                    line.Contains("LSHIFT") ||
                    line.Contains("RSHIFT")
                )
                {
                    if (ParseHelper.TryParse(line, "{left} {type} {right} -> {wire}", out Dictionary<string, string> output))
                    {
                        var leftVal = output["left"];
                        var rightVal = output["right"];
                        var wireVal = output["wire"];
                        var typeVal = output["type"];

                        var gateKey = $"{typeVal}_{leftVal}_{rightVal}_{wireVal}";

                        string type = output["type"];

                        //we always need an output wire
                        Wire? outputWire = SetupWire(wireVal);

                        Gate? gate = null;

                        //setup the gate based on type
                        switch (type)
                        {
                            case "AND":
                                gate = SetupGate(gateKey, outputWire, GateType.AND);
                                break;
                            case "OR":
                                //or uses 3 wires
                                gate = SetupGate(gateKey, outputWire, GateType.OR);
                                break;
                            case "LSHIFT":
                                gate = SetupGate(gateKey, outputWire, GateType.LSHIFT);
                                break;
                            case "RSHIFT":
                                gate = SetupGate(gateKey, outputWire, GateType.RSHIFT);
                                break;
                            default:
                                throw new Exception("Unknown gate type");
                        }

                        //check if the leftVal is a number or wire by parsing
                        if (ushort.TryParse(leftVal, out ushort numLeft))
                        {
                            //it's a number, so we can set the left value of the gate directly
                            gate.SetOverrideLeft(numLeft);
                        }
                        else
                        {
                            //it's a wire, so we need to connect the wire to the gate
                            Wire leftWire = SetupWire(leftVal);
                            leftWire.AddGate(gate, ConnectionType.Left);
                        }

                        //check if the rightVal is a number or wire by parsing
                        if (ushort.TryParse(rightVal, out ushort numRight))
                        {
                            //it's a number, so we can set the left value of the gate directly
                            gate.SetOverrideRight(numRight);
                        }
                        else
                        {
                            //it's a wire, so we need to connect the wire to the gate
                            Wire rightWire = SetupWire(rightVal);
                            rightWire.AddGate(gate, ConnectionType.Right);
                        }
                    }
                }
                else
                {
                    //not a gate, must be a signal
                    if (ParseHelper.TryParse(line, "{value} -> {wire}", out Dictionary<string, string> output))
                    {
                        var valueString = output["value"];
                        var wireId = output["wire"];

                        var wire = SetupWire(wireId);

                        //signals are always unique so they aren't keyed

                        if (ushort.TryParse(valueString, out ushort num))
                        {
                            ushort value = ushort.Parse(valueString);
                            signalList.Add(new Signal(value, wire));
                        }
                        else
                        {
                            //it's not a signal it's actually a wire connecting
                            //to another wire.
                            //set up another wire...
                            var otherWire = SetupWire(valueString);
                            otherWire.AddWire(wire);
                        }
                    }
                }
            }


            ConsoleEx.Blue("Lines Read! Now to process...");

            for (int i = 0; i < signalList.Count; i++)
            {
                signalList[i].Tick();
            }
            wireList = wireList.OrderBy(x => x.Key).ToDictionary();
            foreach (var wire in wireList.Values)
            {
                ConsoleEx.Yellow($"{wire}");
            }

            //now take the signal you got from wire a and set it to wire b

            var signalA = wireList["a"].GetSignal();
            ConsoleEx.Green($"Signal on wire a is {signalA}!!!\n\n");
            Thread.Sleep(2000);
            //running the signals again should propagate the new value through the system


            ////reset the gates so we can reprocess
            foreach (var gate in gateList.Values)
            {
                gate.ResetGate();
            }

            //reset the wires so we can reprocess
            foreach (var wire in wireList.Values)
            {
                wire.ResetWire();
            }
            foreach (var wire in wireList.Values)
            {
                ConsoleEx.Magenta($"{wire}");
            }
            Thread.Sleep(2000);

            wireList["b"].OverrideValue(signalA);

            //now that we've overridden b reprocess
            for (int i = 0; i < signalList.Count; i++)
            {
                signalList[i].Tick();
            }

            foreach (var wire in wireList.Values)
            {
                ConsoleEx.Yellow($"{wire}");
            }

            var newSignalA = wireList["a"].GetSignal();
            ConsoleEx.Green($"Signal on wire a is now{newSignalA}!!!");
        }

        public void WireOrGateValue(string value)
        {
            //check if it's a number or a wire
            if (ushort.TryParse(value, out ushort num))
            {
                //it's a number
            }
            else
            {
                //it's a wire
            }
        }

        public Wire SetupWire(string wireId)
        {
            if (!wireList.ContainsKey(wireId))
            {
                Wire wire = new Wire(wireId);
                wireList.Add(wireId, wire);
                return wire;
            }
            return wireList[wireId];
        }

        public Gate SetupGate(string gateKey, Wire outputWire, GateType type, ushort? rightValue = null, ushort? leftValue = null)
        {
            if (!gateList.ContainsKey(gateKey))
            {
                Gate gate = new Gate(gateKey, outputWire, type, leftValue, rightValue);
                gateList.Add(gateKey, gate);
                return gate;
            }
            return gateList[gateKey];
        }

        public enum InstructionType
        {
            Signal,
            Gate,
        }
        public abstract class Instruction { }
        public class GateInstruction : Instruction
        {
            public GateType type;
            public string? gateId;
            public string? wireId;
        }
        public class SignalInstruction : Instruction
        {
            public string? wireId;
            public ushort powerValue;
        }
        public void ParseInstructions(string[] instructions)
        {

        }
    }
}
