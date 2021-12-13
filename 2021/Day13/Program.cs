namespace Day13;

public class Dot
{
    public int X { get; set; }
    public int Y { get; set; }
}

public class Fold
{
    public string Direction { get; set; } = string.Empty;
    public int Amount { get; set; }
}

public static class Program
{
    public static void Main()
    {
        var input = File.ReadAllLines("input.txt");
        var dots = input.Where(l => !l.StartsWith("fold along") && l.Length > 0)
                        .Select(l => l.Split(','))
                        .Select(d => new Dot
                                     {
                                         X = int.Parse(d[0]),
                                         Y = int.Parse(d[1])
                                     })
                        .ToList();
        var folds = input.Where(l => l.StartsWith("fold along"))
                         .Select(l => l.Replace("fold along ", string.Empty)
                                       .Split('='))
                         .Select(f => new Fold
                                      {
                                          Direction = f[0],
                                          Amount = int.Parse(f[1])
                                      })
                         .ToList();

        var width = dots.Max(d => d.X);
        var height = dots.Max(d => d.Y);
        var grid = new List<List<int>>();
        for (var y = 0; y <= height; y++)
        {
            grid.Add(new List<int>());
            for (var x = 0; x <= width; x++)
            {
                grid[y].Add(0);
            }
        }

        foreach (var dot in dots)
        {
            grid[dot.Y][dot.X] = 1;
        }

        var partOne = 0;

        var limitedWidth = width;
        var limitedHeight = height;

        for (var i = 0; i < folds.Count; i++)
        {
            var localMaxWidth = 0;
            var localMaxHeight = 0;

            for (var y = 0; y <= height; y++)
            {
                for (var x = 0; x <= width; x++)
                {
                    if (grid[y][x] == 1)
                    {
                        localMaxWidth = Math.Max(localMaxWidth, x);
                        localMaxHeight = Math.Max(localMaxHeight, y);
                    }
                }
            }

            limitedWidth = localMaxWidth;
            limitedHeight = localMaxHeight;

            switch (folds[i].Direction)
            {
                case "x":
                {
                    var offsetX = folds[i].Amount;

                    for (var y = 0; y <= limitedHeight; y++)
                    {
                        for (var x = offsetX; x <= limitedWidth; x++)
                        {
                            grid[y][offsetX - (x - offsetX)] = Math.Max(grid[y][offsetX - (x - offsetX)], grid[y][x]);
                            grid[y][x] = 0;
                        }
                    }

                    break;
                }
                case "y":
                {
                    var offsetY = folds[i].Amount;

                    for (var y = offsetY; y <= limitedHeight; y++)
                    {
                        for (var x = 0; x <= limitedWidth; x++)
                        {
                            grid[offsetY - (y - offsetY)][x] = Math.Max(grid[offsetY - (y - offsetY)][x], grid[y][x]);
                            grid[y][x] = 0;
                        }
                    }

                    break;
                }
            }

            if (i == 0)
            {
                partOne = grid.SelectMany(l => l)
                              .Count(x => x == 1);
            }
        }

        Console.WriteLine($"PartOne: {partOne}");
        PrintPartTwo(grid, limitedWidth, limitedHeight);
    }

    private static void PrintPartTwo(List<List<int>> grid, int width, int height)
    {
        Console.WriteLine("PartTwo:");

        for (var y = 0; y <= height; y++)
        {
            var line = string.Empty;

            for (var x = 0; x <= width; x++)
            {
                line += grid[y][x] == 0 ? " " : "#";
            }

            Console.WriteLine(line);
        }

        Console.Write("\n");
    }
}