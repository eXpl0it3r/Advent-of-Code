void PartOne(IEnumerable<int> input)
{
    int? previous = null;
    var increases = 0;

    foreach (var current in input)
    {
        if (previous == null)
        {
            previous = current;
            continue;
        }

        if (previous < current)
        {
            increases++;
        }

        previous = current;
    }

    Console.WriteLine($"Part One: {increases}");
}

void PartTwo(IReadOnlyList<int> ints)
{
    var increases = 0;

    for (var position = 0; position + 3 < ints.Count; position++)
    {
        var window1 = ints[position] + ints[position + 1] + ints[position + 2];
        var window2 = ints[position + 1] + ints[position + 2] + ints[position + 3];

        if (window2 > window1)
        {
            increases++;
        }
    }

    Console.WriteLine($"Part Two: {increases}");
}

var input = File.ReadLines("input.txt")
                .Select(int.Parse)
                .ToList();

PartOne(input);
PartTwo(input);