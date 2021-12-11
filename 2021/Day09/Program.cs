namespace Day09;

public static class Program
{
    private static int _height;
    private static int _width;
    private static List<List<int>> _input = new();
    private static readonly List<List<bool>> _visited = new();

    public static void Main()
    {
        _input = File.ReadLines("input.txt")
                     .Select(s => s.Select(c => int.Parse(c.ToString()))
                                   .ToList())
                     .ToList();

        _height = _input.Count;
        _width = _input.First().Count;

        for (var y = 0; y < _height; y++)
        {
            _visited.Add(new List<bool>());

            for (var x = 0; x < _width; x++)
            {
                _visited[y].Add(false);
            }
        }

        var partOne = 0;
        var basins = new List<int>();

        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                var localMinimum = true;

                if (x != 0 && _input[y][x - 1] <= _input[y][x])
                {
                    localMinimum = false;
                }
                else if (x != _width - 1 && _input[y][x + 1] <= _input[y][x])
                {
                    localMinimum = false;
                }

                if (y != 0 && _input[y - 1][x] <= _input[y][x])
                {
                    localMinimum = false;
                }
                else if (y != _height - 1 && _input[y + 1][x] <= _input[y][x])
                {
                    localMinimum = false;
                }

                if (localMinimum)
                {
                    ResetVisited();

                    partOne += 1 + _input[y][x];
                    basins.Add(Search(y, x));
                }
            }
        }

        Console.WriteLine($"PartOne: {partOne}");

        var partTwo = basins.OrderByDescending(b => b)
                            .Take(3)
                            .ToList();
        Console.WriteLine($"PartTwo: {partTwo[0] * partTwo[1] * partTwo[2]}");
    }

    private static void ResetVisited()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                _visited[y][x] = false;
            }
        }
    }

    private static int Search(int y, int x)
    {
        if (x < 0 || x == _width || y < 0 || y == _height || _visited[y][x] || _input[y][x] == 9)
        {
            return 0;
        }

        _visited[y][x] = true;

        return 1 + Search(y - 1, x) + Search(y + 1, x) + Search(y, x - 1) + Search(y, x + 1);
    }
}