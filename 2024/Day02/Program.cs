var input = await File.ReadAllLinesAsync("input.txt");

var reports = input.Select(line => line.Split(" ").Select(int.Parse).ToList());

var resultsPart1 = new List<bool>();
var resultsPart2 = new List<bool>();

foreach (var report in reports)
{
    var allCase = CheckValidity(report);
    
    resultsPart1.Add(allCase);

    var validity = allCase;
    
    for (var i = 0; i < report.Count; i++)
    {
        if (validity)
        {
            break;
        }
        
        validity = CheckValidity(report.Take(i).Concat(report.Skip(i + 1)).ToList());
    }
    
    resultsPart2.Add(validity);
}

Console.WriteLine($"Part 1: {resultsPart1.Count(b => b)}");
Console.WriteLine($"Part 2: {resultsPart2.Count(b => b)}");

bool IncreasingOrDecreasing(List<int> values)
{
    return values.All(c => c < 0) || values.All(c => c > 0);
}

bool WithinRange(List<int> values)
{
    return values.All(c => Math.Abs(c) >= 1 && Math.Abs(c) <= 3);
}

bool CheckValidity(List<int> report)
{
    var list = new List<int>();
    for (var i = 1; i < report.Count; i++)
    {
        list.Add(report[i - 1] - report[i]);
    }

    return IncreasingOrDecreasing(list) && WithinRange(list);
}