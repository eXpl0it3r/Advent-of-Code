using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    public static class Program
    {
        public static void Main()
        {
            var input = File.ReadLines("input.txt")
                            .Select(l => l.Split(',')
                                          .Select(s => Convert.ToInt32(s))
                                          .ToList())
                            .Single();

            var partOne = MemoryGame(input, 2020);
            Console.WriteLine($"Round 2020: {partOne}");
            var partTwo = MemoryGame(input, 30000000);
            Console.WriteLine($"Round 30000000: {partTwo}");
        }

        private static int MemoryGame(List<int> input, int limit)
        {
            var memory = new Dictionary<int, int>();
            var rounds = 1;

            for (var i = 0; i < input.Count - 1; i++)
            {
                memory[input[i]] = rounds;
                rounds++;
            }

            // Do not memorize the last starting number
            var last = input.Last();
            rounds++;

            while (rounds <= limit)
            {
                if (memory.ContainsKey(last))
                {
                    var temp = memory[last];
                    memory[last] = rounds - 1;
                    last = (rounds - 1) - temp;
                }
                else
                {
                    memory[last] = rounds - 1;
                    last = 0;
                }

                rounds++;
            }

            return last;
        }
    }
}