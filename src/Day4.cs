using System.Security.Cryptography;

namespace AdventOfCode2015
{
    internal class Day4
    {
        MD5 md5 = MD5.Create();
        public Day4(int zeroCount = 5, bool isTest = false)
        {

            string key = "bgvyzdsv";
            if (isTest)
            {
                key = "abcdef";
            }
            for (int i = 0; i < int.MaxValue; i++)
            {

                if (CheckValue(zeroCount, key, i))
                {
                    Console.WriteLine($"Found Value: {i}");
                    return;
                }
            }
            Console.WriteLine($"Tried all values up to {int.MaxValue} and could not find a hash starting with {zeroCount} zeroes.");
        }
        public bool CheckValue(int zeroCount, string key, int value)
        {
            string input = $"{key}{value}";
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            string output = Convert.ToHexString(hashBytes);
            for (int i = 0; i < zeroCount; i++)
            {
                if (output[i] != '0')
                {
                    return false;
                }
            }
            return true;
        }
    }

}
