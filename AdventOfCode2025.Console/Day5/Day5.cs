using System.Globalization;
using System.Transactions;

namespace AdventOfCode2025.Console.Day5;

public static class Day5
{
    public static long Solve()
    {
        var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Day5", "day5.txt"));

        return SecondPart(input);
    }

    private static long FirstPart(string[] input)
    {
        var ranges = new List<(long First, long Second)>();

        var freshCount = 0;

        foreach (var item in input)
        {
            if (item.Contains('-'))
            {
                var range = item.Split('-');

                var first = long.Parse(range[0]);
                var second = long.Parse(range[1]);

                ranges.Add(new(first, second));
            }
            else if (string.IsNullOrEmpty(item))
            {
                continue;
            }
            else
            {
                var id = long.Parse(item);

                if (ranges.Any(x => x.First <= id && id <= x.Second))
                {
                    freshCount++;
                }
            }
        }

        return freshCount;
    }

    private static long SecondPart(string[] input)
    {
        var ranges = new List<Range>();

        foreach (var item in input)
        {
            if (item.Contains('-'))
            {
                var range = item.Split('-');

                var lower = long.Parse(range[0]);
                var upper = long.Parse(range[1]);

                ranges.Add(new(lower, upper));
            }
            else if (string.IsNullOrEmpty(item))
            {
                break;
            }
        }

        long freshCount = 0;

        ranges = [.. ranges.OrderBy(x => x.Lower).ThenBy(x => x.Upper)];

        var merged = new List<Range>();
        var curLow = ranges[0].Lower;
        var curHigh = ranges[0].Upper;

        foreach (var r in ranges.Skip(1))
        {
            // Decide merge condition
            bool overlaps = r.Lower <= curHigh;
            bool touches = r.Lower == curHigh + 1;

            if (overlaps || touches)
            {
                // Extend current range
                curHigh = Math.Max(curHigh, r.Upper);
            }
            else
            {
                merged.Add(new Range(curLow, curHigh));
                curLow = r.Lower;
                curHigh = r.Upper;
            }
        }
        merged.Add(new Range(curLow, curHigh));

        foreach (var mergedRange in merged)
        {
            freshCount += mergedRange.Upper - mergedRange.Lower + 1;
        }

        return freshCount;
    }

    public record Range(long Lower, long Upper);
}
