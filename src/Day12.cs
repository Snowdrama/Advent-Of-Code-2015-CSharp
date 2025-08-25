using System.Text.Json.Nodes;

namespace AdventOfCode2015
{
    internal class Day12
    {
        public Day12(bool newRules = false, bool isTest = false)
        {
            string input;
            if (isTest)
            {
                input = File.ReadAllText("Data/Day12/day12_test.json"); ;
            }
            else
            {
                input = File.ReadAllText("Data/Day12/day12.json");
            }

            JsonNode? jsonObject = JsonNode.Parse(input);

            if (jsonObject == null)
            {
                ConsoleEx.Red($"JSON failed to parse");
                return;
            }

            int value = 0;
            if (newRules)
            {
                //In AOC Day 12 Part 2 we want to exclude red
                value = SumNumbers(jsonObject, new List<String>() { "red" });
            }
            else
            {
                value = SumNumbers(jsonObject);
            }
            Console.WriteLine($"The sum of all numbers in the JSON is: {value}");
        }

        //recurse down the json tree and sum all numbers ignoring any object with "red"
        private int SumNumbers(JsonNode node, List<string> exclusionList = null)
        {
            //we added an exclusions list so we can exclude objects if they contain
            //any values that we don't want. 
            if (exclusionList != null)
            {
                if (node is JsonObject obj)
                {
                    foreach (var property in obj)
                    {
                        if (property.Value is JsonValue jvalue &&
                            jvalue.TryGetValue<string>(out string? strValue) &&
                            strValue != null &&
                            exclusionList.Contains(strValue)
                            )
                        {
                            return 0;
                        }
                    }
                }
            }

            //now sum normally
            int sum = 0;
            if (node is JsonValue value)
            {
                //it's an int value so we should just return the value
                if (value.TryGetValue<int>(out int intValue))
                {
                    return intValue;
                }
            }
            else if (node is JsonArray array)
            {
                //it's an array so we should search through each item
                //to see if the array contains objects or nested arrays
                foreach (var item in array)
                {
                    sum += SumNumbers(item!);
                }
            }
            else if (node is JsonObject sumObj)
            {

                //it's an object so we should iterate over each property
                //and then see if that's an array or object.
                foreach (var property in sumObj)
                {
                    sum += SumNumbers(property.Value!);
                }
            }
            //if it was an object or array we should return the sum we found
            return sum;
        }
    }
}
