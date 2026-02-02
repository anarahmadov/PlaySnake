namespace PlaySnake;

class Direction
{
    public const string UP = "UP";
    public const string DOWN = "DOWN";
    public const string LEFT = "LEFT";
    public const string RIGHT = "RIGHT";
}

internal static class Snake
{
    private static int topBorder = 0; 
    private static int bottomBorder = 50; 
    private static int rightBorder = 100; 
    private static int leftBorder = 0;
    
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
    private static string _currDirection = "RIGHT";
    private static string _prevDirection = "RIGHT";
    private static bool firstRun = true;

    static Snake()
    {
        DrawInitialSnake();
    }

    public static void Run()
    {
        new Thread(InputHandler).Start();

        PopulateCoordsAfterMove(BodyCoords[0]);
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
    private static void DrawInitialSnake()
    {
        Console.SetCursorPosition(51, 25);
        Console.Write(new string('.', 5));
        Console.SetCursorPosition(55, 25);

        PopulateInitialCoords();

        void PopulateInitialCoords()
        {
            BodyCoords.Clear();
            BodyCoords.Add((55, 25));
            BodyCoords.Add((54, 25));
            BodyCoords.Add((53, 25));
            BodyCoords.Add((52, 25));
            BodyCoords.Add((51, 25));
        }
    }

    private static void Draw(int index)
    {
        Console.SetCursorPosition(BodyCoords[index].Item1, BodyCoords[index].Item2);
        Console.Write(".");
    }

    private static void PopulateCoordsAfterMove((int, int) currentCoords, int index = 0)
    {
        if (_currDirection == _prevDirection && index > 0 && index < BodyCoords.Count)
        {
            if (IsSnakeColliding())
                return;

            var prev = BodyCoords[index];
            var newCoord = currentCoords;
            Clear(BodyCoords[index]);
            SetCoords(index, newCoord);
            Draw(index);
            PopulateCoordsAfterMove(prev, ++index);
        }
        else
        {
            if (IsSnakeColliding())
                return;

            Thread.Sleep(500);
            index = 0;
            var prev = BodyCoords[0];
            SetCoords(0, _currDirection);
            Draw(0);
            Clear(prev);
            _prevDirection = _currDirection;
            PopulateCoordsAfterMove(prev, ++index);
        }
    }

    private static void Clear((int, int) coords)
    {
        Console.SetCursorPosition(coords.Item1, coords.Item2);
        Console.Write(" ");
    }
    private static void Clear(int index)
    {
        Console.SetCursorPosition(BodyCoords[index].Item1, BodyCoords[index].Item2);
        Console.Write(" ");
    }
    private static void Clear()
    {
        foreach (var coord in BodyCoords)
        {
            Console.SetCursorPosition(coord.Item1, coord.Item2);
            Console.Write(" ");
        }
    }

    private static bool IsSnakeStraight()
    {
        var leftOfFirstPart = BodyCoords[0].Item1;
        var topOfFirstPart = BodyCoords[0].Item2;

        var vertical = BodyCoords.All(x => x.Item1.Equals(leftOfFirstPart));
        var horizontal = BodyCoords.All(x => x.Item2.Equals(topOfFirstPart));

        return vertical || horizontal;
    }
    private static bool IsSnakeColliding()
    {
        var head = BodyCoords[0];
        return head.Item1.Equals(leftBorder) || head.Item1.Equals(rightBorder) ||
               head.Item2.Equals(topBorder) || head.Item2.Equals(bottomBorder);
    }

    private static void SetCoords(int index, string direction)
    {
        switch (direction)
        {
            case Direction.UP:
                BodyCoords[index] = (BodyCoords[index].Item1, BodyCoords[index].Item2 - 1);
                break;
            case Direction.DOWN:
                BodyCoords[index] = (BodyCoords[index].Item1, BodyCoords[index].Item2 + 1);
                break;
            case Direction.LEFT:
                BodyCoords[index] = (BodyCoords[index].Item1 - 1, BodyCoords[index].Item2);
                break;
            case Direction.RIGHT:
                BodyCoords[index] = (BodyCoords[index].Item1 + 1, BodyCoords[index].Item2);
                break;
        }
    }
    private static void SetCoords(int index, (int, int) prevCoords)
    {
        BodyCoords[index] = prevCoords;
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