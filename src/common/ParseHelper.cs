public class ParseHelper
{
    public static bool TryParse(string input, string format, out Dictionary<string, string> output)
    {
        output = new Dictionary<string, string>();

        var openBracketCount = format.Count(x => x == '{');
        var closeBracketCount = format.Count(x => x == '}');
        if (openBracketCount != closeBracketCount)
        {
            throw new ArgumentException("Format string needs equal numbers of { and } enclosing the keys");
        }


        //keys are the things inside the {}
        var keys = new List<string>();
        //lines are the things outside the {} separated by {}
        var lines = new List<string>();

        //example:
        // "Hi I'm John, I like Pepperoni Pizza and Video Games!"
        // "Hi I'm {name}, I like {food} and {activity}!"
        // Keys:
        // name
        // food
        // activity
        //
        // Lines:
        // "Hi I'm "
        // ", I like "
        // " and "
        // "!"

        //iterate over each character looking for the first open bracket
        int pos = 0;
        int start = 0;
        int end = 0;
        while (pos < format.Length)
        {
            if (format[pos] == '{')
            {

                //mark the end as the character before the {
                end = pos;

                ConsoleEx.Blue($"found open, getting line... {format.Substring(start, end - start)}");
                //add the line range we currently have
                lines.Add(format.Substring(start, end - start));

                //the new start is the character after the {
                start = pos + 1;
            }

            if (format[pos] == '}')
            {
                //mark the end as the character before the }
                end = pos;

                ConsoleEx.Blue($"found close, getting key... {format.Substring(start, end - start)}");
                //add the key as the range we currently have
                keys.Add(format.Substring(start, end - start));

                //the new start is the character after the }
                start = pos + 1;
            }

            pos++;

            //this check has to happen after the pos++
            //because the next loop will exit on this pos
            if (pos == format.Length)
            {
                //special case for the end of the line
                end = pos;

                ConsoleEx.Blue($"end of string, getting line... {format.Substring(start, end - start)}");

                //add the line range we currently have
                lines.Add(format.Substring(start, end - start));
            }
        }

        lines = lines.Where(x => !string.IsNullOrEmpty(x)).ToList();
        ConsoleEx.Cyan($"Splitting string '{input}' by:");
        for (int i = 0; i < lines.Count; i++)
        {
            ConsoleEx.Green($"Lines[{i}]: {lines[i]}");
        }

        //we sort the lines by most specific to least specific
        //this prevents splitting by empty space before strings with spaces
        lines = lines.OrderByDescending(x => x.Length).ToList();
        //split input by lines
        //we need to trim empty splits in the case of the start or end being a line
        //it will result in the first/last value splitting and being empty
        var split = input.Split(lines.ToArray(), StringSplitOptions.TrimEntries).Where(x => !string.IsNullOrEmpty(x)).ToList();

        ConsoleEx.Cyan($"Split input line '{input}':");
        for (int j = 0; j < split.Count; j++)
        {
            ConsoleEx.Green($"Split[{j}]: {split[j]}");
        }

        //after removing empty the key and split count should be equal...
        if (split.Count != keys.Count)
        {
            for (int j = 0; j < lines.Count; j++)
            {
                ConsoleEx.Red($"Lines[{j}]: {lines[j]}");
            }

            for (int j = 0; j < split.Count; j++)
            {
                ConsoleEx.Red($"Split[{j}]: {split[j]}");
            }

            for (int j = 0; j < keys.Count; j++)
            {
                ConsoleEx.Red($"Keys[{j}]: {keys[j]}");
            }
            throw new ArgumentException($"Split count {split.Count} != key count {keys.Count}. " +
                $"Do you have any {{{{overlapping}}keys}} or {{keys}}{{with}}{{no}}{{characters}}{{between}}?");
        }

        //then we can assign to output!
        for (int j = 0; j < keys.Count; j++)
        {
            output.Add(keys[j], split[j]);
        }
        return true;
    }
}
