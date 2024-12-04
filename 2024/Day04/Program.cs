var input = await File.ReadAllLinesAsync("input.txt");

const int XmasLength = 3;

var partOne = 0;

for (var y = 0; y < input.Length; y++)
{
    // Left to Right
    for (var x = 0; x < input[y].Length - XmasLength; x++)
    {
        if (input[y][x] == 'X' && input[y][x + 1] == 'M' && input[y][x + 2] == 'A' && input[y][x + 3] == 'S')
        {
            partOne++;
        }
    }
    
    // Right to Left
    for (var x = input[y].Length - 1; x >= XmasLength; x--)
    {
        if (input[y][x] == 'X' && input[y][x - 1] == 'M' && input[y][x - 2] == 'A' && input[y][x - 3] == 'S')
        {
            partOne++;
        }
    }
    
    // Left Top to Right Bottom
    for (var x = 0; x < input[y].Length - XmasLength; x++)
    {
        if (y >= input.Length - XmasLength)
        {
            break;
        }
        
        if (input[y][x] == 'X' && input[y + 1][x + 1] == 'M' && input[y + 2][x + 2] == 'A' && input[y + 3][x + 3] == 'S')
        {
            partOne++;
        }
    }
    
    // Right Top to Left Bottom
    for (var x = input[y].Length - 1; x >= XmasLength; x--)
    {
        if (y >= input.Length - XmasLength)
        {
            break;
        }
        
        if (input[y][x] == 'X' && input[y + 1][x - 1] == 'M' && input[y + 2][x - 2] == 'A' && input[y + 3][x - 3] == 'S')
        {
            partOne++;
        }
    }
}

for (var y = input.Length - 1; y >= XmasLength; y--)
{
    // Left Bottom to Right Top
    for (var x = 0; x < input[y].Length - XmasLength; x++)
    {
        if (input[y][x] == 'X' && input[y - 1][x + 1] == 'M' && input[y - 2][x + 2] == 'A' && input[y - 3][x + 3] == 'S')
        {
            partOne++;
        }
    }
    
    // Right Bottom to Left Top
    for (var x = input[y].Length - 1; x >= XmasLength; x--)
    {
        if (input[y][x] == 'X' && input[y - 1][x - 1] == 'M' && input[y - 2][x - 2] == 'A' && input[y - 3][x - 3] == 'S')
        {
            partOne++;
        }
    }
}

for (var x = 0; x < input[0].Length; x++)
{
    // Up to Down
    for (var y = 0; y < input.Length - XmasLength; y++)
    {
        if (input[y][x] == 'X' && input[y + 1][x] == 'M' && input[y + 2][x] == 'A' && input[y + 3][x] == 'S')
        {
            partOne++;
        }
    }
    
    // Down to Up
    for (var y = input.Length - 1; y >= XmasLength; y--)
    {
        if (input[y][x] == 'X' && input[y - 1][x] == 'M' && input[y - 2][x] == 'A' && input[y - 3][x] == 'S')
        {
            partOne++;
        }
    }
}

const int MasLength = 2;
var partTwo = 0;

for (var y = 0; y < input.Length - MasLength; y++)
{
    for (var x = 0; x < input[y].Length - MasLength; x++)
    {
        if ((input[y][x] == 'M' && input[y + 1][x + 1] == 'A' && input[y + 2][x + 2] == 'S')
            || (input[y][x] == 'S' && input[y + 1][x + 1] == 'A' && input[y + 2][x + 2] == 'M'))
        {
            if ((input[y][x + 2] == 'M' && input[y + 2][x] == 'S')
                || (input[y][x + 2] == 'S' && input[y + 2][x] == 'M'))
            {
                partTwo++;
            }
        }
    }
}

Console.WriteLine($"Part One: {partOne}");
Console.WriteLine($"Part Two: {partTwo}");