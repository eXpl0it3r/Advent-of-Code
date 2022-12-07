var input = File.ReadAllLines("input.txt");

var tree = new FileSystem("/");
var currentDirectory = tree.Root;

var listMode = false;

foreach (var line in input)
{
    if (line.StartsWith("$"))
    {
        if (listMode)
        {
            listMode = false;
        }

        if (line[2..].StartsWith("cd"))
        {
            var directory = line[5..];

            if (directory == "/")
            {
                currentDirectory = tree.Root;
                continue;
            }

            if (directory == "..")
            {
                currentDirectory = currentDirectory.Parent;
                continue;
            }

            currentDirectory = currentDirectory.FindOrAddDirectory(directory);
            continue;
        }

        if (line[2..] == "ls")
        {
            listMode = true;
            continue;
        }
    }

    if (line.StartsWith("dir"))
    {
        currentDirectory.FindOrAddDirectory(line[4..]);
        continue;
    }

    var file = line.Split(' ');
    currentDirectory.FindOrAddFile(file[1], ulong.Parse(file[0]));
}

PrintTree(tree.Root, 0);

var allSizes = new List<ulong>();
var totalSize = MeasureDirectorySize(tree.Root, allSizes);

var partOne = allSizes.Where(s => s <= 100000).Aggregate(0ul, (current, size) => current + size);

Console.WriteLine($"PartOne: {partOne}");

allSizes = allSizes.Order()
                   .ToList();
var partTwo = allSizes.First(s => s >= 30000000 - (70000000 - totalSize));

Console.WriteLine($"PartTwo: {partTwo}");

ulong MeasureDirectorySize(Directory node, ICollection<ulong> collect)
{
    var size = node.Children.Aggregate(0ul, (current, child) => current + MeasureDirectorySize(child, collect));
    size = node.Files.Aggregate(size, (current, file) => current + file.Size);

    collect.Add(size);

    return size;
}

void PrintTree(Directory node, int depth)
{
    Console.WriteLine($"{string.Concat(Enumerable.Repeat(" ", depth))}{node.Name}");

    foreach (var child in node.Children)
    {
        PrintTree(child, depth + 1);
    }

    foreach (var file in node.Files)
    {
        Console.WriteLine($"{string.Concat(Enumerable.Repeat(" ", depth))}{file.Name} {file.Size}");
    }
}

internal class Directory
{
    public string Name { get; init; }
    public Directory? Parent { get; init; }
    public List<Directory> Children { get; } = new();
    public List<FileInfo> Files { get; } = new();

    public Directory FindOrAddDirectory(string data)
    {
        var child = Children.Find(c => c.Name == data);

        if (child != null)
        {
            return child;
        }

        child = new Directory
                {
                    Name = data,
                    Parent = this
                };
        Children.Add(child);
        return child;
    }

    public FileInfo FindOrAddFile(string name, ulong size)
    {
        var file = Files.Find(f => f.Name == name);

        if (file != null)
        {
            return file;
        }

        file = new FileInfo
               {
                   Name = name,
                   Size = size
               };
        Files.Add(file);
        return file;
    }
}

internal class FileInfo
{
    public string Name { get; init; }
    public ulong Size { get; init; }
}

internal class FileSystem
{
    public FileSystem(string data)
    {
        Root = new Directory
               {
                   Name = data,
                   Parent = null
               };
    }

    public Directory Root { get; }
}