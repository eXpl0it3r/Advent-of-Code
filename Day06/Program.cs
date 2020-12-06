using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06
{
    public static class Program
    {
        private const string NewLine = "\n";

        public static void Main()
        {
            var input = File.ReadAllText("input.txt")
                            .Split($"{NewLine}{NewLine}");


            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(IEnumerable<string> input)
        {
            var result = input.Select(g => g.Split($"{NewLine}")
                                            .SelectMany(l => l)
                                            .Distinct()
                                            .Count())
                              .Sum();

            Console.WriteLine(result);
        }

        private static void PartTwo(IEnumerable<string> input)
        {
            var result = input.Select(group => group.Split($"{NewLine}"))
                              .Select(group => group.SelectMany(c => c)
                                                    .GroupBy(letter => letter)
                                                    .Count(grouping => grouping.Count() == group.Length))
                              .Sum();

            Console.WriteLine(result);
        }
    }
}