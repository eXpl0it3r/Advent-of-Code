using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day05
{
    public static class Program
    {
        public static void Main()
        {
            var input = File.ReadLines("input.txt")
                            .Select(l => (row: l.Where(c => new[] {'F', 'B'}.Contains(c))
                                                .Aggregate((min: 0, max: 127),
                                                           (current, position) => NextRange(position, current))
                                                .min,
                                          column: l.Where(c => new[] {'L', 'R'}.Contains(c))
                                                   .Aggregate((min: 0, max: 7),
                                                              (current, position) => NextRange(position, current))
                                                   .min))
                            .Select(res => (res.row,
                                            res.column,
                                            id: res.row * 8 + res.column))
                            .ToList();
            
            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(IEnumerable<(int row, int column, int id)> input)
        {
            Console.WriteLine($"Max: {input.Max(res => res.id)}");
        }

        private static void PartTwo(IEnumerable<(int row, int column, int id)> input)
        {
            var seats = input.Where(res => res.row > 0 && res.row < 127)
                             .Select(res => res.id)
                             .OrderBy(i => i)
                             .ToList();

            for (var i = 0; i < seats.Count - 2; i++)
            {
                if (seats[i + 1] - seats[i] == 2)
                {
                    Console.WriteLine($"My Seat: {seats[i] + 1}");
                    break;
                }
            }
        }

        private static (int, int) NextRange(char position, (int min, int max) range)
        {
            switch (position)
            {
                case 'F':
                case 'L':
                    return (range.min, range.min + (int) Math.Floor((range.max - range.min) / 2.0));
                case 'B':
                case 'R':
                    return (range.max - (int) Math.Floor((range.max - range.min) / 2.0), range.max);
                default:
                    throw new ArgumentOutOfRangeException($"Unexpected position value provided {position}");
            }
        }
    }
}