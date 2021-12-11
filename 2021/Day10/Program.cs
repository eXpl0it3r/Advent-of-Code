var input = File.ReadAllLines("input.txt")
                .Where(l => l.Length > 0);
var syntaxPoints = new Dictionary<char, int>
                   {
                       {')', 3},
                       {']', 57},
                       {'}', 1197},
                       {'>', 25137}
                   };
var completionPoints = new Dictionary<char, long>
                       {
                           {')', 1},
                           {']', 2},
                           {'}', 3},
                           {'>', 4}
                       };
var mapping = new Dictionary<char, char>
              {
                  {'(', ')'},
                  {'[', ']'},
                  {'{', '}'},
                  {'<', '>'}
              };

var partOne = 0;
var scores = new List<long>();

foreach (var line in input)
{
    var stack = new Stack<char>();

    foreach (var character in line)
    {
        if (character is '(' or '[' or '<' or '{')
        {
            stack.Push(character);
        }
        else if (character is ')' or ']' or '>' or '}')
        {
            // Corrupt Line
            if (stack.Any() && mapping[stack.Pop()] != character)
            {
                partOne += syntaxPoints[character];
                stack.Clear();
                break;
            }
        }
    }

    // Incomplete Line
    if (stack.Any())
    {
        scores.Add(stack.Aggregate(0L, (current, liner) => current * 5L + completionPoints[mapping[liner]]));
    }
}

var partTwo = scores.OrderBy(s => s)
                    .Skip(scores.Count / 2)
                    .Take(1)
                    .Single();


Console.WriteLine($"PartOne: {partOne}");
Console.WriteLine($"PartTwo: {partTwo}");