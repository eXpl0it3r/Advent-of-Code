using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
    public static class Program
    {
        public static void Main()
        {
            var input = File.ReadLines("input.txt")
                            .Select(l => (direction: l.First(), amount: Convert.ToInt32(l.Substring(1))))
                            .ToList();

            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(IEnumerable<(char direction, int amount)> input)
        {
            (int x, int y) direction = (x: 1, y: 0);
            var position = (x: 0, y: 0);
            
            foreach (var instruction in input)
            {
                switch (instruction.direction)
                {
                    case 'N':
                        position.y -= instruction.amount;
                        break;
                    case 'S':
                        position.y += instruction.amount;
                        break;
                    case 'E':
                        position.x += instruction.amount;
                        break;
                    case 'W':
                        position.x -= instruction.amount;
                        break;
                    case 'L':
                        for (var i = 0; i < instruction.amount / 90; i++)
                        {
                            direction = RotateLeft(direction);
                        }

                        break;
                    case 'R':
                        for (var i = 0; i < instruction.amount / 90; i++)
                        {
                            direction = RotateRight(direction);
                        }

                        break;
                    case 'F':
                        position.x += direction.x * instruction.amount;
                        position.y += direction.y * instruction.amount;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Unknown direction {instruction.direction}");
                }
            }

            var (x, y) = position;

            Console.WriteLine($"{x}, {y} -> {Math.Abs(x) + Math.Abs(y)}");
        }
        
        private static void PartTwo(IEnumerable<(char direction, int amount)> input)
        {
            var position = (x: 0, y: 0);
            var waypoint = (x: 10, y: -1); // 10 East, 1 North

            foreach (var instruction in input)
            {
                switch (instruction.direction)
                {
                    case 'N':
                        waypoint.y -= instruction.amount;
                        break;
                    case 'S':
                        waypoint.y += instruction.amount;
                        break;
                    case 'E':
                        waypoint.x += instruction.amount;
                        break;
                    case 'W':
                        waypoint.x -= instruction.amount;
                        break;
                    case 'L':
                        for (var i = 0; i < instruction.amount / 90; i++)
                        {
                            waypoint = RotateLeft(waypoint);
                        }

                        break;
                    case 'R':
                        for (var i = 0; i < instruction.amount / 90; i++)
                        {
                            waypoint = RotateRight(waypoint);
                        }

                        break;
                    case 'F':
                        position.x += waypoint.x * instruction.amount;
                        position.y += waypoint.y * instruction.amount;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Unknown direction {instruction.direction}");
                }
            }

            Console.WriteLine($"{position.x}, {position.y} / {waypoint.x}, {waypoint.y} -> {Math.Abs(position.x) + Math.Abs(position.y)}");
        }

        private static (int x, int y) RotateRight((int x, int y) direction)
        {
            return (x: (direction.y > 0 ? -1 : 1) * Math.Abs(direction.y), y: direction.x);
        }
        private static (int x, int y) RotateLeft((int x, int y) direction)
        {
            return (x: direction.y, y: (direction.x > 0 ? -1 : 1) * Math.Abs(direction.x));
        }
    }
}