namespace AdventOfCode2015
{
    internal class Day5
    {
        string[] naughtyWordSegments = new string[] { "ab", "cd", "pq", "xy" };
        char[] vowelList = new char[] { 'a', 'e', 'i', 'o', 'u' };

        public Day5(bool newRules = false, bool isTest = false)
        {
            string[] input;
            if (isTest)
            {
                input = System.IO.File.ReadAllLines("Data/Day5/day5_test.txt"); ;
            }
            else
            {
                input = System.IO.File.ReadAllLines("Data/Day5/day5.txt");
            }

            int niceWordCount = 0;
            int naughtyWordCount = 0;
            for (int i = 0; i < input.Length; i++)
            {
                string word = input[i];
                ConsoleEx.WriteLine($"\nTesting: {word}");

                if (newRules)
                {
                    if (CheckLetterPairs(word) && CheckPalindrome(word))
                    {
                        ConsoleEx.Green($"{word} is a Nice Word!!!");
                        niceWordCount++;
                    }
                    else
                    {
                        ConsoleEx.Red($"{word} is a Naughty Word...");
                        naughtyWordCount++;
                    }
                }
                else
                {
                    if (CheckDoubleLetter(word) && CheckNaughtySegment(word) && CheckVowelCount(word))
                    {
                        ConsoleEx.Green($"{word} is a Nice Word!!!");
                        niceWordCount++;
                    }
                    else
                    {
                        ConsoleEx.Red($"{word} is a Naughty Word...");
                        naughtyWordCount++;
                    }
                }
            }
            ConsoleEx.Green($"There are {niceWordCount} Nice words in the list");
            ConsoleEx.Red($"There are {naughtyWordCount} Naughty words in the list");

        }

        private bool CheckDoubleLetter(string word)
        {
            for (int i = 0; i < word.Length - 1; i++)
            {
                if (word[i] == word[i + 1])
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckLetterPairs(string word)
        {
            List<string> letterPairs = new List<string>();
            for (int i = 0; i < word.Length - 1; i++)
            {
                var letterPair = $"{word[i]}{word[i + 1]}";
                var firstIndex = word.IndexOf(letterPair);
                var lastIndex = word.LastIndexOf(letterPair);
                if (firstIndex != lastIndex && lastIndex - firstIndex >= 2)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckPalindrome(string word, int palindromeLength = 3)
        {
            for (int i = 0; i < word.Length - (palindromeLength - 1); i++)
            {
                bool isPalindrome = true;
                string currentPalindrome = "";

                for (int c = 0; c < palindromeLength; c++)
                {
                    var c1 = word[i + c];
                    var c2 = word[i + ((palindromeLength - 1) - c)];
                    if (c1 == c2)
                    {
                        currentPalindrome += c1;
                    }
                    else
                    {
                        isPalindrome = false;
                        break;
                    }
                }

                if (isPalindrome)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckNaughtySegment(string word)
        {
            foreach (var segment in naughtyWordSegments)
            {
                if (word.Contains(segment))
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckVowelCount(string word, int vowelCount = 3)
        {
            int vowels = word.Count(w => vowelList.Contains(w));
            if (vowels >= vowelCount)
            {
                return true;
            }
            return false;
        }
    }
}
