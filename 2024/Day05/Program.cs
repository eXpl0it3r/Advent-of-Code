var input = await File.ReadAllLinesAsync("input.txt");

var inputSplit = Array.IndexOf(input, string.Empty);

var orders = input.Take(inputSplit)
                  .Select(o => o.Split("|")
                                .Select(int.Parse)
                                .ToList())
                  .ToList();
var updates = input.Skip(inputSplit + 1)
                   .Select(o => o.Split(",")
                                 .Select(int.Parse)
                                 .ToList())
                   .ToList();

var partOne = 0;

foreach (var update in updates)
{
    var correct = true;

    for (var i = 0; i < update.Count; i++)
    {
        var current = update[i];

        var after = orders.Exists(o =>
                                  {
                                      var location = update.IndexOf(o[1]);
                                      return o[0] == current && location != -1 && location < i;
                                  });
        var before = orders.Exists(o =>
                                   {
                                       var location = update.IndexOf(o[0]);
                                       return o[1] == current && location != -1 && location > i;
                                   });

        if (after || before)
        {
            correct = false;
            break;
        }
    }

    if (correct)
    {
        partOne += update[update.Count / 2];
    }
}

var partTwo = 0;

foreach (var update in updates)
{
    var correct = true;

    var allPassed = false;
    while (!allPassed)
    {
        var allOrdersPassed = 0;
        foreach (var t in orders)
        {
            var before = update.IndexOf(t[0]);
            var after = update.IndexOf(t[1]);
            
            if (before != -1 && after != -1 && after < before)
            {
                correct = false;
                (update[before], update[after]) = (update[after], update[before]);
                continue;
            }

            allOrdersPassed++;
        }

        if (allOrdersPassed == orders.Count)
        {
            allPassed = true;
        }
    }

    if (!correct)
    {
        partTwo += update[update.Count / 2];
    }
}

Console.WriteLine($"Part One: {partOne}");
Console.WriteLine($"Part Two: {partTwo}");
