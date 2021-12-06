param([string] $year = "2021", [Parameter(Mandatory)] [string] $day)

# Create new project & add it to the solution
dotnet new console -lang "C#" -n "Day$day" -o "$year/Day$day/" -f net6.0 --langVersion 10.0
dotnet sln add "$year/Day$day"

# Better default code
$program = 'using System.IO;

var input = File.ReadAllLines("input.txt");'

Set-Content -Path "$year/Day$day/Program.cs" -Value $program

# Webrequest for the input - requires authentication
#$numberDay = [int]$day
#Invoke-WebRequest -Uri "https://adventofcode.com/$year/day/$numberDay/input" -OutFile "$year/Day$day/input.txt"

# Modify project to always copy the input.txt file to the output directory
$proj = [xml](Get-Content "$year/Day$day/Day$day.csproj")

$itemGroup = $proj.CreateElement("ItemGroup");
$proj.Project.AppendChild($itemGroup);

$noneNode = $proj.CreateElement("None");
$noneNode.SetAttribute("Update", "input.txt");
$itemGroup.AppendChild($noneNode)

$copyNode = $proj.CreateElement("CopyToOutputDirectory");
$noneNode.AppendChild($copyNode)

$copyNode.InnerText = "Always"

$currentPath = Get-Location

$proj.Save("$currentPath/$year/Day$day/Day$day.csproj")