namespace PlaySnake;

internal class Game
{
    private const int PlayGroundWidth = 40;
    private const int PlayGroundHeight = 40;
    public static void Start(params string[] args)
    {
        Snake snake = new Snake();
        snake.Length = 5;
        snake.Score = 0;

        Console.Clear();
        ConsoleLayout.SetCursorPosition(GameConsoleWideValues.GameMenu);
        ConsoleLayout.Write();
        ConsoleLayout.Write("Good luck!");
        ConsoleLayout.Write();
        ConsoleLayout.Write("Your snake: " + snake.ToString());
        ConsoleLayout.Write("Score: " + snake.Score);
        ConsoleLayout.Write();
        ConsoleLayout.Write(GameContent.SeparatorLineMax);
        ConsoleLayout.Write();

        while (true)
        {
            Game game = new Game();
            game.DrawArea();

            var keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Q)
                break;

            Move(keyInfo.Key);
        }
    }

    private void DrawArea()
    {
        Point topLeft = new Point(0, 0);
        Point bottomLeft = new Point(0, PlayGroundHeight - 1);

        for (int x = 0; x < PlayGroundHeight; x++)
        {
            var currentPoint = new Point(x, 0);
            if (currentPoint.Equals(topLeft))
            {
                DrawHorizontalBorder();
            }
            else if (currentPoint.Equals(bottomLeft))
            {
                DrawHorizontalBorder();
            }
            DrawVerticalBorder();
            for (int y = 0; y < PlayGroundHeight; y++)
            {
                DrawEmpty();
            }
        }
    }

    private static void Move(ConsoleKey direction)
    {
        switch (direction)
        {
            case ConsoleKey.DownArrow:
                Console.WriteLine("Move Down");
                break;
            case ConsoleKey.UpArrow:
                Console.WriteLine("Move Up");
                break;
            case ConsoleKey.LeftArrow:
                Console.WriteLine("Move Left");
                break;
            case ConsoleKey.RightArrow:
                Console.WriteLine("Move Right");
                break;
            default:
                Console.WriteLine("Invalid Move");
                break;
        }
    }

    private void DrawEmpty()
    {
        Console.Write(" ");
    }

    private void DrawHorizontalBorder()
    {
        for (int i = 0; i < PlayGroundWidth; i++)
        {
            Console.Write("=");
        }
    }

    private void DrawVerticalBorder()
    {
        Console.WriteLine("||");
    }
}
