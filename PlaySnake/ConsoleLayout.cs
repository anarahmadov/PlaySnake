namespace PlaySnake;

internal static class ConsoleLayout
{
    private static int _currentLeft;   

    public static void Write()
    {
        SetCursorPosition(_currentLeft);
        Console.WriteLine();
    }

    public static void Write(string text)
    {
        SetCursorPosition(text.Length);
        Console.WriteLine(text);
    }

    public static void SetCursorPosition(int length)
    {
        int left = CalculateLeftPosition(length);
        Console.SetCursorPosition(left, Console.CursorTop);
        _currentLeft = left;
    }

    public static void SetCursorPosition(GameConsoleWideValues value)
    {
        int left = CalculateLeftPosition((int)value);
        Console.SetCursorPosition(left, Console.CursorLeft);
        _currentLeft = left;
    }

    private static int CalculateLeftPosition(int length)
    {
        return Math.Max((Console.WindowWidth - length) / 2, 0);
    }

    public static void WriteHorizontallyCenteredBlock(params string[] lines)
    {
        int width = Console.WindowWidth;
        int top = Console.CursorTop;

        foreach (var line in lines)
        {
            int left = Math.Max((width - line.Length) / 2, 0);

            Console.SetCursorPosition(left, top);
            Console.Write(line);

            top++;
        }
    }
}

