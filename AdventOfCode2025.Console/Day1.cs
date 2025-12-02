namespace AdventOfCode2025.Console;

public class Day1
{
    public static int FindResult()
    {
        var textInput = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "day1.txt"));

        var safeInputs = textInput.Split(Environment.NewLine).Select(x =>
        {
            var direction = x[0] is 'L' ? Direction.Left : Direction.Right;

            var distance = int.Parse(x[1..]);

            return new
            {
                Distance = distance,
                Direction = direction
            };
        }).ToArray();

        var zeroCounter = 0;

        var safe = new Safe(50);

        foreach (var input in safeInputs)
        {
            zeroCounter += safe.Move(input.Distance, input.Direction);
        }

        return zeroCounter;
    }
}


public class Safe
{
    private readonly LinkedList<int> _values = new(Enumerable.Range(0, 100));

    public Safe(int start)
    {
        Current = _values.Find(start) 
            ?? throw new ArgumentException("Start value cannot be found");
    }

    public LinkedListNode<int> Current { get; set; }

    /// <summary>
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="direction"></param>
    /// <returns>The number of times the dial pointed at zero during the move</returns>
    public int Move(int distance, Direction direction)
    {
        var zeroCount = 0;

        for (var i = 0; i < distance; i++)
        {
            if (direction is Direction.Left)
            {
                Current = PreviousOrLast();
            }
            else
            {
                Current = NextOrFirst();
            }

            if (Current.Value is 0)
            {
                zeroCount++;
            }
        }

        return zeroCount;
    }

    private LinkedListNode<int> NextOrFirst()
    {
        return Current.Next ?? Current.List!.First!;
    }

    private LinkedListNode<int> PreviousOrLast()
    {
        return Current.Previous ?? Current.List!.Last!;
    }
}

public enum Direction
{
    Left,
    Right
}