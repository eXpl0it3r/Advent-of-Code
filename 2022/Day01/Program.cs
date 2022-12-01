var input = File.ReadAllLines("input.txt");

var elves = new List<List<int>>();
var calories = new List<int>();

foreach (var calorie in input)
{
    if (calorie == string.Empty)
    {
        elves.Add(calories);
        calories = new List<int>();
        continue;
    }
    
    calories.Add(Convert.ToInt32(calorie));
}

var mostCalories = elves.Select(c => c.Sum())
                        .Max();

Console.WriteLine($"PartOne: {mostCalories}");

var topThree = elves.Select(c => c.Sum())
                    .OrderDescending()
                    .Take(3)
                    .Sum();

Console.WriteLine($"PartOne: {mostCalories}");