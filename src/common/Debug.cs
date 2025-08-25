public static class ConsoleEx
{
    public static void Red(string output, bool writeLine = true)
    {
        System.Console.ForegroundColor = ConsoleColor.Red;
        if (writeLine)
        {
            System.Console.WriteLine(output);
        }
        else
        {
            System.Console.Write(output);
        }
        System.Console.ResetColor();
    }
    public static void Blue(string output, bool writeLine = true)
    {
        System.Console.ForegroundColor = ConsoleColor.Blue;
        if (writeLine)
        {
            System.Console.WriteLine(output);
        }
        else
        {
            System.Console.Write(output);
        }
        System.Console.ResetColor();
    }
    public static void Green(string output, bool writeLine = true)
    {
        System.Console.ForegroundColor = ConsoleColor.Green;
        if (writeLine)
        {
            System.Console.WriteLine(output);
        }
        else
        {
            System.Console.Write(output);
        }
        System.Console.ResetColor();
    }
    public static void Cyan(string output, bool writeLine = true)
    {
        System.Console.ForegroundColor = ConsoleColor.Cyan;
        if (writeLine)
        {
            System.Console.WriteLine(output);
        }
        else
        {
            System.Console.Write(output);
        }
        System.Console.ResetColor();
    }
    public static void Magenta(string output, bool writeLine = true)
    {
        System.Console.ForegroundColor = ConsoleColor.Magenta;
        if (writeLine)
        {
            System.Console.WriteLine(output);
        }
        else
        {
            System.Console.Write(output);
        }
        System.Console.ResetColor();
    }
    public static void Yellow(string output, bool writeLine = true)
    {
        System.Console.ForegroundColor = ConsoleColor.Yellow;
        if (writeLine)
        {
            System.Console.WriteLine(output);
        }
        else
        {
            System.Console.Write(output);
        }
        System.Console.ResetColor();
    }

    public static void WriteLine(string output)
    {
        Console.WriteLine(output);
    }
    public static void Write(string output)
    {
        Console.Write(output);
    }

    public static string? ReadLine(string lineToDisplay = "", ConsoleColor color = ConsoleColor.White)
    {
        System.Console.ForegroundColor = color;
        System.Console.WriteLine(lineToDisplay);
        System.Console.ResetColor();
        return Console.ReadLine();
    }

    static int waitingAnimIndex;
    static char[] waitingAnim = new char[] { '|', '\\', '-', '/' };
    static char waitingCursor = '|';
    public static void DrawWaitingThing(string extra = "")
    {
        var pos = Console.GetCursorPosition();
        waitingAnimIndex = (waitingAnimIndex + 1) % waitingAnim.Length;
        waitingCursor = waitingAnim[waitingAnimIndex];
        Console.SetCursorPosition(20, 0);
        Console.Write($"{waitingCursor} {extra}");
        Console.SetCursorPosition(pos.Left, pos.Top);
    }
}