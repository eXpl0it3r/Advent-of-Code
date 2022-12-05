using System.Text.RegularExpressions;

List<Stack<char>> InitialStacks(IReadOnlyList<string> strings, int instructionSplit1)
{
    var numberOfColumns = strings[instructionSplit1 - 1].Split("   ").Length + 1;

    var list = Enumerable.Range(1, numberOfColumns)
                         .Select(_ => new Stack<char>())
                         .ToList();

    for (var c = 1; c < numberOfColumns; c++)
    {
        for (var r = instructionSplit1 - 2; r >= 0; --r)
        {
            var crate = strings[r][1 + (c - 1) * 4];

            if (crate != ' ')
            {
                list[c].Push(crate);
            }
        }
    }

    return list;
}

var input = File.ReadAllLines("input.txt");

var instructionSplit = 0;

for (var i = 0; i < input.Length; ++i)
{
    if (input[i] == string.Empty)
    {
        instructionSplit = i;
        break;
    }
}

var partOneStacks = InitialStacks(input, instructionSplit);

foreach (var stack in partOneStacks)
{
    foreach (var crate in stack)
    {
        Console.Write(crate);
    }

    Console.Write("\n");
}

for (var i = instructionSplit + 1; i < input.Length; ++i)
{
    var match = Regex.Matches(input[i], @"move (?<amount>\d+) from (?<source>\d+) to (?<destination>\d+)")
                     .Single();

    var amount = int.Parse(match.Groups["amount"].Value);
    var source = int.Parse(match.Groups["source"].Value);
    var destination = int.Parse(match.Groups["destination"].Value);

    for (var c = 0; c < amount; c++)
    {
        partOneStacks[destination].Push(partOneStacks[source].Pop());
    }
}

Console.Write("PartOne: ");
foreach (var stack in partOneStacks)
{
    Console.Write(stack.TryPeek(out var top) ? top : string.Empty);
}

Console.Write("\n");


var partTwoStacks = InitialStacks(input, instructionSplit);

for (var i = instructionSplit + 1; i < input.Length; ++i)
{
    var match = Regex.Matches(input[i], @"move (?<amount>\d+) from (?<source>\d+) to (?<destination>\d+)")
                     .Single();

    var amount = int.Parse(match.Groups["amount"].Value);
    var source = int.Parse(match.Groups["source"].Value);
    var destination = int.Parse(match.Groups["destination"].Value);

    var stack = new Stack<char>();

    for (var c = 0; c < amount; c++)
    {
        stack.Push(partTwoStacks[source].Pop());
    }

    foreach (var crate in stack)
    {
        partTwoStacks[destination].Push(crate);
    }
}

Console.Write("PartTwo: ");
foreach (var stack in partTwoStacks)
{
    Console.Write(stack.TryPeek(out var top) ? top : string.Empty);
}

Console.Write("\n");