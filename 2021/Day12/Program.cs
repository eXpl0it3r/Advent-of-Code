namespace Day12;

public class Node
{
    public string Name { get; set; } = string.Empty;
    public List<Node> Connections { get; } = new();
    public bool IsLarge => char.IsUpper(Name.First());
}

public static class Program
{
    private static readonly Dictionary<string, Node> Nodes = new();
    private static readonly List<string> Solutions = new();

    public static void Main()
    {
        var input = File.ReadAllLines("input.txt")
                        .Where(l => l.Length > 0)
                        .Select(l => l.Split('-'));

        foreach (var edge in input)
        {
            if (!Nodes.ContainsKey(edge[0]))
            {
                Nodes.Add(edge[0], new Node
                                   {
                                       Name = edge[0]
                                   });
            }

            if (!Nodes.ContainsKey(edge[1]))
            {
                Nodes.Add(edge[1], new Node
                                   {
                                       Name = edge[1]
                                   });
            }

            Nodes[edge[0]].Connections.Add(Nodes[edge[1]]);
            Nodes[edge[1]].Connections.Add(Nodes[edge[0]]);
        }

        foreach (var node in Nodes)
        {
            Console.WriteLine(node.Key);
        }

        PrintAllPaths(true);
        var partOne = Solutions.Count;
        Console.WriteLine($"PartOne: {partOne}");

        Solutions.Clear();
        PrintAllPaths(false);
        var partTwo = Solutions.Count;
        Console.WriteLine($"PartOne: {partTwo}");
    }

    private static void PrintAllPaths(bool visitedSmallCave)
    {
        var isVisited = Nodes.Select(kvp => kvp.Key)
                             .ToDictionary(n => n, _ => 0);
        var path = new Stack<string>();
        path.Push("start");
        PrintAllPathsUtil("start", isVisited, visitedSmallCave, path);
    }

    private static void PrintAllPathsUtil(string current, Dictionary<string, int> isVisited, bool visitedSmallCave, Stack<string> localPath)
    {
        if (current == "end")
        {
            Solutions.Add(string.Join(',', localPath));
            return;
        }

        if (!Nodes[current].IsLarge)
        {
            if (current is "start" or "end")
            {
                isVisited[current] = 1;
            }
            else if (!visitedSmallCave && isVisited[current] == 1)
            {
                visitedSmallCave = true;
                isVisited[current] = 2;
            }
            else
            {
                isVisited[current] = 1;
            }
        }

        foreach (var connection in Nodes[current].Connections
                                                 .Where(connection => isVisited[connection.Name] == 0
                                                                                  || isVisited[connection.Name] == 1 && connection.Name is not "start" and not "end" && !visitedSmallCave))
        {
            localPath.Push(connection.Name);
            PrintAllPathsUtil(connection.Name, isVisited, visitedSmallCave, localPath);
            localPath.Pop();
        }

        if (visitedSmallCave && !Nodes[current].IsLarge && isVisited[current] == 2)
        {
            isVisited[current] = 1;
        }
        else
        {
            isVisited[current] = 0;
        }
    }
}