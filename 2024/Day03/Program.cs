using System.Text.RegularExpressions;

var input = await File.ReadAllLinesAsync("input.txt");

var resultPart1 = input.Select(line => Regex.Matches(line, @"mul\((\d{1,3}),(\d{1,3})\)")
                                            .Select(m => (int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value))))
                       .Select(values => values.Sum(v => v.Item1 * v.Item2))
                       .Sum();

var resultPart2 = input.Select(line => Regex.Matches(line, @"mul\((\d{1,3}),(\d{1,3})\)")
                                            .Select(m =>
                                                    {
                                                        var disable = line.LastIndexOf("don't()", m.Index, StringComparison.InvariantCulture);
                                                        var enable = line.LastIndexOf("do()", m.Index, StringComparison.InvariantCulture);

                                                        if (disable != -1 && disable > enable)
                                                        {
                                                            return (0, 0);
                                                        }

                                                        return (int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value));
                                                    }))
                       .Select(values => values.Sum(v => v.Item1 * v.Item2))
                       .Sum();

Console.WriteLine($"Part 1: {resultPart1}");
Console.WriteLine($"Part 2: {resultPart2}");