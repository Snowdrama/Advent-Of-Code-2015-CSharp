using AdventOfCode2015;
using AdventOfCode2015.Day7;
using AdventOfCode2015.Day8;
using AdventOfCode2015.Day9;
public class Program
{
    public static void Main(string[] args)
    {
        bool running = true;
        Console.WriteLine("Advent of Code 2015!");

        while (running)
        {
            Console.WriteLine("\nChoose a Day:(type 'list' for all days or 'exit' to quit)");
            string? input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("No input provided. Please enter a valid day number or 'exit' to quit.");
                continue;
            }

            bool isTest = false;
            if (input.ToLowerInvariant().Contains("--test"))
            {
                isTest = true;
            }

            //remove any leading or trailing whitespace and --test from the input
            input = input.ToLowerInvariant().Replace("--test", "").Trim();

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
                Console.WriteLine("1 --- Day 1");
                Console.WriteLine("1.2 - Day 1 Part 2");
                Console.WriteLine("2 --- Day 2 Part 1 & 2");
                Console.WriteLine("3 --- Day 3");
                Console.WriteLine("3.2 - Day 3 Part 2");
                // Add more days as needed
                Console.WriteLine("If you want to run a test, type the day number followed by --test (e.g., 1.2 --test).");
                Console.WriteLine("Type the day number to run it or 'exit' to quit.");
            }
            else if (float.TryParse(input, out float dayNumber))
            {
                float day = float.Parse(input);
                switch (day)
                {
                    case 1.0f:
                    case 1.1f:
                        Day1 day1 = new Day1(isTest);
                        break;

                    case 1.2f:
                        Day1 day1_part2 = new Day1(findBasement: true, isTest);
                        break;

                    case 2.0f:
                    case 2.1f:
                        Day2 day2 = new Day2(false, isTest);
                        break;
                    case 2.2f:
                        Day2 day2Ribbon = new Day2(true, isTest);
                        break;

                    case 3.0f:
                    case 3.1f:
                        Day3 day3 = new Day3(false, isTest);
                        break;
                    case 3.2f:
                        Day3 day3Robo = new Day3(true, isTest);
                        break;

                    case 4:
                    case 4.1f:
                        Day4 day4 = new Day4(5, isTest);
                        break;
                    case 4.2f:
                        Day4 day4SixZeros = new Day4(6, isTest);
                        break;

                    case 5:
                    case 5.1f:
                        Day5 day5 = new Day5(false, isTest);
                        break;
                    case 5.2f:
                        Day5 day5newRules = new Day5(true, isTest);
                        break;

                    case 6:
                    case 6.1f:
                        Day6 day6 = new Day6(false, isTest);
                        break;
                    case 6.2f:
                        Day6 day6newRules = new Day6(true, isTest);
                        break;

                    case 7:
                    case 7.1f:
                        Day7 day7 = new Day7(false, isTest);
                        break;
                    case 7.2f:
                        Day7 day7newRules = new Day7(true, isTest);
                        break;

                    case 8:
                    case 8.1f:
                        Day8 day8 = new Day8(false, isTest);
                        break;
                    case 8.2f:
                        Day8 day8newRules = new Day8(true, isTest);
                        break;
                    case 9:
                    case 9.1f:
                        Day9 day9 = new Day9(false, isTest);
                        break;
                    case 9.2f:
                        Day9 day9newRules = new Day9(true, isTest);
                        break;


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

