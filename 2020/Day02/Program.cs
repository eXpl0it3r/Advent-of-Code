using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day02
{
    public static class Program
    {
        public static void Main()
        {
            var regex = new Regex(@"(?<min>[1-9][0-9]*)\-(?<max>[1-9][0-9]*) (?<letter>[a-z])\: (?<password>[a-z]+)$",
                                  RegexOptions.Compiled | RegexOptions.IgnoreCase);
            
            var input = File.ReadLines("input.txt")
                            .Select(l => regex.Matches(l).Single())
                            .Select(m => (
                                             min: Convert.ToInt32(m.Groups["min"].Value),
                                             max: Convert.ToInt32(m.Groups["max"].Value),
                                             letter: m.Groups["letter"].Value.Single(),
                                             password: m.Groups["password"].Value
                                         ))
                            .ToList();

            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(IEnumerable<(int min, int max, char letter, string password)> input)
        {
            var result = input.Count(r => r.password.Count(c => c == r.letter) >= r.min &&
                                          r.password.Count(c => c == r.letter) <= r.max);

            Console.WriteLine($"Number of valid passwords: {result}");
        }
        
        private static void PartTwo(IEnumerable<(int min, int max, char letter, string password)> input)
        {
            var result = input.Count(r => (r.password[r.min - 1] == r.letter ^ r.password[r.max - 1] == r.letter));
            
            Console.WriteLine($"Number of valid passwords: {result}");
        }
    }
}