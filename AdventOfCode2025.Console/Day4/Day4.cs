namespace AdventOfCode2025.Console.Day4;

public static class Day4
{
    public static int Solve()
    {
        var input = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Day4", "day4.txt"));

        var rows = input.Split(Environment.NewLine);
        var rowLength = rows.First().Length;

        var grid = InitializeGrid(rows, rowLength);

        var accessibleCount = 0;

        bool paperToRemove;
        do
        {
            var accessibleThisRound = CheckGrid(grid);
            accessibleCount += accessibleThisRound;

            paperToRemove = accessibleThisRound > 0;
            if (paperToRemove)
            {
                RemovePaperFromGrid(grid);
            }
        }
        while (paperToRemove);

        return accessibleCount;
    }

    private static Cell[,] InitializeGrid(string[] rows, int rowLength)
    {
        var grid = new Cell[rowLength, rows.Length];

        for (var y = 0; y < rows.Length; y++)
        {
            var row = rows[y];
            for (var x = 0; x < row.Length; x++)
            {
                var cell = new Cell { IsPaper = row[x] is '@' };

                grid[x, y] = cell;
            }
        }

        return grid;
    }

    private static int CheckGrid(Cell[,] grid)
    {
        var accessibleCount = 0;

        for (var y = 0; y < grid.GetLength(1); y++)
        {
            for (var x = 0; x < grid.GetLength(0); x++)
            {
                var cell = grid[x, y];
                if (!cell.IsPaper)
                {
                    continue;
                }

                // Check each direction, including diagonals
                var directions = new (int dx, int dy)[]
                {
                    (0, -1),  // Up
                    (0, 1),   // Down
                    (-1, 0),  // Left
                    (1, 0),   // Right
                    (-1, -1), // Up-Left
                    (1, -1),  // Up-Right
                    (-1, 1),  // Down-Left
                    (1, 1)    // Down-Right
                };

                var cellPaperCount = 0;
                foreach (var (dx, dy) in directions)
                {
                    var nx = x + dx;
                    var ny = y + dy;

                    var isValid = nx >= 0
                                    && nx < grid.GetLength(0)
                                    && ny >= 0
                                    && ny < grid.GetLength(1);

                    if (isValid && grid[nx, ny].IsPaper)
                    {
                        cellPaperCount++;
                    }
                }

                if (cellPaperCount < 4)
                {
                    cell.IsAccessible = true;
                    accessibleCount++;
                }
            }
        }

        return accessibleCount;
    }

    public static void RemovePaperFromGrid(Cell[,] grid)
    {
        for (var y = 0; y < grid.GetLength(1); y++)
        {
            for (var x = 0; x < grid.GetLength(0); x++)
            {
                var cell = grid[x, y];
                if (cell.IsAccessible)
                {
                    cell.IsPaper = false;
                    cell.IsAccessible = false;
                }
            }
        }
    }
}

public class Cell
{
    public bool IsPaper { get; set; }
    public bool IsAccessible { get; set; }
}