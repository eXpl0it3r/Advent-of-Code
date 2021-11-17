using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day14
{
    public static class Program
    {
        private const int Bits = 36;
        private const string MaskStart = "mask = ";
        private const string DefaultMask = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

        private static readonly Regex MemoryRegex = new("^mem\\[(?<address>\\d+)\\] = (?<value>\\d+)$",
                                                        RegexOptions.Singleline | RegexOptions.Compiled);

        public static void Main()
        {
            var input = File.ReadLines("input.txt")
                            .ToList();

            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(IEnumerable<string> input)
        {
            var memory = new Dictionary<ulong, ulong>();
            var mask = DefaultMask;

            foreach (var line in input)
            {
                if (line.StartsWith(MaskStart))
                {
                    mask = line.Substring(MaskStart.Length);
                }
                else
                {
                    var match = MemoryRegex.Match(line);

                    if (!match.Success)
                    {
                        throw new ArgumentException("Memory pattern is invalid");
                    }

                    var address = Convert.ToUInt64(match.Groups["address"].Value);
                    var value = Convert.ToUInt64(match.Groups["value"].Value);

                    memory[address] = ApplyMask(value, mask);
                }
            }

            var sum = memory.Aggregate<KeyValuePair<ulong, ulong>, ulong>(0, (current, mem) => current + mem.Value);

            Console.WriteLine($"Sum: {sum}");
        }

        private static ulong ApplyMask(ulong value, string mask)
        {
            var bits = Convert.ToString((long) value, 2)
                              .PadLeft(Bits, '0');
            var output = Enumerable.Range(1, Bits)
                                   .Select(_ => '0')
                                   .ToArray();

            for (var i = 0; i < bits.Length; i++)
            {
                output[i] = mask[i] == 'X' ? bits[i] : mask[i];
            }

            return Convert.ToUInt64(new string(output), 2);
        }

        private static void PartTwo(IEnumerable<string> input)
        {
            var memory = new Dictionary<ulong, ulong>();
            var mask = DefaultMask;

            foreach (var line in input)
            {
                if (line.StartsWith(MaskStart))
                {
                    mask = line.Substring(MaskStart.Length);
                }
                else
                {
                    var match = MemoryRegex.Match(line);

                    if (!match.Success)
                    {
                        throw new ArgumentException("Memory pattern is invalid");
                    }

                    var address = Convert.ToUInt64(match.Groups["address"].Value);
                    var value = Convert.ToUInt64(match.Groups["value"].Value);

                    var addresses = GenerateAddresses(address, mask);

                    foreach (var addr in addresses)
                    {
                        memory[addr] = value;
                    }
                }
            }

            var sum = memory.Aggregate<KeyValuePair<ulong, ulong>, ulong>(0, (current, mem) => current + mem.Value);

            Console.WriteLine($"Sum: {sum}");
        }

        private static IEnumerable<ulong> GenerateAddresses(ulong address, string mask)
        {
            var bits = Convert.ToString((long) address, 2)
                              .PadLeft(Bits, '0');
            var output = Enumerable.Range(1, Bits)
                                   .Select(_ => '0')
                                   .ToArray();

            for (var i = 0; i < bits.Length; i++)
            {
                output[i] = mask[i] == '0' ? bits[i] : mask[i];
            }

            var addresses = new List<string> {new(output)};

            while (addresses.Any(s => s.Any(c => c == 'X')))
            {
                var newAddresses = new List<string>();
                var removeAddresses = new List<string>();

                foreach (var addr in addresses.Where(s => s.Contains('X')))
                {
                    removeAddresses.Add(addr);

                    var pos = addr.LastIndexOf('X');
                    newAddresses.Add(addr.Substring(0, pos) + "0" + addr.Substring(pos + 1));
                    newAddresses.Add(addr.Substring(0, pos) + "1" + addr.Substring(pos + 1));
                }

                foreach (var removeAddress in removeAddresses)
                {
                    addresses.Remove(removeAddress);
                }

                addresses.AddRange(newAddresses);
            }

            return addresses.Select(a => Convert.ToUInt64(new string(a), 2));
        }
    }
}