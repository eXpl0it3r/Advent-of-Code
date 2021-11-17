using System;
using System.IO;
using System.Linq;

namespace Day08
{
    public static class Program
    {
        public static void Main()
        {
            var instructions = File.ReadLines("input.txt")
                                   .Select(l => l.Split(" "))
                                   .Select(i => (instruction: i[0], amount: Convert.ToInt32(i[1]), visited: false))
                                   .ToArray();

            PartOne(instructions);
            PartTwo(instructions);
        }

        private static void PartOne((string instruction, int amount, bool visited)[] instructions)
        {
            var accumulator = 0;
            var current = 0;
            var next = current;

            while (instructions[next].visited == false)
            {
                current = next;

                next = ParseInstructions(instructions, current, ref accumulator);

                instructions[current].visited = true;
            }

            Console.WriteLine($"Acc: {accumulator}");
        }

        private static int ParseInstructions((string instruction, int amount, bool visited)[] instructions, int current,
                                             ref int accumulator)
        {
            int next;
            switch (instructions[current].instruction)
            {
                case "nop":
                    next = current + 1;
                    break;
                case "acc":
                    accumulator += instructions[current].amount;
                    next = current + 1;
                    break;
                case "jmp":
                    next = current + instructions[current].amount;
                    break;
                default:
                    throw new
                        ArgumentOutOfRangeException($"Instruction {instructions[current].instruction} not supported");
            }

            return next;
        }

        private static void PartTwo((string instruction, int amount, bool visited)[] instructions)
        {
            var found = false;

            for (var i = 0; i < instructions.Length && !found; i++)
            {
                var inst = CloneInstructions(instructions);

                inst[i].instruction = inst[i].instruction switch
                                      {
                                          "nop" => "jmp",
                                          "jmp" => "nop",
                                          _ => inst[i].instruction
                                      };

                var accumulator = 0;
                var current = 0;
                var next = current;

                while (next != inst.Length && inst[next].visited == false)
                {
                    current = next;
                    next = ParseInstructions(inst, current, ref accumulator);

                    inst[current].visited = true;

                    if (next == inst.Length)
                    {
                        Console.WriteLine($"Found instruction to fix: {current}");
                        Console.WriteLine($"Acc: {accumulator}");
                        found = true;
                    }
                }
            }
        }

        private static (string instruction, int amount, bool visited)[] CloneInstructions(
            (string instruction, int amount, bool visited)[] instructions)
        {
            return instructions.Select(a => (
                                                instruction: (string) a.instruction.Clone(),
                                                a.amount,
                                                visited: false
                                            ))
                               .ToArray();
        }
    }
}