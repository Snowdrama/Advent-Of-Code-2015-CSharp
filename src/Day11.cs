namespace AdventOfCode2015
{
    internal class Day11
    {
        //password can only use these letters
        //i, o, and l are removed
        //this makes sure we never use invalid characters
        string passChars = "abcdefghijklmnopqrstuvwxyz";
        string invalidChars = "ilo";

        public Day11()
        {
            var input = ConsoleEx.ReadLine($"Input the current password[example: hepxcrrq]:", ConsoleColor.Blue);
            while (input.Any(x => !char.IsLetter(x)))
            {
                input = ConsoleEx.ReadLine($"Needs to be all characters[example: hepxcrrq]:", ConsoleColor.Red);
            }

            ConsoleEx.Cyan($"Next valid password: {GetNextPassword(input)}");
        }

        private string GetNextPassword(string input)
        {
            //we always increase once because we need to get a new pass
            input = IncrementPassword(input);
            while (!IsValidPassword(input))
            {
                input = IncrementPassword(input);
            }
            return input;
        }

        private string IncrementPassword(string password)
        {
            char[] pass = password.ToCharArray();
            for (int i = pass.Length - 1; i >= 0; i--)
            {
                //get the index of the current character
                int index = passChars.IndexOf(pass[i]);

                //increment the index
                index++;

                if (index >= passChars.Length)
                {
                    //wrap around if it's after z back to A
                    pass[i] = passChars[0];

                    //continue to go to the next character and increment it
                    continue;
                }
                else
                {
                    //set the character to the next one in the allowed list
                    pass[i] = passChars[index];

                    //break the for loop since we don't need to increment the next digit
                    break;
                }
            }
            return string.Concat(pass);
        }

        private bool IsValidPassword(string password)
        {
            bool hasStraight = false;
            int pairCount = 0;

            char[] chars = password.ToCharArray();

            if (chars.Any(x => invalidChars.Contains(x)))
            {
                //fail due to having an invalid character
                return false;
            }

            for (int i = 0; i < password.Length; i++)
            {
                string segment = password.Substring(i, Math.Min(3, password.Length - i));
                if (segment.Length < 3)
                {
                    continue;
                }

                int indexOf1 = passChars.IndexOf(segment[0]);
                int indexOf2 = passChars.IndexOf(segment[1]);
                int indexOf3 = passChars.IndexOf(segment[2]);

                //check for straight
                if (indexOf1 + 1 == indexOf2 && indexOf2 + 1 == indexOf3)
                {
                    hasStraight = true;
                    break;
                }
            }

            if (!hasStraight)
            {
                //fail due to not having any straights
                return false;
            }


            for (int i = 0; i < password.Length; i++)
            {
                string segment = password.Substring(i, Math.Min(2, password.Length - i));
                if (segment.Length < 2)
                {
                    continue;
                }
                int indexOf1 = passChars.IndexOf(segment[0]);
                int indexOf2 = passChars.IndexOf(segment[1]);
                //check for straight
                if (indexOf1 == indexOf2)
                {
                    pairCount++;
                    //if it's a pair we need to advance twice
                    //because we don't want them overlapping
                    //(aaa) is not (aa+aa)
                    //so advance i one more
                    i++;
                }
            }

            if (pairCount < 2)
            {
                //fail due to having 0 or 1 pair
                return false;
            }

            return true;
        }

    }
}
