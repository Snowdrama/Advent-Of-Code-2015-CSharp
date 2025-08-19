using AdventOfCode2015;
public class Program
{
    public static void Main(string[] args)
    {
        bool running = true;
        Console.WriteLine("Advent of Code 2015!");

        while (running)
        {
            Console.WriteLine("\nChoose a Day:(type 'list' for all days or 'exit' to quit)");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("No input provided. Exiting.");
            }
            else if (input.ToLower() == "exit")
            {
                running = false;
                Console.WriteLine("Exiting the program.");
            }
            else if (input.ToLower() == "list")
            {
                Console.WriteLine("Available days:");
                Console.WriteLine("1.1 Day 1");
                Console.WriteLine("1.2 Day 1 Part 2");
                // Add more days as needed
                Console.WriteLine("Type the day number to run it or 'exit' to quit.");
            }
            else if (input.ToLower() == "exit")
            {
                running = false;
                Console.WriteLine("Exiting the program.");
            }
            else if (float.TryParse(input, out float dayNumber))
            {
                float day = float.Parse(input);
                switch (day)
                {
                    case 1.0f:
                    case 1.1f:
                        Day1 day1 = new Day1();
                        break;

                    case 1.2f:
                        Day1 day1_part2 = new Day1(findBasement: true);
                        break;

                    case 2.0f:
                        Day2 day2 = new Day2();
                        break;
                    // Add cases for other days as needed
                    default:
                        Console.WriteLine("Invalid input. Please enter a valid day number or 'exit' to quit.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid day number or 'exit' to quit.");
            }
        }
    }
}

