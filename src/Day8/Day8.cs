using System.Text.RegularExpressions;

namespace AdventOfCode2015.Day8
{

    //Please don't look at the code here XD this is messy because I spent
    //an hour working on the regex and so I didn't clean up after myself XD
    class Day8
    {
        public Day8(bool newRules = false, bool isTest = false)
        {
            string[] input;
            if (isTest)
            {
                input = File.ReadAllLines("Data/Day8/day8_test.txt"); ;
            }
            else
            {
                input = File.ReadAllLines("Data/Day8/day8.txt");
            }
            int unescapedLength = 0;
            int escapedLength = 0;
            int totalLength = 0;

            int reescapedLength = 0;
            for (int i = 0; i < input.Length; i++)
            {
                //escaped length is the input length
                escapedLength += input[i].Length;



                //start modding
                ConsoleEx.Yellow($"Modifying: {input[i]}");

                //first we trim the line to remove the double quotes ""
                var trimmed = input[i].Trim('"');
                ConsoleEx.Blue($"trimmed: {trimmed}");


                //then let's handle \\
                var fixSlashes = trimmed.Replace(@"\\", "\\");
                ConsoleEx.Blue($"fixSlashes: {fixSlashes}");

                //then we'll handle \"
                var fixDoubleQuote = fixSlashes.Replace(@"\""", "\"");
                ConsoleEx.Blue($"fixDoubleQuote: {fixDoubleQuote}");


                //then we'll handle the hex things
                var replaceHex = Regex.Replace(fixDoubleQuote,
                    @"\\x([A-Fa-f0-9]{2})",
                    new MatchEvaluator(
                        (Match match) =>
                        {
                            var st = match.ToString();
                            Console.WriteLine(st);
                            ConsoleEx.Yellow(Regex.Unescape(st));
                            return Regex.Unescape(st);
                        }));

                //the final string
                ConsoleEx.Blue($"Final:{replaceHex}");

                //add the length after all this to the unescaped length
                unescapedLength += replaceHex.Length;

                Console.WriteLine("");
                Console.WriteLine("");


                //Part 2
                Console.WriteLine($"Now Reescaping the original input: {input[i]}");

                //start modding
                var newEscaped = input[i];

                //escape slashes
                newEscaped = newEscaped.Replace(@"\", @"\\");
                ConsoleEx.Blue($"Slashes: {newEscaped}");

                //escape double quotes
                newEscaped = newEscaped.Replace("\"", @"\""");
                ConsoleEx.Blue($"DoubleQuotes: {newEscaped}");

                //add double quotes to the outside
                newEscaped = $"\"{newEscaped}\"";
                ConsoleEx.Blue($"Wrapped: {newEscaped}");

                //add the length
                reescapedLength += newEscaped.Length;

                //the final string
                ConsoleEx.Green($"Escaped String: {newEscaped}");
                Console.WriteLine("");
                Console.WriteLine("");
            }

            ConsoleEx.Green($"Total length: {escapedLength} - {unescapedLength} = {(escapedLength - unescapedLength)}");
            Console.WriteLine($"Total length after Re-Escaping:{reescapedLength} - {escapedLength} = {reescapedLength - escapedLength}");
            //Console.WriteLine($"Total length: {totalLength}");
        }
    }
}
