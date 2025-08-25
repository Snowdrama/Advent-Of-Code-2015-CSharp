using System.Diagnostics;
using System.Text;

namespace AdventOfCode2015
{
    internal class Day10
    {
        public Day10(bool newRules = false, bool isTest = false)
        {

            var input = ConsoleEx.ReadLine($"Input the input[example: 3113322113]:", ConsoleColor.Blue);
            while (input.Any(x => char.IsLetter(x)))
            {
                input = ConsoleEx.ReadLine($"Needs to be all numbers[example: 3113322113]:", ConsoleColor.Red);
            }



            int iterations = 40;
            var iterationString = ConsoleEx.ReadLine($"Input the number of iterations[example: 10]:", ConsoleColor.Blue);
            while (!int.TryParse(iterationString, out iterations))
            {
                iterationString = ConsoleEx.ReadLine($"Try again please, a NUMBER only[example: 10]:", ConsoleColor.Red);
            }


            //var word = input;
            //for (int x = 0; x < iterations; x++)
            //{
            //    var newWord = new StringBuilder(10000000);
            //    char tempN = word[0];
            //    int tempC = 1;

            //    for (int k = 1; k < word.Length; k++)
            //    {
            //        if (word[k] == tempN)
            //        {
            //            tempC++;
            //        }
            //        else
            //        {
            //            newWord.Append($"{tempC}{tempN}");
            //            tempC = 1;
            //            tempN = word[k];
            //        }
            //    }
            //    newWord.Append($"{tempC}{tempN}");
            //    word = newWord.ToString();
            //}
            //ConsoleEx.Cyan($"Final Output: {word.Length}");

            //return;






            return;











            var finalOutput = input;
            var currentInput = new char[500_000_000];
            var currentOutput = new char[500_000_000];


            ConsoleEx.Yellow("Copying Input to output");
            for (int z = 0; z < finalOutput.Length; z++)
            {
                ConsoleEx.Yellow($"{finalOutput[z]}");
                currentInput[z] = finalOutput[z];
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < iterations; i++)
            {
                int currentIndex = 0;
                for (int c = 0; c < currentInput.Length; c++)
                {
                    ConsoleEx.DrawWaitingThing($"{sw.ElapsedMilliseconds / 1000.0:F2}");
                    char currentChar = currentInput[c];
                    if (char.IsControl(currentChar))
                    {
                        break;
                    }

                    // take the character and count how many times it appears in a row
                    int charCount = 1;
                    while (c + 1 < currentInput.Length && currentInput[c + 1] == currentChar)
                    {
                        charCount++;
                        c++;
                    }
                    //ConsoleEx.Yellow($"Char[c]: {currentChar} charCount: {charCount}");
                    // Output the character and its count

                    var toAdd = $"{charCount}{currentChar}";
                    //ConsoleEx.Yellow($"toAdd: {toAdd}");

                    for (int x = 0; x < toAdd.Length; x++)
                    {
                        currentOutput[currentIndex] = toAdd[x];
                        currentIndex++;
                    }
                    //Console.WriteLine($"{count}{currentChar}");
                }

                currentOutput.CopyTo(currentInput, 0);
                ConsoleEx.Blue($"Time: {sw.ElapsedMilliseconds * 0.0001:F2}");
                sw.Restart();
                ConsoleEx.Magenta($"Finished iteration {i}, Length: {currentIndex}, copying output to input");


                //Console.WriteLine($"New Characters: {currentOutput}");
            }

            finalOutput = "";
            for (int z = 0; z < currentInput.Length; z++)
            {
                if (char.IsControl(currentInput[z]))
                {
                    break;
                }
                finalOutput += currentInput[z];
            }
            //ConsoleEx.Cyan($"Final Output: {finalOutput}");
            ConsoleEx.Cyan($"Final Output: {finalOutput.Length}");


            // Constructor logic can be added here if needed
        }

        public static void Run(string input, int runCount = 1)
        {
            if (input.Length <= 0)
            {
                return;
            }

            string current = input;
            for (int i = 0; i < runCount; i++)
            {
                current = RunInternal(current);
            }

            Console.WriteLine(current);
            Console.WriteLine(current.Length);
        }

        private static string RunInternal(string input)
        {
            StringBuilder output = new StringBuilder();
            char lastCharacter = input[0];
            int lastSwitch = 0;

            for (int i = 0; i < input.Length; i++)
            {
                char currentCharacter = input[i];

                if (currentCharacter != lastCharacter)
                {
                    output.Append((i - lastSwitch).ToString());
                    output.Append(lastCharacter);

                    lastCharacter = currentCharacter;
                    lastSwitch = i;
                }
            }

            output.Append((input.Length - lastSwitch).ToString());
            output.Append(input[input.Length - 1]);

            return output.ToString();
        }

    }
}
