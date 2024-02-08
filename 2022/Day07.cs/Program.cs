string[] lines = File.ReadAllLines("input.txt");

string currentDirectoryInHierarchy = "";
List<FileItem> files = new();
List<string> directories = new();

for (int i = 0; i < lines.Length; i++)
{
    if (lines[i] == "$ ls") continue;
    if (lines[i].StartsWith("dir")) continue;

    if (lines[i] == "$ cd ..")
    {
        currentDirectoryInHierarchy = currentDirectoryInHierarchy.Substring(0, currentDirectoryInHierarchy.LastIndexOf("/"));
        if (!directories.Contains(currentDirectoryInHierarchy)) directories.Add(currentDirectoryInHierarchy);
        continue;
    }

    var lineData = lines[i].Split(" ");

    if (lines[i].StartsWith("$ cd"))
    {
        currentDirectoryInHierarchy += "/" + lineData[2].Replace("/", ".");
        if (!directories.Contains(currentDirectoryInHierarchy)) directories.Add(currentDirectoryInHierarchy);
        continue;
    }

    files.Add(new()
    {
        ParentHierarchy = currentDirectoryInHierarchy,
        Name = lineData[1],
        Size = Convert.ToInt32(lineData[0])
    });
}

var directorySizes = directories
    .Select(d => new { Directory = d, Size = files.Where(f => f.ParentHierarchy.StartsWith(d)).Sum(f => f.Size) });

Console.WriteLine(directorySizes.Where(d => d.Size <= 100000).Sum(d => d.Size));
// 847705 --> NO

/////

int total = 70000000;
int required = 30000000;
int currentUsedSpace = directorySizes.First(d => d.Directory == "/.").Size;
int currentFreeSpace = total - currentUsedSpace;
int neededToDelete = required - currentFreeSpace;

var x = directorySizes.OrderBy(d => d.Size).First(d => d.Size > neededToDelete);
Console.WriteLine(x.Size);

Console.WriteLine();
