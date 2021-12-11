var input = File.ReadAllLines("input.txt");

var numbers = input.Take(1)
                   .Single()
                   .Split(',')
                   .Select(int.Parse)
                   .ToList();
input = input.Skip(2)
             .ToArray();

var boards = new List<List<List<Tuple<int, bool>>>>
             {
                 new()
             };
var current = 0;

foreach (var line in input)
{
    if (line.Length == 0)
    {
        boards.Add(new List<List<Tuple<int, bool>>>());
        current++;
        continue;
    }

    var row = new List<Tuple<int, bool>>();

    for (var i = 0; i < (line.Length + 1) / 3; i++)
    {
        row.Add(new Tuple<int, bool>(int.Parse(line.Substring(i * 3, i * 3 + 3 > line.Length ? 2 : 3).Trim()), false));
    }

    boards[current].Add(row);
}

var firstWinner = 0;
var lastWinner = 0;
var winOrder = new Dictionary<int, int>();
var order = 0;

foreach (var number in numbers)
{
    foreach (var row in boards.SelectMany(board => board))
    {
        for (var field = 0; field < row.Count; field++)
        {
            if (row[field].Item1 == number)
            {
                row[field] = new Tuple<int, bool>(number, true);
            }
        }
    }

    for (var board = 0; board < boards.Count; board++)
    {
        var winningRow = boards[board].Any(r => r.All(f => f.Item2));
        var winningColumn = true;
        for (var column = 0; column < boards[board].First().Count; column++)
        {
            winningColumn = true;
            winningColumn = boards[board].Aggregate(winningColumn, (winner, row) => winner & row[column].Item2);

            if (winningColumn)
            {
                break;
            }
        }

        if (winningRow || winningColumn)
        {
            if (firstWinner == 0)
            {
                firstWinner = number * boards[board].SelectMany(row => row)
                                                    .Where(field => !field.Item2)
                                                    .Select(field => field.Item1)
                                                    .Sum();
            }

            if (!winOrder.ContainsKey(board))
            {
                winOrder[board] = order;
                order++;
            }
        }
    }

    if (winOrder.Count == boards.Count)
    {
        lastWinner = number * boards[winOrder.OrderByDescending(wo => wo.Value)
                                             .Select(wo => wo.Key)
                                             .First()].SelectMany(row => row)
                                                      .Where(field => !field.Item2)
                                                      .Select(field => field.Item1)
                                                      .Sum();

        break;
    }
}

Console.WriteLine($"PartOne: {firstWinner}");
Console.WriteLine($"PartTwo: {lastWinner}");