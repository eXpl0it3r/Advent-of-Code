var grid = File.ReadAllLines("input.txt")
               .Select(l => l.Select(c => int.Parse(c.ToString()))
                             .ToList())
               .ToList();

var visible = 0;

for (var y = 0; y < grid.Count; ++y)
{
    for (var x = 0; x < grid[y].Count; x++)
    {
        if (Border(y, grid.Count) || Border(x, grid[y].Count)
                                  || VisibleFromLeft(grid, x, y, grid[y][x])
                                  || VisibleFromRight(grid, x, y, grid[y][x])
                                  || VisibleFromTop(grid, x, y, grid[y][x])
                                  || VisibleFromBottom(grid, x, y, grid[y][x]))
        {
            visible++;
        }
    }
}

Console.WriteLine($"PartOne: {visible}");

var highScore = 0;

for (var y = 0; y < grid.Count; ++y)
{
    for (var x = 0; x < grid[y].Count; x++)
    {
        highScore = Math.Max(highScore, ScoreLeft(grid, x, y, grid[y][x])
                                        * ScoreRight(grid, x, y, grid[y][x])
                                        * ScoreTop(grid, x, y, grid[y][x])
                                        * ScoreBottom(grid, x, y, grid[y][x]));
    }
}

Console.WriteLine($"PartTwo: {highScore}");

int ScoreLeft(List<List<int>> grid, int x, int y, int height)
{
    if (x - 1 < 0)
    {
        return 0;
    }

    if (grid[y][x - 1] >= height)
    {
        return 1;
    }

    return 1 + ScoreLeft(grid, x - 1, y, height);
}

int ScoreRight(List<List<int>> grid, int x, int y, int height)
{
    if (x + 1 == grid.First().Count)
    {
        return 0;
    }

    if (grid[y][x + 1] >= height)
    {
        return 1;
    }

    return 1 + ScoreRight(grid, x + 1, y, height);
}

int ScoreTop(List<List<int>> grid, int x, int y, int height)
{
    if (y - 1 < 0)
    {
        return 0;
    }

    if (grid[y - 1][x] >= height)
    {
        return 1;
    }

    return 1 + ScoreTop(grid, x, y - 1, height);
}

int ScoreBottom(List<List<int>> grid, int x, int y, int height)
{
    if (y + 1 == grid.Count)
    {
        return 0;
    }

    if (grid[y + 1][x] >= height)
    {
        return 1;
    }

    return 1 + ScoreBottom(grid, x, y + 1, height);
}

bool Border(int v, int limit)
{
    return v - 1 < 0 || v + 1 == limit;
}

bool VisibleFromLeft(List<List<int>> grid, int x, int y, int height)
{
    if (x - 1 < 0)
    {
        return true;
    }

    return grid[y][x - 1] < height && VisibleFromLeft(grid, x - 1, y, height);
}

bool VisibleFromRight(List<List<int>> grid, int x, int y, int height)
{
    if (x + 1 == grid.First().Count)
    {
        return true;
    }

    return grid[y][x + 1] < height && VisibleFromRight(grid, x + 1, y, height);
}

bool VisibleFromTop(List<List<int>> grid, int x, int y, int height)
{
    if (y - 1 < 0)
    {
        return true;
    }

    return grid[y - 1][x] < height && VisibleFromTop(grid, x, y - 1, height);
}

bool VisibleFromBottom(List<List<int>> grid, int x, int y, int height)
{
    if (y + 1 == grid.Count)
    {
        return true;
    }

    return grid[y + 1][x] < height && VisibleFromBottom(grid, x, y + 1, height);
}