using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("input.txt")
                .First();

var partOne = input.Split(',')
                   .Select(int.Parse)
                   .ToList();

for (var day = 0; day < 80; day++)
{
    var today = partOne.Count;

    for (var fish = 0; fish < today; fish++)
    {
        if (partOne[fish] == 0)
        {
            partOne[fish] = 6;
            partOne.Add(8);
        }
        else
        {
            partOne[fish] -= 1;
        }
    }
}

Console.WriteLine($"PartOne: {partOne.Count}");

var partTwo = input.Split(',')
                   .Select(int.Parse)
                   .ToList();

var schoolOfFish = new List<long>
                   {
                       0, 0, 0, 0, 0, 0, 0, 0, 0
                   };

foreach (var fish in partTwo)
{
    schoolOfFish[fish]++;
}

for (var day = 0; day < 256; day++)
{
    var first = schoolOfFish[0];
    schoolOfFish.RemoveAt(0);
    schoolOfFish[6] += first;
    schoolOfFish.Add(first);
}

Console.Write($"PartTwo: {schoolOfFish.Sum()}");