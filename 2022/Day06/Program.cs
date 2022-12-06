var input = File.ReadAllLines("input.txt")
                .First();

var sequenceStart = -1;

var buffer = input.Take(4).ToList();

if (buffer.GroupBy(x => x).Count() == 4)
{
    sequenceStart = 4;
}

for (var i = 4; i < input.Length && sequenceStart < 0; ++i)
{
    buffer.Remove(buffer[0]);
    buffer.Add(input[i]);

    if (buffer.GroupBy(x => x).Count() == 4)
    {
        sequenceStart = i + 1;
    }
}

Console.WriteLine($"PartOne: {sequenceStart}");

var messageStart = -1;
buffer = input.Skip(sequenceStart - 5).Take(14).ToList();

if (buffer.GroupBy(x => x).Count() == 14)
{
    messageStart = sequenceStart + 9;
}

for (var i = sequenceStart + 9; i < input.Length && messageStart < 0; ++i)
{
    buffer.Remove(buffer[0]);
    buffer.Add(input[i]);

    if (buffer.GroupBy(x => x).Count() == 14)
    {
        messageStart = i + 1;
    }
}

Console.WriteLine($"PartTwo: {messageStart}");