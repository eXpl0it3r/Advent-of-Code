using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = new[] {0}.Union(File.ReadLines("input.txt")
                                            .Select(a => Convert.ToInt32(a))
                                            .OrderBy(a => a))
                                 .ToList();

            PartOne(input);
            PartTwo(input);
        }

        private static long CountPath(IReadOnlyList<(int jolt, List<long> count)> input, int index, int distance)
        {
            // Only one final destination
            if (index == input.Count - 1 && distance == 3)
            {
                return 1;
            }

            if (input[index].count[distance - 1] > -1)
            {
                return input[index].count[distance - 1];
            }

            var count = 0L;

            for (var i = 1; i <= 3; i++)
            {
                if (index + i < input.Count && input[index + i].jolt - input[index].jolt == distance)
                {
                    count = CountPath(input, index + i, 1)
                            + CountPath(input, index + i, 2)
                            + CountPath(input, index + i, 3);
                }
            }

            input[index].count[distance - 1] = count;
            return input[index].count[distance - 1];
        }

        private static void PartTwo(IEnumerable<int> input)
        {
            var inputWithMemory = input.Select(a => (jolt: a, count: new List<long> {-1L, -1L, -1L}))
                                       .ToList();

            var count = CountPath(inputWithMemory, 0, 1)
                        + CountPath(inputWithMemory, 0, 2)
                        + CountPath(inputWithMemory, 0, 3);

            Console.WriteLine($"Possibilities: {count}");
        }

        private static void PartOne(IReadOnlyList<int> input)
        {
            var diff = 0;
            var countOne = 0;
            var countThree = 1;

            for (var i = 0; i < input.Count - 1 && diff <= 3; i++)
            {
                diff = input[i + 1] - input[i];

                switch (diff)
                {
                    case 1:
                        countOne++;
                        break;
                    case 3:
                        countThree++;
                        break;
                }
            }

            Console.WriteLine($"Calc: {countOne} * {countThree} = {countOne * countThree}");
        }
    }
}