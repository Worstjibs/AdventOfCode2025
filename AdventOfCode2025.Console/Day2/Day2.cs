namespace AdventOfCode2025.Console.Day2;

public static class Day2
{
    public static long Solve()
    {
        var inputFile = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Day2", "day2.txt"));

        var ids = inputFile.Split(',').SelectMany(x =>
        {
            var rangeBlinds = x.Split('-');

            var range = new List<string>();

            var lower = long.Parse(rangeBlinds[0]);
            var upper = long.Parse(rangeBlinds[1]);

            for (var i = lower; i < upper; i++)
            {
                range.Add(i.ToString());
            }

            return range;
        }).ToArray();

        var invalidIds = ids
            .Where(Approach2)
            .Select(long.Parse)
            .ToArray();

        var result = invalidIds.Sum();

        return result;
    }


    private static bool Approach1(string id)
    {
        if (id.Length % 2 is not 0)
        {
            return false;
        }

        var midpoint = id.Length / 2;

        var firstHalf = id[0..midpoint];
        var secondHalf = id[midpoint..];

        return firstHalf == secondHalf;
    }

    private static bool Approach2(string id)
    {
        var midpoint = id.Length / 2;

        // Create a loop defining the window size
        for (var i = 1; i <= midpoint; i++)
        {
            var repeat = FindRepeat(id, i);
            if (repeat is true)
            {
                return true;
            }
        }

        return false;
    }

    private static bool FindRepeat(string id, int windowSize)
    {
        if (id.Length % windowSize is not 0)
        {
            return false;
        }

        // Now loop over the whole id using the window size
        for (var j = 0; (j + windowSize) < id.Length; j++)
        {
            if (id[j] != id[j + windowSize])
            {
                return false;
            }
        }

        return true;
    }
}
