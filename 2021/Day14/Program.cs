var input = File.ReadAllLines("input.txt");

var template = input.First();
var pairs = input.Skip(2)
                 .Where(l => l.Length > 0)
                 .Select(l => l.Split(" -> "))
                 .ToDictionary(l => l[0], l => l[1]);

for (var i = 1; i <= 10; i++)
{
    var next = $"{template[0]}";

    for (var t = 0; t < template.Length - 1; t++)
    {
        var key = $"{template[t]}{template[t + 1]}";

        if (pairs.ContainsKey(key))
        {
            next += $"{pairs[key]}{template[t + 1]}";
        }
    }

    template = next;
}

var groupingPartOne = template.GroupBy(c => c)
                              .Select(g => g.Count())
                              .OrderByDescending(g => g)
                              .ToList();
var partOne = groupingPartOne.First() - groupingPartOne.Last();

Console.WriteLine($"PartOne: {partOne}");

template = input.First();
var map = pairs.ToDictionary(kvp => kvp.Key, kvp => $"{kvp.Key[0]}{kvp.Value}{kvp.Key[1]}");

string Iterate(string substring)
{
    //Console.WriteLine($"{substring}");
    
    if (map.ContainsKey(substring))
    {
        return map[substring];
    }
    if (substring.Length == 2)
    {
        return substring;
    }

    string mapping;

    if (substring.Length == 3)
    {
        mapping = Iterate(substring.Substring(0, 2)) + pairs[substring.Substring(1, 2)] + substring.Last();
    }
    else
    {
        var division = substring.Length / 2;
        mapping = Iterate(substring.Substring(0, division)) + pairs[substring.Substring(division - 1, 2)] + Iterate(substring.Substring(division));
    }

    //Console.WriteLine($"{substring} -> {mapping}");
    
    map.Add(substring, mapping);
    return mapping;
}

for (var i = 1; i <= 40; i++)
{
    Console.WriteLine($"Iteration #{i}");
    template = Iterate(template);
}

var groupingPartTwo = template.GroupBy(c => c)
                              .Select(g => g.Count())
                              .OrderByDescending(g => g)
                              .ToList();
var partTwo = groupingPartTwo.First() - groupingPartTwo.Last();

Console.WriteLine($"PartTwo: {partTwo}");