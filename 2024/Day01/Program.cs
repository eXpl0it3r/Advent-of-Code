var input = await File.ReadAllLinesAsync("input.txt");

var list1 = new List<int>();
var list2 = new List<int>();

foreach (var line in input)
{
    var values = line.Split("   ");
    list1.Add(int.Parse(values[0]));
    list2.Add(int.Parse(values[1]));
}

list1.Sort();
list2.Sort();

var sum = 0;

for (int i = 0; i < list1.Count; i++)
{
    var diff = Math.Abs(list1[i] - list2[i]);
    sum += diff;
}

Console.WriteLine($"Part 1: {sum}");

var mul = 0;

foreach (var location in list1)
{
    mul += location * list2.Count(l => l == location);
}

Console.WriteLine($"Part 2: {mul}");