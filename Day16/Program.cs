using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day16
{
    public static class Program
    {
        public static void Main()
        {
            var input = File.ReadAllText("input.txt")
                            .Split("\n\n")
                            .Select(s => s.Split("\n"))
                            .ToList();

            var rules = input[0].Select(r => r.Split(": ")
                                              .Select(p => p.Split(" or "))
                                              .ToList())
                                .ToDictionary(k => k[0].Single(), v => new List<List<int>>
                                                                       {
                                                                           v[1][0].Split("-")
                                                                               .Select(p => Convert.ToInt32(p))
                                                                               .ToList(),
                                                                           v[1][1].Split("-")
                                                                               .Select(p => Convert.ToInt32(p))
                                                                               .ToList()
                                                                       });
            var myTicket = input[1].Skip(1)
                                   .Single()
                                   .Split(",")
                                   .Select(s => Convert.ToInt32(s))
                                   .ToList();
            var tickets = input[2].Skip(1)
                                  .Select(t => t.Split(",")
                                                .Select(s => Convert.ToInt32(s))
                                                .ToList())
                                  .ToList();

            var errorRate = 0;
            var validTickets = new List<List<int>> {myTicket};

            foreach (var ticket in tickets)
            {
                var ticketErrorRate = ticket.Where(number => !rules.Any(rule => InRanges(rule.Value, number))).Sum();
                errorRate += ticketErrorRate;

                if (ticketErrorRate == 0)
                {
                    validTickets.Add(ticket);
                }
            }
            
            Console.WriteLine($"PartOne: {errorRate}");

            var ruleOrder = DetermineOrderOfRules(rules, validTickets);
            var departure = ruleOrder.Where(r => r.Key.StartsWith("departure"))
                                     .Select(r => r.Value.Single())
                                     .Aggregate(1L, (current, rule) => current * myTicket[rule]);

            Console.WriteLine($"PartTwo: {departure}");
        }

        private static bool InRanges(IReadOnlyList<List<int>> ranges, int number)
        {
            return (number >= ranges[0][0] && number <= ranges[0][1])
                   || (number >= ranges[1][0] && number <= ranges[1][1]);
        }

        private static Dictionary<string, List<int>> DetermineOrderOfRules(Dictionary<string, List<List<int>>> rules, IReadOnlyCollection<List<int>> validTickets)
        {
            var ruleOrder = new Dictionary<string, List<int>>();

            foreach (var (ruleName, ranges) in rules)
            {
                for (var position = 0; position < validTickets.First().Count; position++)
                {
                    if (!validTickets.All(t => InRanges(ranges, t[position])))
                    {
                        continue;
                    }

                    if (ruleOrder.ContainsKey(ruleName))
                    {
                        ruleOrder[ruleName].Add(position);
                    }
                    else
                    {
                        ruleOrder[ruleName] = new List<int> {position};
                    }
                }
            }

            while (ruleOrder.Any(l => l.Key.StartsWith("departure") && l.Value.Count > 1))
            {
                var unique = ruleOrder.Where(l => l.Value.Count == 1)
                                      .SelectMany(l => l.Value)
                                      .ToList();

                foreach (var value in unique)
                {
                    foreach (var rule in ruleOrder.Where(rule => rule.Value.Count > 1 && rule.Value.Contains(value)))
                    {
                        rule.Value.Remove(value);
                    }
                }
            }

            return ruleOrder;
        }
    }
}