var input = File.ReadAllLines("input.txt")
                .Where(l => l.Length > 0)
                .Select(l => l.Select(c => int.Parse(c.ToString()))
                              .ToList())
                .ToList();

var width = input.First().Count;
var height = input.Count;
var flashCount = 0;
var partOne = 0;
var partTwo = 0;

void Increase()
{
    for (var y = 0; y < height; y++)
    {
        for (var x = 0; x < width; x++)
        {
            input[y][x]++;
        }
    }
}

void Flash()
{
    var visited = CreateVisitedList();

    for (var y = 0; y < height; y++)
    {
        for (var x = 0; x < width; x++)
        {
            if (input[y][x] > 9)
            {
                Iterate(y, x, visited);
            }
        }
    }
}

List<List<bool>> CreateVisitedList()
{
    var visited = new List<List<bool>>();

    for (var y = 0; y < height; y++)
    {
        visited.Add(new List<bool>());

        for (var x = 0; x < width; x++)
        {
            visited[y].Add(false);
        }
    }

    return visited;
}

void Iterate(int y, int x, List<List<bool>> visited)
{
    if (input[y][x] <= 9)
    {
        input[y][x]++;

        if (input[y][x] <= 9)
        {
            return;
        }
    }

    if (visited[y][x])
    {
        return;
    }

    visited[y][x] = true;
    flashCount++;

    for (var v = -1; v <= 1; v++)
    {
        for (var h = -1; h <= 1; h++)
        {
            if (y + v >= 0 && y + v < height && x + h >= 0 && x + h < width)
            {
                if (v == 0 && h == 0)
                {
                    continue;
                }

                Iterate(y + v, x + h, visited);
            }
        }
    }
}

void ResetFlashing()
{
    for (var y = 0; y < height; y++)
    {
        for (var x = 0; x < width; x++)
        {
            if (input[y][x] > 9)
            {
                input[y][x] = 0;
            }
        }
    }
}

for (var i = 0; i < 1000; i++)
{
    Increase();
    Flash();
    ResetFlashing();

    if (i + 1 == 100)
    {
        partOne = flashCount;
    }

    if (input.SelectMany(x => x).Count(x => x == 0) == width * height)
    {
        partTwo = i + 1;
        break;
    }
}

Console.WriteLine($"PartOne: {partOne}");
Console.WriteLine($"PartTwo: {partTwo}");