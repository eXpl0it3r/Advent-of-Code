int Prioritize(int item) => item switch
                            {
                                >= 65 and <= 90 => item - 38,
                                >= 97 and <= 122 => item - 96
                            };

var input = File.ReadAllLines("input.txt");

var duplicateItem = input.Select(s => new Tuple<string, string>(s[..(s.Length / 2)], s[(s.Length / 2)..]))
                         .Select(s => (int)s.Item1.Intersect(s.Item2).Single())
                         .Select(Prioritize)
                         .Sum();

Console.WriteLine($"PartOne: {duplicateItem}");

var badges = 0;
for (var i = 0; i < input.Length; i += 3)
{
    badges += Prioritize(input[i].Intersect(input[i + 1])
                                 .Intersect(input[i + 2])
                                 .Single());
}

Console.WriteLine($"PartTwo: {badges}");