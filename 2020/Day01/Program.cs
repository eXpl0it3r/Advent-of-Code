using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day01
{
    public static class Program
    {
        private const int Target = 2020;

        public static void Main()
        {
            var input = File.ReadLines("input.txt")
                            .Select(s => Convert.ToInt32(s))
                            .OrderBy(i => i)
                            .ToList();

            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(List<int> input)
        {
            var (number, result) = input.Where(line => line * 2 <= Target)
                                        .Select(line => (
                                                            number: line,
                                                            result: input.BinarySearch(Target - line)
                                                        ))
                                        .Single(search => search.result >= 0);

            Console.WriteLine($"{number} * {input[result]} = {number * input[result]}");
        }

        private static void PartTwo(List<int> input)
        {
            var (numberOne, numberTwo, result) = input.SelectMany(_ => input,
                                                                  (firstNumber, secondNumber) => (
                                                                          numberOne: firstNumber,
                                                                          numberTwo: secondNumber,
                                                                          result: input.BinarySearch(Target -
                                                                              firstNumber - secondNumber)
                                                                      )
                                                                 )
                                                      .First(search => search.result >= 0);

            Console.WriteLine($"{numberOne} * {numberTwo} * {input[result]} = {numberOne * numberTwo * input[result]}");
        }
    }
}