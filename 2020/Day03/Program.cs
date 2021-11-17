using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day03
{
    public static class Program
    {
        public static void Main()
        {
            var input = File.ReadLines("input.txt")
                            .ToList();

            var partOne = CountTrees(input, 3, 1);
            Console.WriteLine($"Number of trees: {partOne}");

            Console.WriteLine($"{CountTrees(input, 1, 1)}, {CountTrees(input, 3, 1)}, {CountTrees(input, 5, 1)}, {CountTrees(input, 7, 1)}, {CountTrees(input, 1, 2)}");

            var partTwo = CountTrees(input, 1, 1)
                          * CountTrees(input, 3, 1)
                          * CountTrees(input, 5, 1)
                          * CountTrees(input, 7, 1)
                          * CountTrees(input, 1, 2);
            Console.WriteLine($"Number of trees: {partTwo}");
        }

        private static int CountTrees(IEnumerable<string> input, int right, int down)
        {
            var offset = 0;
            var trees = 0;
            var rows = 0;

            foreach (var line in input)
            {
                rows++;

                // Skip row
                if (down > 1 && rows % down == 0) continue;

                if (line[offset % 31] == '#') trees++;

                offset += right;
            }

            return trees;
        }
    }
}