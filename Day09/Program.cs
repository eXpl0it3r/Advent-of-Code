using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day09
{
    public static class Program
    {
        private const int PreambleLength = 25;

        public static void Main()
        {
            var input = File.ReadLines("input.txt")
                            .Select(l => Convert.ToUInt64(l))
                            .ToList();

            var invalid = PartOne(input);
            Console.WriteLine($"Invalid Number: {invalid}");
            
            PartTwo(input, invalid);
        }

        private static ulong PartOne(IReadOnlyCollection<ulong> input)
        {
            var skip = PreambleLength + 1;

            while (skip < input.Count)
            {
                var preamble = input.Skip(skip - PreambleLength)
                                    .Take(PreambleLength)
                                    .ToList();
                var target = input.Skip(skip)
                                  .Take(1)
                                  .Single();

                var match = preamble.Any(n1 => preamble.Any(n2 => n1 + n2 == target));

                if (!match)
                {
                    return target;
                }

                skip++;
            }

            throw new EndOfStreamException("We reached the end of the file without an invalid number");
        }

        private static void PartTwo(List<ulong> input, ulong invalid)
        {
            var top = input.FindIndex(n => n == invalid);
            var start = 0;
            var end = 0;
            
            for (var i = 0; i < top && start == 0; i++)
            {
                ulong sum = 0;

                for (var j = i; j < top && sum < invalid; j++)
                {
                    sum += input[j];

                    if (sum == invalid)
                    {
                        end = j;
                    }
                }

                if (end != 0)
                {
                    start = i;
                }
            }

            var range = input.Skip(start)
                             .Take(end - start)
                             .ToList();

            Console.WriteLine($"Encryption Weakness: {range.Min() + range.Max()}");
        }
    }
}