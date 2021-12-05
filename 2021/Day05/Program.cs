using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day05
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Line
    {
        public Point Start { get; set; } = new() {X = 0, Y = 0};
        public Point End { get; set; } = new() {X = 0, Y = 0};
    }

    public static class Program
    {
        public static void Main()
        {
            var input = File.ReadAllLines("input.txt")
                            .Select(l => l.Split(" -> ")
                                          .Select(p =>
                                                  {
                                                      var point = p.Split(',');
                                                      return new Point
                                                             {
                                                                 X = int.Parse(point[0]),
                                                                 Y = int.Parse(point[1])
                                                             };
                                                  })
                                          .ToList())
                            .Select(p => new Line
                                         {
                                             Start = p[0].X <= p[1].X && p[0].Y <= p[1].Y ? p[0] : p[1],
                                             End = p[0].X <= p[1].X && p[0].Y <= p[1].Y ? p[1] : p[0]
                                         })
                            .ToList();

            var grid = new List<List<int>>();

            for (var y = 0; y < 1000; y++)
            {
                grid.Add(new List<int>());

                for (var x = 0; x < 1000; x++)
                {
                    grid[y].Add(0);
                }
            }

            var straightLines = input.Where(l => l.Start.X == l.End.X || l.Start.Y == l.End.Y)
                                     .ToList();

            foreach (var line in straightLines)
            {
                for (var y = line.Start.Y; y <= line.End.Y; y++)
                {
                    for (var x = line.Start.X; x <= line.End.X; x++)
                    {
                        grid[y][x] += 1;
                    }
                }
            }

            var partOne = grid.SelectMany(g => g)
                              .Count(s => s >= 2);

            Console.WriteLine($"PartOne: {partOne}");

            var diagonalLines = input.Where(l => l.Start.X != l.End.X && l.Start.Y != l.End.Y)
                                     .ToList();

            foreach (var line in diagonalLines)
            {
                var y = line.Start.Y;
                var x = line.Start.X;

                if (line.Start.Y < line.End.Y)
                {
                    if (line.Start.X < line.End.X)
                    {
                        for (; y <= line.End.Y && x <= line.End.X; y++, x++)
                        {
                            grid[y][x] += 1;
                        }
                    }
                    else
                    {
                        for (; y <= line.End.Y && x >= line.End.X; y++, x--)
                        {
                            grid[y][x] += 1;
                        }
                    }
                }
                else
                {
                    if (line.Start.X < line.End.X)
                    {
                        for (; y >= line.End.Y && x <= line.End.X; y--, x++)
                        {
                            grid[y][x] += 1;
                        }
                    }
                    else
                    {
                        for (; y >= line.End.Y && x >= line.End.X; y--, x--)
                        {
                            grid[y][x] += 1;
                        }
                    }
                }
            }

            var partTwo = grid.SelectMany(g => g)
                              .Count(s => s >= 2);

            Console.WriteLine($"PartTwo: {partTwo}");
        }
    }
}