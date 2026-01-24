using System.Text;

namespace PlaySnake;

internal class Game
{
    private const int PlayGroundWidth = 100;
    private const int PlayGroundHeight = 50;

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

            //Move(keyInfo.Key);
        }
    }

    private void DrawArea()
    {
        var topLeft = new Point(0, 0);
        var bottomLeft = new Point(0, PlayGroundHeight / 2 - 1);

        for (int x = 0; x < PlayGroundHeight; x++)
        {
            var currentPoint = new Point(x, 0);

            if (currentPoint.Equals(topLeft))
                DrawHorizontalBorder();
            if (currentPoint.Equals(bottomLeft))
                DrawHorizontalBorder();

            DrawLine(PlayGroundWidth);
        }
    }

    private void DrawLine(int width)
    {
        var emptyLine = new StringBuilder("|");

        for (int i = 0; i < width - 2; i++)
            emptyLine.Append(" ");

        emptyLine.Append("|");
        Console.WriteLine(emptyLine.ToString());
    }

    private void DrawHorizontalBorder()
    {
        for (int i = 0; i < PlayGroundWidth; i++)
        {
            if (i == PlayGroundWidth - 1)
                Console.Write("_");

            Console.Write("_");
        }
    }

    private void DrawVerticalBorder()
    {
        Console.Write("|");
    }
}
