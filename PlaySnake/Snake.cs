namespace PlaySnake;

enum Direction
{
    UP = -1,
    DOWN = 1,
    LEFT = -1,
    RIGHT = 1
}

internal static class Snake
{
    private static List<(int, int)> _bodyCoords = new();
    private static List<(int, int)> BodyCoords
    {
        get => _bodyCoords;
        set
        {
            value = _bodyCoords;
        }
    }

    public const int Length = 5;
    private static Direction _currDirection = Direction.RIGHT;
    private static Direction _prevDirection = Direction.RIGHT;

    static Snake()
    {
        DrawInitialSnake();
    }

    public static void Run()
    {
        new Thread(InputHandler).Start();

        while (true)
        {
            Thread.Sleep(500);
            ClearSnake();
            MoveCursor();
            Draw();
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
                    if (_currDirection != Direction.DOWN)
                        _currDirection = Direction.UP;
                    break;
                case ConsoleKey.DownArrow:
                    if (_currDirection != Direction.UP)
                        _currDirection = Direction.DOWN;
                    break;
                case ConsoleKey.LeftArrow:
                    if (_currDirection != Direction.RIGHT)
                        _currDirection = Direction.LEFT;
                    break;
                case ConsoleKey.RightArrow:
                    if (_currDirection != Direction.LEFT)
                        _currDirection = Direction.RIGHT;
                    break;
                default:
                    break;
            }
        }
    }

    private static void Draw()
    {
        foreach (var coord in BodyCoords)
        {
            Console.SetCursorPosition(coord.Item1, coord.Item2);
            Console.Write(".");
        }
    }
    private static void MoveCursor()
    {
        if (_currDirection == Direction.UP)
        {
           
        }
        else if (_currDirection == Direction.DOWN)
        {
            var head = BodyCoords[0];
            var headTop = head.Item2;
            var headLeft = head.Item1;
            for (int i = 0; i < BodyCoords.Count; i++)
            {

            }
        }
        else if (_currDirection == Direction.LEFT)
        {
            var head = BodyCoords[0];
            BodyCoords.RemoveAt(BodyCoords.Count - 1);
            BodyCoords.Insert(0, (head.Item1 - 1, head.Item2));
        }
        else if (_currDirection == Direction.RIGHT)
        {
            var head = BodyCoords[0];
            BodyCoords.RemoveAt(BodyCoords.Count - 1);
            BodyCoords.Insert(0, (head.Item1 + 1, head.Item2));
        }
    }
    private static void IsSnakeStraight()
    {
    }
    private static void ClearSnake()
    {
        foreach (var coord in BodyCoords)
        {
            Console.SetCursorPosition(coord.Item1, coord.Item2);
            Console.Write(" ");
        }
        ClearCoords();
    }
    private static void ClearCoords()
    {
        BodyCoords.Clear();
    }
    private static void DrawInitialSnake()
    {
        Console.SetCursorPosition(45, 25);
        Console.Write(new string('.', 5));
        Console.SetCursorPosition(55, 25);

        PopulateInitialCoords();
    }
    private static void PopulateInitialCoords()
    {
        BodyCoords.Clear();
        BodyCoords.Add((55, 25));
        BodyCoords.Add((54, 25));
        BodyCoords.Add((53, 25));
        BodyCoords.Add((52, 25));
        BodyCoords.Add((51, 25));
    }

    #region old methods
    //private static void Move()
    //{
    //    if (_previousDirection == _currentDirection && _bodyDrawed == Length)
    //    {
    //        for (int i = 0; i < _bodyCoords.Count; i++)
    //        {
    //            _bodyCoords[i] = _currentDirection switch
    //            {
    //                "UP" => (_bodyCoords[i].Item1, _bodyCoords[i].Item2 - 1),
    //                "DOWN" => (_bodyCoords[i].Item1, _bodyCoords[i].Item2 + 1),
    //                "LEFT" => (_bodyCoords[i].Item1 - 1, _bodyCoords[i].Item2),
    //                "RIGHT" => (_bodyCoords[i].Item1 + 1, _bodyCoords[i].Item2),
    //                _ => _bodyCoords[i]
    //            };
    //        }
    //    }
    //    else if (_previousDirection != _currentDirection)
    //    {
    //        _previousDirection = _currentDirection;
    //        _bodyDrawed = 0;
    //        _currentBodyIndex = Length - 1;
    //        var currentBodyPart = _bodyCoords[_currentBodyIndex];
    //        _bodyCoords[_currentBodyIndex] = _currentDirection switch
    //        {
    //            "UP" => (currentBodyPart.Item1, currentBodyPart.Item2 - 1),
    //            "DOWN" => (currentBodyPart.Item1, currentBodyPart.Item2 + 1),
    //            "LEFT" => (currentBodyPart.Item1 - 1, currentBodyPart.Item2),
    //            "RIGHT" => (currentBodyPart.Item1 + 1, currentBodyPart.Item2),
    //            _ => currentBodyPart
    //        };
    //        _currentBodyIndex--;
    //        _bodyDrawed++;
    //    }
    //    else if (_previousDirection == _currentDirection && _bodyDrawed < Length)
    //    {
    //        _bodyCoords[_currentBodyIndex] = _bodyCoords[_currentBodyIndex + 1];
    //        _currentBodyIndex--;
    //        _bodyDrawed++;
    //    }
    //}

    //private static void KillSnake(List<(int, int)> bodyCoords)
    //{
    //    foreach (var coord in bodyCoords)
    //    {
    //        Console.SetCursorPosition(coord.Item1, coord.Item2);
    //        Console.Write(" ");
    //    }
    //}

    //private static void DrawBody(List<(int, int)> bodyCoords)
    //{
    //    foreach (var coord in bodyCoords)
    //    {
    //        Console.SetCursorPosition(coord.Item1, coord.Item2);
    //        Console.Write(".");
    //    }
    //}

    //private static void DrawInitialSnake()
    //{
    //    int pox = 56;
    //    while (--pox > 50)
    //    {
    //        Console.SetCursorPosition(pox, 25);
    //        Console.Write(".");
    //    }
    //}

    //private static void PopulateSnakeBody()
    //{
    //    BodyCoords.Clear();
    //    int counter = 0;
    //    while (counter < Length)
    //    {
    //        BodyCoords.Add((_initialHeadCoord.Item1 - counter, _initialHeadCoord.Item2));
    //        counter++;
    //    }
    //    BodyCoords.Reverse();
    //}
    #endregion
}