namespace AdventOfCode2015.Day9
{
    public struct Destination
    {
        public string destinationName;
        public Dictionary<string, int> destinationDistances;

        public Destination(string name)
        {
            destinationName = name;
            destinationDistances = new Dictionary<string, int>();
        }

        public void AddDestination(string name, int distance)
        {
            if (destinationDistances.ContainsKey(name))
            {
                // If the new destination is shorter than the existing one
                // we update the distance
                if (distance < destinationDistances[name])
                {
                    destinationDistances[name] = distance;
                }
                return;
            }
            // If the destination does not exist, add it
            destinationDistances.Add(name, distance);
        }

        public override string ToString()
        {
            string output = $"{destinationName}:\n";
            foreach (var dest in destinationDistances)
            {
                output += $"  {dest.Key}: {dest.Value} km\n";
            }
            var shortestDistance = destinationDistances.OrderBy(x => x.Value).First();
            output += $"Shortest Distance: {shortestDistance.Key} -> {shortestDistance.Value}";

            return output;
        }

        public int GetDistanceTo(string dest)
        {
            if (destinationDistances.ContainsKey(dest))
            {
                return destinationDistances[dest];
            }
            return int.MaxValue;
        }

        public string GetShortestKey(int skip = 0)
        {
            var shortest = destinationDistances.OrderBy(x => x.Value);
            return shortest.Skip(skip).First().Key;
        }
        public string GetLongestKey(int skip = 0)
        {
            var shortest = destinationDistances.OrderByDescending(x => x.Value);
            return shortest.Skip(skip).First().Key;
        }

        public Queue<string> GetShortestPathRecursive(Dictionary<string, Destination> destinations, Queue<string> currentQueue)
        {
            var shortest = GetShortestKey();
            int skip = 1;

            ConsoleEx.Red($"Comparing {destinationName} to {shortest}");

            ConsoleEx.Yellow($"Current Queue:");

            foreach (var item in currentQueue)
            {
                ConsoleEx.Yellow($"{item}");
            }

            ConsoleEx.Blue($"Is {shortest} in the list??? {currentQueue.Contains(shortest)}");

            //if we already have this in the stack we need to get the next longest
            while (currentQueue.Contains(shortest) && skip < destinations.Count)
            {
                ConsoleEx.Red($"Already contains {shortest} finding new longest");
                shortest = GetShortestKey(skip);
                ConsoleEx.Red($"Trying next longest {shortest}");
                skip++;
            }

            ConsoleEx.Green($"Adding {shortest} to list!");
            currentQueue.Enqueue(shortest);

            if (currentQueue.Count == destinations.Count)
            {
                ConsoleEx.Green($"Queue is capped! Final Queue");
                foreach (var item in currentQueue)
                {
                    ConsoleEx.Yellow($"{item}");
                }
                return currentQueue;
            }

            ConsoleEx.Yellow($"Current Queue:");
            foreach (var item in currentQueue)
            {
                ConsoleEx.Yellow($"{item}");
            }

            return destinations[shortest].GetShortestPathRecursive(destinations, currentQueue);
        }

        public Queue<string> GetLongestPathRecursive(Dictionary<string, Destination> destinations, Queue<string> currentQueue)
        {
            var longest = GetLongestKey();
            int skip = 1;

            ConsoleEx.Red($"Comparing {destinationName} to {longest}");

            ConsoleEx.Yellow($"Current Queue:");

            foreach (var item in currentQueue)
            {
                ConsoleEx.Yellow($"{item}");
            }

            ConsoleEx.Blue($"Is {longest} in the list??? {currentQueue.Contains(longest)}");

            //if we already have this in the stack we need to get the next longest
            while (currentQueue.Contains(longest) && skip < destinations.Count)
            {
                ConsoleEx.Red($"Already contains {longest} finding new longest");
                longest = GetLongestKey(skip);
                ConsoleEx.Red($"Trying next longest {longest}");
                skip++;
            }

            ConsoleEx.Green($"Adding {longest} to list!");
            currentQueue.Enqueue(longest);

            if (currentQueue.Count == destinations.Count)
            {
                ConsoleEx.Green($"Queue is capped! Final Queue");
                foreach (var item in currentQueue)
                {
                    ConsoleEx.Yellow($"{item}");
                }
                return currentQueue;
            }

            ConsoleEx.Yellow($"Current Queue:");
            foreach (var item in currentQueue)
            {
                ConsoleEx.Yellow($"{item}");
            }

            return destinations[longest].GetLongestPathRecursive(destinations, currentQueue);
        }
    }

