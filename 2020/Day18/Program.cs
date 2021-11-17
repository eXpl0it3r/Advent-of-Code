using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day18
{
    public static class Program
    {
        public static void Main()
        {
            var input = File.ReadLines("input.txt")
                            .Select(l => l.Split(" "));

            foreach (var line in input)
            {
                var stack = new Stack<(int value, char op)>();
                var current = (value: 0, op: ' ');

                foreach (var expression in line)
                {
                    if (expression.StartsWith("("))
                    {
                        foreach (var _ in expression.Where(c => c == '('))
                        {
                            stack.Push(current);
                            current = (0, ' ');
                        }

                        current = CalculateNewCurrent(current, expression.Substring(expression.LastIndexOf("(", StringComparison.InvariantCultureIgnoreCase) + 1));
                        continue;
                    }

                    if (expression.EndsWith(")"))
                    {
                        current = CalculateNewCurrent(current, expression.Substring(expression.IndexOf("(", 0, StringComparison.InvariantCultureIgnoreCase)));
                        
                        foreach (var _ in expression.Where(c => c == ')'))
                        {
                            var previous = stack.Pop();
                            //current = CalculateNewCurrent(previous, next.);
                        }
                    }
                    
                    switch (expression)
                    {
                        case "+":
                            current = (current.value, '+');
                            continue;
                        case "*":
                            current = (current.value, '*');
                            continue;
                    }

                    current = CalculateNewCurrent(current, expression);
                }
                
                Console.WriteLine($"{current.value}");
            }
        }

        private static (int value, char op) CalculateNewCurrent((int value, char op) current, string expression)
        {
            var value = Convert.ToInt32(expression);

            return current.op switch
                   {
                       ' ' => (value, ' '),
                       '+' => (current.value + value, ' '),
                       '*' => (current.value * value, ' '),
                       _ => current
                   };
        }
    }
}