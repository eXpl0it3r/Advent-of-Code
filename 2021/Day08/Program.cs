var input = File.ReadAllLines("input.txt")
                .Select(l =>
                        {
                            var inputOutput = l.Split(" | ");
                            return new
                                   {
                                       Signal = inputOutput[0].Split(' '),
                                       Output = inputOutput[1].Split(' ')
                                   };
                        })
                .ToList();

var count = input.Select(i => i.Output.Select(o => o.Length))
                 .SelectMany(o => o)
                 .Count(o => o is 2 or 4 or 3 or 7);

Console.WriteLine($"PartOne: {count}");

var solution = 0;

foreach (var line in input)
{
    var digits = Enumerable.Range(0, 9)
                           .ToDictionary(i => i, _ => string.Empty);

    digits[1] = line.Signal.Single(s => s.Length == 2);
    digits[4] = line.Signal.Single(s => s.Length == 4);
    digits[7] = line.Signal.Single(s => s.Length == 3);
    digits[8] = line.Signal.Single(s => s.Length == 7);

    /*
     * ## 0 ##
     * 1     2
     * ## 3 ##
     * 4     5
     * ## 6 ##
     */
    var parts = Enumerable.Repeat((char) 0, 7).ToList();
    var zeroOrSixOrNine = line.Signal.Where(s => s.Length == 6).ToList();
    var twoOrThreeOrFive = line.Signal.Where(s => s.Length == 5).ToList();

    parts[0] = digits[7].Single(c => !digits[1].Contains(c));
    parts[3] = twoOrThreeOrFive[0].Intersect(twoOrThreeOrFive[1])
                                  .Intersect(twoOrThreeOrFive[2])
                                  .Single(s => s != parts[0] && digits[4].Contains(s));
    parts[6] = twoOrThreeOrFive[0].Intersect(twoOrThreeOrFive[1])
                                  .Intersect(twoOrThreeOrFive[2])
                                  .Single(s => s != parts[0] && s != parts[3]);
    digits[0] = zeroOrSixOrNine.Single(d => !d.Contains(parts[3]));
    digits[3] = twoOrThreeOrFive.Single(s => s.Contains(parts[0]) && s.Contains(parts[3]) && s.Contains(parts[6]) &&
                                             s.Contains(digits[1][0]) && s.Contains(digits[1][1]));
    digits[6] = zeroOrSixOrNine.Where(d => d != digits[0])
                               .Single(c => !(c.Contains(digits[1][0]) && c.Contains(digits[1][1])));
    parts[2] = digits[1].Single(c => !digits[6].Contains(c));
    digits[2] = twoOrThreeOrFive.Where(d => d != digits[3])
                                .Single(d => d.Contains(parts[2]));
    digits[5] = twoOrThreeOrFive.Single(d => d != digits[3] && !d.Contains(parts[2]));
    digits[9] = zeroOrSixOrNine.Single(d => d != digits[0] && d != digits[6]);

    var output = line.Output.Aggregate(string.Empty, (solutionOutput, output) => digits.Where(digit => output.OrderBy(e => e)
                                                                                                             .SequenceEqual(digit.Value.OrderBy(e => e)))
                                                                                       .Aggregate(solutionOutput, (solutionDigit, digit) => solutionDigit + digit.Key));

    solution += int.Parse(output);
}

Console.WriteLine($"PartTwo: {solution}");