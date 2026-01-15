namespace PlaySnake;

public static class Menu
{
    public static void MainMenu()
    {
        Console.Clear();

        Console.WriteLine("=====================================");
        Console.WriteLine("----- WELCOME TO PLAY SNAKE !!! -----");
        Console.WriteLine("=====================================");
        Console.WriteLine();

        Console.WriteLine("Press any key to start the game...");
        Console.WriteLine();

        Console.ReadKey();
        Console.WriteLine();

        Console.WriteLine("START GAME ------------- 1");
        Console.WriteLine("INSTRUCTIONS ----------- 2");
        Console.WriteLine("CUSTOMIZATION ---------- 3");
        Console.WriteLine("QUIT ------------------- 4");

        var keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
            case ConsoleKey.D1:
            case ConsoleKey.NumPad1:
                Game.Start();
                break;
            case ConsoleKey.D2:
            case ConsoleKey.NumPad2:
                ShowInstructions();
                break;
            case ConsoleKey.D3:
            case ConsoleKey.NumPad3:
                Customize();
                break;
            case ConsoleKey.D4:
            case ConsoleKey.NumPad4:
                Quit();
                break;
            default:
                Console.Write("Invalid selection. Please try again.");
                break;
        }
    }

    public static void Customize()
    {
        Console.Clear();

        Console.WriteLine("CUSTOMIZATION MENU");
        Console.WriteLine("1. Change Snake Color");
        Console.WriteLine("2. Change Game Speed");
        Console.WriteLine("3. Back to Main Menu");

        var keyInfo = Console.ReadKey();
        switch (keyInfo.Key)
        {
            case ConsoleKey.D1:
            case ConsoleKey.NumPad1:
                Console.Write("Feature to change snake color coming soon!");
                break;
            case ConsoleKey.D2:
            case ConsoleKey.NumPad2:
                Console.Write("Feature to change game speed coming soon!");
                break;
            case ConsoleKey.D3:
            case ConsoleKey.NumPad3:
                return;
            default:
                Console.Write("Invalid selection. Please try again.");
                break;
        }
    }

    public static void Quit()
    {
        Console.Clear();
        Console.WriteLine("Thank you for playing PlaySnake! Goodbye!");
        Thread.Sleep(2000);
        Environment.Exit(0);
    }

    public static void ShowInstructions()
    {
        Console.Clear();
        Console.WriteLine("=== Instructions ===");
        Console.WriteLine("Use the arrow keys to control the snake.");
        Console.WriteLine("Eat food to grow longer and increase your score.");
        Console.WriteLine("Avoid running into walls or yourself.");
        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey();
    }
}