    public class Route
    {
        public Route()
        {

        }
    }
    internal class Day9
    {
        Dictionary<string, float> places = new Dictionary<string, float>();

        Dictionary<string, Destination> destinations = new Dictionary<string, Destination>();
        public Day9(bool newRules = false, bool isTest = false)
        {
            string[] input;
            if (isTest)
            {
                input = File.ReadAllLines("Data/Day9/day9_test.txt"); ;
            }
            else
            {
                input = File.ReadAllLines("Data/Day9/day9.txt");
            }


            //first we need to parse the input
            for (int i = 0; i < input.Length; i++)
            {
                if (ParseHelper.TryParse(input[i], "{destination1} to {destination2} = {distance}", out Dictionary<string, string> output))
                {
                    string dest1Name = output["destination1"];
                    string dest2Name = output["destination2"];
                    int distance = int.Parse(output["distance"]);
                    Destination destination1;
                    Destination destination2;
                    if (!destinations.ContainsKey(dest1Name))
                    {
                        destination1 = new Destination(dest1Name);
                        destinations.Add(dest1Name, destination1);
                    }
                    else
                    {
                        destination1 = destinations[dest1Name];
                    }

                    if (!destinations.ContainsKey(dest2Name))
                    {
                        destination2 = new Destination(dest2Name);
                        destinations.Add(dest2Name, destination2);
                    }
                    else
                    {
                        destination2 = destinations[dest2Name];
                    }

                    destination1.AddDestination(dest2Name, distance);
                    destination2.AddDestination(dest1Name, distance);
                }
            }



            foreach (var dest in destinations)
            {
                ConsoleEx.Blue(dest.Value.ToString());
            }



            (int, Queue<string>) shortest = GetPath();
            (int, Queue<string>) longest = GetPath(true);

            ConsoleEx.Magenta($"Final Shortest Distance: {shortest.Item1}");
            ConsoleEx.Magenta($"Final Path:");
            foreach (var nextDest in shortest.Item2)
            {
                ConsoleEx.Cyan(nextDest);
            }

            ConsoleEx.Magenta($"Final Longest Distance: {longest.Item1}");
            ConsoleEx.Magenta($"Final Path:");
            foreach (var nextDest in longest.Item2)
            {
                ConsoleEx.Cyan(nextDest);
            }
        }

        public (int, Queue<string>) GetPath(bool longestPath = false)
        {
            //shorest path
            Queue<string> path = new Queue<string>();
            int bestDistance = int.MaxValue;
            if (longestPath)
            {
                bestDistance = int.MinValue;
            }

            string bestStart = "";

            Queue<string> bestPath = new Queue<string>();

            List<(string, int)> pathDistances = new List<(string, int)>();
            foreach (var dest in destinations)
            {
                ConsoleEx.Yellow($"Currently At: {dest.Key}");

                path.Clear();
                path.Enqueue(dest.Key);

                var shortest = dest.Value.GetShortestKey();

                Queue<string>? foundPath = null;

                if (longestPath)
                {
                    foundPath = dest.Value.GetLongestPathRecursive(destinations, path);
                }
                else
                {
                    foundPath = dest.Value.GetShortestPathRecursive(destinations, path);
                }


                int totalDistance = 0;

                string output = "";

                string? lastLocation = null;
                foreach (var nextDest in foundPath)
                {
                    if (lastLocation == null)
                    {
                        lastLocation = nextDest;
                        continue;
                    }
                    int dist = destinations[lastLocation].GetDistanceTo(nextDest);
                    output += $"{lastLocation} to {nextDest} = {dist} ... ";
                    totalDistance += dist;
                    lastLocation = nextDest;
                }

                pathDistances.Add((dest.Key, totalDistance));

                ConsoleEx.Green($"Total Distance = {totalDistance}");
                Console.WriteLine($"{output}\n\n");

                if (longestPath)
                {
                    if (totalDistance > bestDistance)
                    {
                        ConsoleEx.Magenta($"Found new shortest distance: {totalDistance}\n\n");

                        bestDistance = totalDistance;
                        bestStart = dest.Key;
                        bestPath = foundPath;
                    }
                }
                else
                {
                    if (totalDistance < bestDistance)
                    {
                        ConsoleEx.Magenta($"Found new longest distance: {totalDistance}\n\n");

                        bestDistance = totalDistance;
                        bestStart = dest.Key;
                        bestPath = foundPath;
                    }
                }
            }

            foreach (var item in pathDistances)
            {
                ConsoleEx.Red($"{item.Item1}: {item.Item2}");
            }

            return (bestDistance, bestPath);
        }

    }
}
