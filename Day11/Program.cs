using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day11
{
    public static class Program
    {
        public static void Main()
        {
            var input = File.ReadLines("input.txt")
                            .Select(l => l.Select(c => c)
                                          .ToList())
                            .ToList();

            PartOne(input.Select(l => new List<char>(l))
                         .ToList());
            PartTwo(input);
        }

        private static void PartTwo(List<List<char>> input)
        {
            var changes = new List<(int x, int y, char seat)>();

            do
            {
                changes.Clear();

                for (var y = 0; y < input.Count; y++)
                {
                    for (var x = 0; x < input[y].Count; x++)
                    {
                        if (input[y][x] == '.')
                        {
                            continue;
                        }

                        var count = CountVisible(input, (x, y));

                        switch (input[y][x])
                        {
                            case 'L' when count == 0:
                                changes.Add((x, y, '#'));
                                break;
                            case '#' when count >= 5:
                                changes.Add((x, y, 'L'));
                                break;
                        }
                    }
                }

                foreach (var (x, y, seat) in changes)
                {
                    input[y][x] = seat;
                }
            } while (changes.Any());

            var result = input.SelectMany(l => l.Select(c => c))
                              .Count(c => c == '#');

            Console.WriteLine($"Result: {result}");
        }

        private static void PartOne(List<List<char>> input)
        {
            var changes = new List<(int x, int y, char seat)>();

            do
            {
                changes.Clear();

                for (var y = 0; y < input.Count; y++)
                {
                    for (var x = 0; x < input[y].Count; x++)
                    {
                        if (input[y][x] == '.')
                        {
                            continue;
                        }

                        var count = CountAdjacent(input, (x, y));

                        switch (input[y][x])
                        {
                            case 'L' when count == 0:
                                changes.Add((x, y, '#'));
                                break;
                            case '#' when count >= 4:
                                changes.Add((x, y, 'L'));
                                break;
                        }
                    }
                }

                foreach (var (x, y, seat) in changes)
                {
                    input[y][x] = seat;
                }
            } while (changes.Any());

            var result = input.SelectMany(l => l.Select(c => c))
                              .Count(c => c == '#');

            Console.WriteLine($"Result: {result}");
        }

        private static int CountVisible(List<List<char>> input, (int x, int y) current)
        {
            var count = 0;
            var dimension = (width: input.First().Count, height: input.Count);

            for (var x = current.x - 1; x >= 0 && input[current.y][x] != 'L'; x--)
            {
                if (input[current.y][x] == '.')
                {
                    continue;
                }
                
                count++;
                break;
            }
            for (var x = current.x + 1; x < dimension.width && input[current.y][x] != 'L'; x++)
            {
                if (input[current.y][x] == '.')
                {
                    continue;
                }

                count++;
                break;
            }
            for (var y = current.y - 1; y >= 0 && input[y][current.x] != 'L'; y--)
            {
                if (input[y][current.x] == '.')
                {
                    continue;
                }

                count++;
                break;
            }
            for (var y = current.y + 1; y < dimension.height && input[y][current.x] != 'L'; y++)
            {
                if (input[y][current.x] == '.')
                {
                    continue;
                }

                count++;
                break;
            }

            for (var z = -1; current.x + z >= 0 && current.y + z >= 0 && input[current.y + z][current.x + z] != 'L'; z--)
            {
                if (input[current.y + z][current.x + z] == '.')
                {
                    continue;
                }

                count++;
                break;
            }
            for (var z = 1; current.x + z < dimension.width && current.y + z < dimension.height && input[current.y + z][current.x + z] != 'L'; z++)
            {
                if (input[current.y + z][current.x + z] == '.')
                {
                    continue;
                }

                count++;
                break;
            }
            for (var z = -1; current.x - z < dimension.width && current.y + z >= 0 && input[current.y + z][current.x - z] != 'L'; z--)
            {
                if (input[current.y + z][current.x - z] == '.')
                {
                    continue;
                }

                count++;
                break;
            }
            for (var z = 1; current.x - z >= 0 && current.y + z < dimension.height && input[current.y + z][current.x - z] != 'L'; z++)
            {
                if (input[current.y + z][current.x - z] == '.')
                {
                    continue;
                }

                count++;
                break;
            }
            
            return count;
        }

        private static int CountAdjacent(List<List<char>> input, (int x, int y) current)
        {
            var count = 0;

            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }

                    var element = input.ElementAtOrDefault(current.y + y)
                                       ?.ElementAtOrDefault(current.x + x);

                    if (element == '#')
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}