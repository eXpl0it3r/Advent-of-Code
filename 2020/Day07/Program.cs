using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day07
{
    public static class Program
    {
        private const string Target = "shiny gold";

        public static void Main()
        {
            const string Contains = " bags contain ";

            var input = File.ReadLines("input.txt")
                            .Where(l => !l.Contains("no other bags", StringComparison.InvariantCultureIgnoreCase))
                            .Select(l => (
                                             bag:
                                             l.Substring(0,
                                                         l.IndexOf(Contains,
                                                                   StringComparison.InvariantCultureIgnoreCase)),
                                             bags: l
                                                   .Substring(l.IndexOf(Contains,
                                                                        StringComparison.InvariantCultureIgnoreCase) +
                                                              Contains.Length)
                                                   .Replace(".", string.Empty)
                                                   .Split(", ")
                                                   .Select(b => (
                                                                    bag:
                                                                    b.Substring(2,
                                                                                    b.IndexOf(" bag",
                                                                                        StringComparison
                                                                                            .InvariantCultureIgnoreCase) -
                                                                                    2),
                                                                    count: Convert.ToInt32(b.Substring(0, 1))
                                                                ))
                                         ))
                .ToDictionary(g => g.bag, g => g.bags);
            
            PartOne(input);
            PartTwo(input);
        }

        private static void PartTwo(Dictionary<string, IEnumerable<(string bag, int count)>> input)
        {
            var sum = Summing(input, Target) - 1;
            Console.WriteLine($"Sum: {sum}");
        }

        private static int Summing(Dictionary<string, IEnumerable<(string bag, int count)>> input, string target)
        {
            if (!input.ContainsKey(target))
            {
                return 1;
            }
            
            return 1 + input[target].Sum(bag => bag.count * Summing(input, bag.bag));
        }

        private static void PartOne(Dictionary<string, IEnumerable<(string bag, int count)>> input)
        {
            var solutions = FindSolutions(input, Target);
            Console.WriteLine($"Bags: {solutions.Distinct().Count()}");
        }

        private static IEnumerable<string> FindSolutions(Dictionary<string, IEnumerable<(string bag, int count)>> input, string target)
        {
            return FindBags(input, target).Aggregate(FindBags(input, target), (current, bag) => current.Union(FindSolutions(input, bag)));
        }

        private static IEnumerable<string> FindBags(Dictionary<string, IEnumerable<(string bag, int count)>> input, string target)
        {
            return input.Where(b => b.Key != target && b.Value.Any(x => x.bag == target))
                        .Select(b => b.Key);
        }
    }
}