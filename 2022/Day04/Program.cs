List<int> ConvertRange(string range)
{
    var r = range.Split('-');
    var start = Convert.ToInt32(r[0]);
    var end = Convert.ToInt32(r[1]);

    return Enumerable.Range(start, end - start + 1)
                     .ToList();
}

bool Contain(IReadOnlyCollection<int> rangeA, IReadOnlyCollection<int> rangeB)
{
    var intersectionSize = rangeA.Intersect(rangeB).Count();

    return intersectionSize == rangeA.Count || intersectionSize == rangeB.Count;
}

var input = File.ReadAllLines("input.txt")
                .Select(p => p.Split(','))
                .Select(p => new Tuple<List<int>, List<int>>(ConvertRange(p[0]), ConvertRange(p[1])))
                .ToList();

var overlapCount = input.Select(r => Contain(r.Item1, r.Item2))
                        .Count(x => x);

Console.WriteLine($"PartOne: {overlapCount}");

var overlappingPairs = input.Count(r => r.Item1.Intersect(r.Item2).Any());

Console.WriteLine($"PartTwo: {overlappingPairs}");