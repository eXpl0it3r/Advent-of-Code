var input = File.ReadAllLines("input.txt")
                .First()
                .Split(',')
                .Select(int.Parse)
                .OrderBy(h => h)
                .ToList();

var solutionsPartOne = new Dictionary<int, int>();

for (var i = input.Min(); i <= input.Max(); i++)
{
    var sum = input.Sum(value => Math.Abs(value - i));
    solutionsPartOne.Add(i, sum);
}

var minPartOne = solutionsPartOne.Select(s => s.Value)
                                 .Min();
Console.WriteLine($"PartOne: {minPartOne}");

var solutionsPartTwo = new Dictionary<int, int>();

for (var i = input.Min(); i <= input.Max(); i++)
{
    var sum = input.Sum(value => Math.Abs(value - i) * (Math.Abs(value - i) + 1) / 2);
    solutionsPartTwo.Add(i, sum);
}

var minPartTwo = solutionsPartTwo.Select(s => s.Value)
                                 .Min();
Console.WriteLine($"PartOne: {minPartTwo}");