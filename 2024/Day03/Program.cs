using System.Text.RegularExpressions;

var input = await File.ReadAllTextAsync("input.txt");

var resultPart1 = Regex.Matches(input, @"mul\((\d{1,3}),(\d{1,3})\)")
                       .Select(m => (int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value)))
                       .Sum(v => v.Item1 * v.Item2);

var resultPart2 = Regex.Matches(input, @"mul\((\d{1,3}),(\d{1,3})\)")
                       .Select(m =>
                               {
                                   var disable = input.LastIndexOf("don't()", m.Index, StringComparison.InvariantCulture);
                                   var enable = input.LastIndexOf("do()", m.Index, StringComparison.InvariantCulture);

                                   if (disable != -1 && disable > enable)
                                   {
                                       return (0, 0);
                                   }

                                   return (int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value));
                               })
                       .Sum(v => v.Item1 * v.Item2);

Console.WriteLine($"Part 1: {resultPart1}");
Console.WriteLine($"Part 2: {resultPart2}");