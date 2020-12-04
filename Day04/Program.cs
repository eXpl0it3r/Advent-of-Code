using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day04
{
    public static class Program
    {
        public static void Main()
        {
            const string NewLine = "\n";
            var requiredKeys = new[] {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};

            var input = File.ReadAllText("input.txt")
                            .Split($"{NewLine}{NewLine}")
                            .Select(p => p.Replace($"{NewLine}", " ")
                                          .Split(" ")
                                          .Select(kvp => kvp.Split(":"))
                                          .ToDictionary(kvp => kvp[0], kvp => kvp[1]))
                            .Where(p => requiredKeys.All(p.ContainsKey))
                            .ToList();

            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(ICollection input)
        {
            Console.WriteLine($"\"Valid\" Passports: {input.Count}");
        }

        private static void PartTwo(IEnumerable<Dictionary<string, string>> input)
        {
            var validatedInput = input.Where(d => InRange(d["byr"], 1920, 2002)
                                                  && InRange(d["iyr"], 2010, 2020)
                                                  && InRange(d["eyr"], 2020, 2030)
                                                  && ValidHeight(d["hgt"])
                                                  && ValidHairColor(d["hcl"])
                                                  && ValidEyeColor(d["ecl"])
                                                  && ValidPassportId(d["pid"]));
            
            Console.WriteLine($"\"Valid\" Passports: {validatedInput.Count()}");
        }

        private static bool InRange(string value, int lower, int upper)
        {
            var integer = Convert.ToInt32(value);
            return integer >= lower && integer <= upper;
        }

        private static bool ValidHeight(string height)
        {
            var regex = new Regex("^(?<height>[1-9][0-9]*)(?<unit>cm|in)$");
            var match = regex.Match(height);

            if (!match.Success)
            {
                return false;
            }

            return match.Groups["unit"].Value == "cm"
                       ? InRange(match.Groups["height"].Value, 150, 193)
                       : match.Groups["unit"].Value == "in" && InRange(match.Groups["height"].Value, 59, 76);
        }

        private static bool ValidHairColor(string hairColor)
        {
            return Regex.IsMatch(hairColor, "^#[0-9a-f]{6}$");
        }

        private static bool ValidEyeColor(string eyeColor)
        {
            var validEyeColors = new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

            return validEyeColors.Contains(eyeColor);
        }

        private static bool ValidPassportId(string passportId)
        {
            return Regex.IsMatch(passportId, "^[0-9]{9}$");
        }
    }
}