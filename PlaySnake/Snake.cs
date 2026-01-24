namespace PlaySnake;

internal static class Snake
{
    private static List<(int, int)> _bodyCoords = new();
    private static (int, int) _tailCoord;
    private static (int, int) _headCoord;
    private static (int, int) _initialTailCoord = (45, 25);
    private static List<(int, int)> BodyCoords
    {
        get => _bodyCoords;
        set
        {
            value = _bodyCoords;
            _headCoord = _bodyCoords[^1];
            _tailCoord = _bodyCoords[0];
        }
    }

    public const int Length = 5;
    public static int Score { get; set; }
    private static string _previousDirection = "RIGHT";
    private static string _currentDirection = "RIGHT";
    private static int _bodyDrawed = 0;
    private static int _currentBodyIndex = 0;

    static Snake()
    {
        _bodyDrawed = Length;
        _currentBodyIndex = Length - 1;
        PopulateSnakeBody();
        Console.SetCursorPosition(50 - Length, 25);
        Console.Write(new string('.', Length));
    }

    public static void Run()
    {
        new Thread(InputHandler).Start();

        while (true)
        {
            Thread.Sleep(500);
            KillSnake(_bodyCoords);
            Move();
            DrawBody(_bodyCoords);
        }
    }

    private static void InputHandler()
    {
        while (true)
        {
            var consoleKey = Console.ReadKey(true).Key;
            switch (consoleKey)
            {
                case ConsoleKey.UpArrow:
                    if (_currentDirection != "DOWN")
                        _currentDirection = "UP";
                    break;
                case ConsoleKey.DownArrow:
                    if (_currentDirection != "UP")
                        _currentDirection = "DOWN";
                    break;
                case ConsoleKey.LeftArrow:
                    if (_currentDirection != "RIGHT")
                        _currentDirection = "LEFT";
                    break;
                case ConsoleKey.RightArrow:
                    if (_currentDirection != "LEFT")
                        _currentDirection = "RIGHT";
                    break;
                default:
                    break;
            }
        }
    }

    private static void Move()
    {
        if (_previousDirection == _currentDirection && _bodyDrawed == Length)
        {
            for (int i = 0; i < _bodyCoords.Count; i++)
            {
                _bodyCoords[i] = _currentDirection switch
                {
                    "UP" => (_bodyCoords[i].Item1, _bodyCoords[i].Item2 - 1),
                    "DOWN" => (_bodyCoords[i].Item1, _bodyCoords[i].Item2 + 1),
                    "LEFT" => (_bodyCoords[i].Item1 - 1, _bodyCoords[i].Item2),
                    "RIGHT" => (_bodyCoords[i].Item1 + 1, _bodyCoords[i].Item2),
                    _ => _bodyCoords[i]
                };
            }
        }
        else if (_previousDirection != _currentDirection)
        {
            _previousDirection = _currentDirection;
            _bodyDrawed = 0;
            var currentBodyPart = _bodyCoords[_currentBodyIndex];
            _bodyCoords[_currentBodyIndex] = _currentDirection switch
            {
                "UP" => (currentBodyPart.Item1, currentBodyPart.Item2 - 1),
                "DOWN" => (currentBodyPart.Item1, currentBodyPart.Item2 + 1),
                "LEFT" => (currentBodyPart.Item1 - 1, currentBodyPart.Item2),
                "RIGHT" => (currentBodyPart.Item1 + 1, currentBodyPart.Item2),
                _ => currentBodyPart
            };
            _currentBodyIndex--;
            _bodyDrawed++;
        }
        else if (_previousDirection == _currentDirection && _bodyDrawed < Length)
        {
            _bodyCoords[_currentBodyIndex] = _bodyCoords[_currentBodyIndex + 1];
            _currentBodyIndex--;
            _bodyDrawed++;
            //for (int i = 0; i < Length; i++)
            //{
            //    _currentBodyIndex
            //}
        }
    }

    private static void KillSnake(List<(int, int)> bodyCoords)
    {
        foreach (var coord in bodyCoords)
        {
            Console.SetCursorPosition(coord.Item1, coord.Item2);
            Console.Write(" ");
        }
    }

    private static void DrawBody(List<(int, int)> bodyCoords)
    {
        foreach (var coord in bodyCoords)
        {
            Console.SetCursorPosition(coord.Item1, coord.Item2);
            Console.Write(".");
        }
    }

    private static void PopulateSnakeBody()
    {
        BodyCoords.Clear();
        for (int i = 0; i < Length; i++)
        {
            BodyCoords.Add((_initialTailCoord.Item1 + i, _initialTailCoord.Item2));
        }
    }
}