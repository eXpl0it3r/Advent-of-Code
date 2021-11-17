using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day17
{
    public static class Program
    {
        private const char Active = '#';
        private const int Iterations = 2;

        private static int _dimension;

        public static void Main()
        {
            var input = File.ReadLines("input.txt")
                            .Select(l => l.Select(c => c == Active)
                                          .ToList())
                            .ToList();

            _dimension = input.Count;

            PartOne(input);
        }

        private static void PartTwo(List<List<bool>> input)
        {
            var hyper = Enumerable.Range(1, 1 + Iterations * 2)
                                  .Select(_ => Enumerable.Range(1, 1 + Iterations * 2)
                                                         .Select(_ => Enumerable.Range(1, _dimension + Iterations * 2)
                                                                                .Select(_ => Enumerable.Range(1, _dimension + Iterations * 2)
                                                                                                       .Select(_ => (state: false, change: false))
                                                                                                       .ToList())
                                                                                .ToList())
                                                         .ToList())
                                  .ToList();

            SetInitialState4D(hyper, input);

            for (var i = 1; i <= Iterations; i++)
            {
                Console.WriteLine($"Iteration {i}");

                for (var w = Iterations - i; w < Iterations + i; w++)
                {
                    for (var z = Iterations - i; z <= Iterations + i; z++)
                    {
                        Console.WriteLine($"z = {z}");

                        for (var y = Iterations - i; y < Iterations + _dimension + i; y++)
                        {
                            for (var x = Iterations - i; x < Iterations + _dimension + i; x++)
                            {
                                var activeNeighbors = CountActiveNeighbors4D(hyper, (x, y, z, w));
                                // Console.WriteLine($"{x}, {y}, {z}: {activeNeighbors} / {cubes[z][y][x]}");

                                //Console.Write($"{(cubes[z][y][x].state ? "1" : "0")} ");
                                Console.Write($"{activeNeighbors} ");

                                if (hyper[w][z][y][x].state)
                                {
                                    if (activeNeighbors != 2 && activeNeighbors != 3)
                                    {
                                        hyper[w][z][y][x] = (hyper[w][z][y][x].state, true);
                                    }
                                }
                                else
                                {
                                    if (activeNeighbors == 3)
                                    {
                                        hyper[w][z][y][x] = (hyper[w][z][y][x].state, true);
                                    }
                                }
                            }

                            Console.Write("\n");
                        }

                        Console.Write("\n");
                    }
                }

                ApplyState4D(hyper);

                for (var z = Iterations - i; z <= Iterations + i; z++)
                {
                    PrintSlice4D(hyper);
                    Console.Write("\n");
                }
            }

            var count = hyper.SelectMany(s => s.SelectMany(l => l.SelectMany(c => c))).Sum(w => w.state ? 1 : 0);
            Console.WriteLine($"Sum: {count}");
        }

        private static void SetInitialState4D(List<List<List<List<(bool state, bool change)>>>> cubes, List<List<bool>> input)
        {
            for (var y = 0; y < _dimension; y++)
            {
                for (var x = 0; x < _dimension; x++)
                {
                    cubes[Iterations][Iterations][Iterations + y][Iterations + x] = (input[y][x], false);
                }
            }

            PrintSlice4D(cubes);
        }

        private static void PrintSlice4D(List<List<List<List<(bool state, bool change)>>>> cubes)
        {
            for (var w = 0; w < cubes.Count; w++)
            {
                for (var z = 0; z < cubes[w].Count; z++)
                {
                    Console.WriteLine($"z = {z}, w = {w}");
                    
                    for (var y = 0; y < cubes[w][z].Count; y++)
                    {
                        for (var x = 0; x < cubes[w][z][y].Count; x++)
                        {
                            Console.Write($"{(cubes[w][z][y][x].state ? 1 : 0)} ");
                        }

                        Console.Write("\n");
                    }
                    
                    Console.Write("\n");
                }
            }
        }

        private static void ApplyState4D(List<List<List<List<(bool state, bool change)>>>> cubes)
        {
            for (var w = 0; w < cubes.Count; w++)
            {
                for (var z = 0; z < cubes[w].Count; z++)
                {
                    for (var y = 0; y < cubes[w][z].Count; y++)
                    {
                        for (var x = 0; x < cubes[w][z][y].Count; x++)
                        {
                            if (cubes[w][z][y][x].change)
                            {
                                cubes[w][z][y][x] = (!cubes[w][z][y][x].state, false);
                            }
                        }
                    }
                }
            }
        }

        private static int CountActiveNeighbors4D(List<List<List<List<(bool state, bool change)>>>> cubes, (int x, int y, int z, int w) current)
        {
            const int DoubleIterations = Iterations * 2;
            var active = 0;

            for (var w = -1; w <= 1; w++)
            {
                if (current.w + w < 0 || current.w + w >= DoubleIterations)
                {
                    continue;
                }

                for (var z = -1; z <= 1; z++)
                {
                    if (current.z + z < 0 || current.z + z > DoubleIterations)
                    {
                        continue;
                    }

                    for (var y = -1; y <= 1; y++)
                    {
                        if (current.y + y < 0 || current.y + y >= DoubleIterations + _dimension)
                        {
                            continue;
                        }

                        for (var x = -1; x <= 1; x++)
                        {
                            if (x == 0 && y == 0 && z == 0 && w == 0 || current.x + x < 0 || current.x + x >= DoubleIterations + _dimension)
                            {
                                continue;
                            }

                            //Console.WriteLine($"{current.x} {current.y} {current.z} / {current.x + x}, {current.y + y}, {current.z + z}: {active}");

                            if (cubes[current.w + w][current.z + z][current.y + y][current.x + x].state)
                            {
                                active++;
                                //Console.WriteLine($"{current.x + x}, {current.y + y}, {current.z + z}: {active}");
                            }

                            //Console.WriteLine($"{current.x}, {current.y}, {current.z} , {x}, {y}, {z} , {current.x + x}, {current.y + y}, {current.z + z} , {(cubes[current.z + z][current.y + y][current.x + x].state ? 1 : 0)}");
                        }
                    }
                }
            }

            //Console.WriteLine("");

            return active;
        }

        private static void PartOne(List<List<bool>> input)
        {
            var cubes = Enumerable.Range(1, 1 + Iterations * 2)
                                  .Select(_ => Enumerable.Range(1, _dimension + Iterations * 2)
                                                         .Select(_ => Enumerable.Range(1, _dimension + Iterations * 2)
                                                                                .Select(_ => (state: false, change: false))
                                                                                .ToList())
                                                         .ToList())
                                  .ToList();

            SetInitialState3D(_dimension, cubes, input);

            for (var i = 1; i <= Iterations; i++)
            {
                Console.WriteLine($"Iteration {i}");

                for (var z = Iterations - i; z <= Iterations + i; z++)
                {
                    Console.WriteLine($"z = {z}");

                    for (var y = Iterations - i; y < Iterations + _dimension + i; y++)
                    {
                        for (var x = Iterations - i; x < Iterations + _dimension + i; x++)
                        {
                            var activeNeighbors = CountActiveNeighbors3D(cubes, _dimension, (x, y, z));
                            // Console.WriteLine($"{x}, {y}, {z}: {activeNeighbors} / {cubes[z][y][x]}");

                            //Console.Write($"{(cubes[z][y][x].state ? "1" : "0")} ");
                            Console.Write($"{activeNeighbors} ");

                            if (cubes[z][y][x].state)
                            {
                                if (activeNeighbors != 2 && activeNeighbors != 3)
                                {
                                    cubes[z][y][x] = (cubes[z][y][x].state, true);
                                }
                            }
                            else
                            {
                                if (activeNeighbors == 3)
                                {
                                    cubes[z][y][x] = (cubes[z][y][x].state, true);
                                }
                            }
                        }

                        Console.Write("\n");
                    }

                    Console.Write("\n");
                }

                ApplyState3D(cubes);

                for (var z = Iterations - i; z <= Iterations + i; z++)
                {
                    PrintSlice3D(cubes, z);
                    Console.Write("\n");
                }
            }

            var count = cubes.SelectMany(s => s.SelectMany(l => l)).Sum(c => c.state ? 1 : 0);
            Console.WriteLine($"Sum: {count}");
        }

        private static void SetInitialState3D(int dimension, List<List<List<(bool state, bool change)>>> cubes, List<List<bool>> input)
        {
            for (var y = 0; y < dimension; y++)
            {
                for (var x = 0; x < dimension; x++)
                {
                    cubes[Iterations][Iterations + y][Iterations + x] = (input[y][x], false);
                }
            }

            PrintSlice3D(cubes, Iterations);
        }

        private static void PrintSlice3D(List<List<List<(bool state, bool change)>>> cubes, int slice)
        {
            for (var y = 0; y < cubes[slice].Count; y++)
            {
                for (var x = 0; x < cubes[slice][y].Count; x++)
                {
                    Console.Write($"{(cubes[slice][y][x].state ? 1 : 0)} ");
                }

                Console.Write("\n");
            }
        }

        private static void ApplyState3D(List<List<List<(bool state, bool change)>>> cubes)
        {
            foreach (var lines in cubes.SelectMany(sections => sections))
            {
                for (var x = 0; x < lines.Count; x++)
                {
                    if (lines[x].change)
                    {
                        lines[x] = (!lines[x].state, false);
                    }
                }
            }
        }

        private static int CountActiveNeighbors3D(List<List<List<(bool state, bool change)>>> cubes, int dimension, (int x, int y, int z) current)
        {
            const int DoubleIterations = Iterations * 2;
            var active = 0;

            for (var z = -1; z <= 1; z++)
            {
                if (current.z + z < 0 || current.z + z > DoubleIterations)
                {
                    continue;
                }

                for (var y = -1; y <= 1; y++)
                {
                    if (current.y + y < 0 || current.y + y >= DoubleIterations + dimension)
                    {
                        continue;
                    }

                    for (var x = -1; x <= 1; x++)
                    {
                        if (x == 0 && y == 0 && z == 0 || current.x + x < 0 || current.x + x >= DoubleIterations + dimension)
                        {
                            continue;
                        }

                        //Console.WriteLine($"{current.x} {current.y} {current.z} / {current.x + x}, {current.y + y}, {current.z + z}: {active}");

                        if (cubes[current.z + z][current.y + y][current.x + x].state)
                        {
                            active++;
                            //Console.WriteLine($"{current.x + x}, {current.y + y}, {current.z + z}: {active}");
                        }

                        //Console.WriteLine($"{current.x}, {current.y}, {current.z} , {x}, {y}, {z} , {current.x + x}, {current.y + y}, {current.z + z} , {(cubes[current.z + z][current.y + y][current.x + x].state ? 1 : 0)}");
                    }
                }
            }

            //Console.WriteLine("");

            return active;
        }
    }
}