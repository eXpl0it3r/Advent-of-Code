var inputs = File.ReadLines("input.txt")
                 .Select(l =>
                         {
                             var instruction = l.Split(' ');
                             return new
                                    {
                                        Instruction = instruction[0],
                                        Amount = int.Parse(instruction[1])
                                    };
                         })
                 .ToList();

var depth = inputs.Where(i => i.Instruction == "up")
                  .Select(i => -i.Amount)
                  .Concat(inputs.Where(i => i.Instruction == "down")
                                .Select(i => i.Amount))
                  .Sum();
var forward = inputs.Where(i => i.Instruction == "forward")
                    .Select(i => i.Amount)
                    .Sum();
Console.WriteLine($"PartOne: {depth * forward}");

var aim = 0;
var y = 0;
var x = 0;

foreach (var instruction in inputs)
{
    aim += instruction.Instruction switch
           {
               "down" => instruction.Amount,
               "up" => -instruction.Amount,
               _ => 0
           };

    if (instruction.Instruction == "forward")
    {
        x += instruction.Amount;
        y += aim * instruction.Amount;
    }
}

Console.WriteLine($"PartTwo: {x * y}");