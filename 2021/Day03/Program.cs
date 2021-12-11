namespace Day03;

public class BinarySum
{
    public int Zero { get; set; }
    public int One { get; set; }
}

public static class Program
{
    public static void Main()
    {
        var input = File.ReadAllLines("input.txt");
        var outputWidth = input.First().Length;

        PartOne(input, outputWidth);
        PartTwo(input, outputWidth);
    }

    private static void PartOne(string[] input, int outputWidth)
    {
        var sums = new List<BinarySum>();
        for (var d = 0; d < outputWidth; ++d)
        {
            sums.Add(new BinarySum
                     {
                         Zero = 0,
                         One = 0
                     });
        }

        foreach (var line in input)
        {
            for (var d = 0; d < line.Length; ++d)
            {
                switch (line[d])
                {
                    case '0':
                        sums[d].Zero += 1;
                        break;
                    case '1':
                        sums[d].One += 1;
                        break;
                }
            }
        }

        var result = sums.Aggregate(string.Empty, (current, bs) => current + (bs.Zero > bs.One ? "0" : "1"));

        var gamma = Convert.ToInt32(result, 2);
        var epsilon = Convert.ToInt32(result.Replace("0", "X")
                                            .Replace("1", "0")
                                            .Replace("X", "1"), 2);

        Console.WriteLine($"PartOne: {gamma * epsilon}");
    }

    private static void PartTwo(string[] input, int outputWidth)
    {
        var oxygenRatingSelection = input;

        for (var i = 0; oxygenRatingSelection.Length > 1 && i < outputWidth; i++)
        {
            var oxygenCriteria = GetOxygenCriteria(oxygenRatingSelection, i);
            oxygenRatingSelection = oxygenRatingSelection.Where(l => l[i] == oxygenCriteria)
                                                         .ToArray();
        }

        var oxygenRating = oxygenRatingSelection.Single();

        var carbonRatingSelection = input;

        for (var i = 0; carbonRatingSelection.Length > 1 && i < outputWidth; i++)
        {
            var carbonCriteria = GetCarbonCriteria(carbonRatingSelection, i);
            carbonRatingSelection = carbonRatingSelection.Where(l => l[i] == carbonCriteria)
                                                         .ToArray();
        }

        var carbonRating = carbonRatingSelection.Single();

        Console.WriteLine($"PartTwo: {Convert.ToInt32(oxygenRating, 2) * Convert.ToInt32(carbonRating, 2)}");
    }

    private static char GetOxygenCriteria(string[] oxygenRatingSelection, int position)
    {
        return oxygenRatingSelection.Count(l => l[position] == '0') > oxygenRatingSelection.Length / 2 ? '0' : '1';
    }

    private static char GetCarbonCriteria(string[] carbonRatingSelection, int position)
    {
        return carbonRatingSelection.Count(l => l[position] == '0') <= carbonRatingSelection.Length / 2 ? '0' : '1';
    }
}