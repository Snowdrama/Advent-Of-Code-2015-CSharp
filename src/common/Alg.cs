namespace AdventOfCode2015.src.common
{
    public class Alg
    {
        //Uses heap's algorithm to get all permutations without recursion
        //https://en.wikipedia.org/wiki/Heap%27s_algorithm
        public static List<List<T>> GetPermutations<T>(List<T> input)
        {
            var permutations = new List<List<T>>();
            var items = new List<T>(input);
            int count = items.Count;

            var indexes = new int[count];
            ConsoleEx.Cyan($"Generating permutations for {count} items...");

            // The first permutation is the input order
            permutations.Add(new List<T>(items));

            int position = 0;
            while (position < count)
            {
                ConsoleEx.Blue($"indexes[position] < position = {indexes[position]} < {position}");
                if (indexes[position] < position)
                {
                    // If position is even, swap with first element
                    // If position is odd, swap with indexes[position]
                    if (position % 2 == 0)
                    {
                        ConsoleEx.Magenta($"Even! Swapping {0} and {position}");
                        Swap(items, 0, position);
                    }
                    else
                    {
                        ConsoleEx.Magenta($"Odd! Swapping {indexes[position]} and {position}");
                        Swap(items, indexes[position], position);
                    }

                    // Store the new permutation
                    permutations.Add(new List<T>(items));

                    indexes[position] += 1;
                    position = 0; // Restart from the beginning
                }
                else
                {
                    ConsoleEx.Magenta($"Setting indexes[{position}]: 0");
                    indexes[position] = 0;
                    ConsoleEx.Magenta($"Incrementing position from {position} to {position + 1}");
                    position++;
                }
                ConsoleEx.Cyan($"Indexes: [{string.Join(",", indexes)}]");
            }

            return permutations;
        }

        private static void Swap<T>(List<T> list, int i, int j)
        {

            ConsoleEx.Red($"list[{i}]: {list[i]}");
            ConsoleEx.Red($"list[{j}]: {list[j]}");
            ConsoleEx.Red($"Old List: {string.Join(",", list.ToArray())}");
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
            ConsoleEx.Red($"Old List: {string.Join(",", list.ToArray())}");
        }
    }
}
