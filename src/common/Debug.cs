public static class ConsoleEx
{
    public static void Red(string output)
    {
        System.Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine(output);
        System.Console.ResetColor();
    }
    public static void Blue(string output)
    {
        System.Console.ForegroundColor = ConsoleColor.Blue;
        System.Console.WriteLine(output);
        System.Console.ResetColor();
    }
    public static void Green(string output)
    {
        System.Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine(output);
        System.Console.ResetColor();
    }
    public static void Cyan(string output)
    {
        System.Console.ForegroundColor = ConsoleColor.Cyan;
        System.Console.WriteLine(output);
        System.Console.ResetColor();
    }
    public static void Magenta(string output)
    {
        System.Console.ForegroundColor = ConsoleColor.Magenta;
        System.Console.WriteLine(output);
        System.Console.ResetColor();
    }
    public static void Yellow(string output)
    {
        System.Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine(output);
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

    public static string? ReadLine()
    {
        return Console.ReadLine();
    }
}