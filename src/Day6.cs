namespace AdventOfCode2015
{
    internal class Day6
    {
        Light[,] lights = new Light[1000, 1000];
        public Day6(bool newRules = false, bool isTest = false)
        {
            string[] input;
            if (isTest)
            {
                input = System.IO.File.ReadAllLines("Data/Day6/day6_test.txt"); ;
            }
            else
            {
                input = System.IO.File.ReadAllLines("Data/Day6/day6.txt");
            }
            if (!newRules)
            {
                Part1(input);
            }
            else
            {
                Part2(input);
            }
        }
        public void Part1(string[] input)
        {
            var lightCommands = ParseCommands(input);
            foreach (LightCommand lc in lightCommands)
            {

                for (int y = lc.MinPos.Y; y <= lc.MaxPos.Y; y++)
                {
                    for (int x = lc.MinPos.X; x <= lc.MaxPos.X; x++)
                    {
                        switch (lc.Command)
                        {
                            case "on":
                                lights[x, y].brightness = 1;
                                //ConsoleEx.Green($"Setting[{x},{y}]:on");
                                break;
                            case "off":
                                lights[x, y].brightness = 0;
                                //ConsoleEx.Red($"Setting[{x},{y}]:off");
                                break;
                            case "toggle":
                                //ConsoleEx.Cyan($"Setting[{x},{y}]:toggle{lights[x, y]} to {lights[x, y]}");
                                if (lights[x, y].brightness == 0)
                                {
                                    lights[x, y].brightness = 1;
                                }
                                else
                                {
                                    lights[x, y].brightness = 0;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            int lightCount = 0;
            for (int y = 0; y < lights.GetLength(1); y++)
            {
                for (int x = 0; x < lights.GetLength(0); x++)
                {
                    if (lights[x, y].brightness > 0)
                    {
                        lightCount++;
                    }
                }
            }
            ConsoleEx.Cyan($"Final Light Count: {lightCount}");
        }
        public void Part2(string[] input)
        {
            var lightCommands = ParseCommands(input);
            foreach (LightCommand lc in lightCommands)
            {

                for (int y = lc.MinPos.Y; y <= lc.MaxPos.Y; y++)
                {
                    for (int x = lc.MinPos.X; x <= lc.MaxPos.X; x++)
                    {
                        switch (lc.Command)
                        {
                            case "on":
                                lights[x, y].brightness += 1;
                                //ConsoleEx.Green($"Setting[{x},{y}]:on");
                                break;
                            case "off":
                                lights[x, y].brightness -= 1;
                                //ConsoleEx.Red($"Setting[{x},{y}]:off");
                                break;
                            case "toggle":
                                //ConsoleEx.Cyan($"Setting[{x},{y}]:toggle{lights[x, y]} to {lights[x, y]}");
                                lights[x, y].brightness += 2;
                                break;
                            default:
                                break;
                        }
                        //make sure we never go below 0
                        lights[x, y].brightness = Math.Clamp(lights[x, y].brightness, 0, int.MaxValue);
                    }
                }
            }
            int brigtnessTotal = 0;
            for (int y = 0; y < lights.GetLength(1); y++)
            {
                for (int x = 0; x < lights.GetLength(0); x++)
                {
                    brigtnessTotal += lights[x, y].brightness;
                }
            }
            ConsoleEx.Cyan($"Total Brightness: {brigtnessTotal}");
        }


        public LightCommand[] ParseCommands(string[] input)
        {
            LightCommand[] commands = new LightCommand[input.Length];
            for (int i = 0; i < commands.Length; i++)
            {
                var command = "";
                var line = input[i].ToLowerInvariant();

                if (line.StartsWith("toggle"))
                {
                    command = "toggle";
                    line = line.Replace("toggle ", "");
                }
                else if (line.StartsWith("turn on"))
                {
                    command = "on";
                    line = line.Replace("turn on ", "");
                }
                else if (line.StartsWith("turn off"))
                {
                    command = "off";
                    line = line.Replace("turn off ", "");
                }
                //ConsoleEx.Blue($"Command: turn {command}");

                var split = line.Split(" through ");
                //ConsoleEx.Magenta($"split[0]: {split[0]}");
                //ConsoleEx.Magenta($"split[1]: {split[1]}");

                var firstCoord = split[0].Split(',');
                var seconndCoord = split[1].Split(',');


                //ConsoleEx.Magenta($"firstCoord: {firstCoord}");
                //ConsoleEx.Magenta($"seconndCoord: {seconndCoord}");

                int xMin = int.Parse(firstCoord[0]);
                int yMin = int.Parse(firstCoord[1]);
                int xMax = int.Parse(seconndCoord[0]);
                int yMax = int.Parse(seconndCoord[1]);

                commands[i] = new LightCommand()
                {
                    Command = command,
                    MinPos = new Vector2I(xMin, yMin),
                    MaxPos = new Vector2I(xMax, yMax),
                };
            }
            return commands;
        }
        public struct Light
        {
            public int brightness { get; set; }
            public bool isOn { get { return brightness > 0; } }
        }

        public struct LightCommand
        {
            public string Command;
            public Vector2I MinPos;
            public Vector2I MaxPos;
        }
    }
}
