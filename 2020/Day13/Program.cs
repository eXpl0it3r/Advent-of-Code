using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Xml;

namespace Day13
{
    public static class Program
    {
        public static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            var departure = Convert.ToInt64(input[0]);
            var schedules = input[1].Split(',');

            PartOne(schedules.Select(s => (string) s.Clone()).ToArray(), departure);
            PartTwo(schedules, departure);
        }

        private static void PartTwo(string[] schedules, long departure)
        {
            var diff = 0;
            var buses = new List<(long id, long diff)>();
            
            foreach (var schedule in schedules)
            {
                if (schedule == "x")
                {
                    diff++;
                    continue;
                }
                
                buses.Add((Convert.ToInt64(schedule), diff));
                diff++;
            }

            foreach (var b in buses)
            {
                Console.WriteLine($"{b.id} {b.diff}");
            }

            var start = buses.First();

            for (long i = 2702702702702; true; i++)
            {
                var found = true;

                foreach (var bus in buses.Skip(1))
                {
                    found = (i * start.id + bus.diff) % bus.id == 0;
                    
                    if (!found)
                    {
                        break;
                    }
                }

                if (found)
                {
                    Console.WriteLine($"Found: {i * start.id}");
                    break;
                }
            }
        }

        private static void PartOne(string[] schedules, long departure)
        {
            var bus = schedules.Where(b => b != "x")
                               .Select(b => Convert.ToInt64(b))
                               .OrderBy(b => departure / b * b + b - departure)
                               .First();
            var number = departure / bus * bus + bus - departure;

            Console.WriteLine($"Found: {bus} * {number} = {number * bus}");
        }
    }
}