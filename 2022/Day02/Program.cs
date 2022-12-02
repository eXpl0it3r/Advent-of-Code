var input = File.ReadAllLines("input.txt")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(s => s.Split(' '))
                .Select(s => new Tuple<string, string>(s[0], s[1]))
                .ToList();

var score = input.Sum(strategy => strategy switch
                                  {
                                      {Item1: "A", Item2: "Z"} => 0 + 3,
                                      {Item1: "B", Item2: "X"} => 0 + 1,
                                      {Item1: "C", Item2: "Y"} => 0 + 2,
                                      {Item1: "A", Item2: "X"} => 3 + 1,
                                      {Item1: "B", Item2: "Y"} => 3 + 2,
                                      {Item1: "C", Item2: "Z"} => 3 + 3,
                                      {Item1: "C", Item2: "X"} => 6 + 1,
                                      {Item1: "A", Item2: "Y"} => 6 + 2,
                                      {Item1: "B", Item2: "Z"} => 6 + 3
                                  });

Console.WriteLine($"PartOne: {score}");

var newScore = input.Sum(strategy => strategy switch
                                     {
                                         {Item1: "A", Item2: "X"} => 0 + 3,
                                         {Item1: "B", Item2: "X"} => 0 + 1,
                                         {Item1: "C", Item2: "X"} => 0 + 2,
                                         {Item1: "A", Item2: "Y"} => 3 + 1,
                                         {Item1: "B", Item2: "Y"} => 3 + 2,
                                         {Item1: "C", Item2: "Y"} => 3 + 3,
                                         {Item1: "A", Item2: "Z"} => 6 + 2,
                                         {Item1: "B", Item2: "Z"} => 6 + 3,
                                         {Item1: "C", Item2: "Z"} => 6 + 1
                                     });

Console.WriteLine($"PartTwo: {newScore}");