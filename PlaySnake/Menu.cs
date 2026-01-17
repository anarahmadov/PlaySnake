namespace PlaySnake;

public static class Menu
{
    public static void MainMenu()
    {
        do
        {
            Console.Clear();

            #region old version
            ConsoleLayout.WriteHorizontallyCenteredBlock(
                "                          ",
                "START GAME ------------- 1",
                "INSTRUCTIONS ----------- 2",
                "CUSTOMIZATION ---------- 3",
                "TEST AREA -------------- T",
                "QUIT ------------------- 4");
            #endregion

            var keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    StartGame();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    ShowInstructions();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    CustomizationMenu();
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    Quit();
                    break;
                default:
                    Console.WriteLine();
                    Console.Write("Invalid selection. Please try again.");
                    Console.WriteLine();
                    Thread.Sleep(1500);
                    continue;
            }
        } while (true);
    }

    public static void CustomizationMenu()
    {
        Console.Clear();

        Console.WriteLine("CUSTOMIZATION MENU");
        Console.WriteLine();
        Console.WriteLine("1. Change Snake Color");
        Console.WriteLine("2. Change Game Speed");
        Console.WriteLine("3. Back to Main Menu");

        var keyInfo = Console.ReadKey(true);
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

    public static void ShowInstructions()
    {
        Console.Clear();
        Console.WriteLine("=== Instructions ===");
        Console.WriteLine("Use the arrow keys to control the snake.");
        Console.WriteLine("Eat food to grow longer and increase your score.");
        Console.WriteLine("Avoid running into walls or yourself.");
        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey(true);
    }

    public static void Quit()
    {
        Console.Clear();
        Console.WriteLine("Thank you for playing PlaySnake! Goodbye!");
        Thread.Sleep(1000);
        Environment.Exit(0);
    }
}
