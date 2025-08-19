namespace AdventOfCode2015
{
    public class Day1
    {
        public Day1(bool findBasement = false, bool isTest = false)
        {
            string input;
            if (isTest)
            {
                input = System.IO.File.ReadAllText("Data/Day1/day1_test.txt"); ;
            }
            else
            {
                input = System.IO.File.ReadAllText("Data/Day1/day1.txt");
            }

            int floor = 0;
            Console.WriteLine($"Number of total instructions is: {input.Length}");
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    floor++;
                }
                else if (input[i] == ')')
                {
                    floor--;
                }
                else
                {
                    Console.WriteLine($"Invalid character found at pos {i}: {input[i]}");
                    continue;
                }
                if (findBasement)
                {
                    if (floor == -1)
                    {
                        Console.WriteLine("Basement entered at position: " + (i + 1));
                        return;
                    }
                }
            }
            Console.WriteLine("Final floor found: " + floor);

        }
    }
}
