using System.Diagnostics;

string inpputFilePath = "./input.txt";
string[] inputLines = File.ReadAllLines(inpputFilePath);


long[] seeds = [];
GardenMap currentMap = default;
List<GardenMap> maps = [];

for (int i = 0; i < inputLines.Length; i++)
{
    if (string.IsNullOrWhiteSpace(inputLines[i])) continue;

    if (inputLines[i].StartsWith("seeds:"))
    {
        seeds = inputLines[i]
            .Split(' ')
            .Skip(1)
            .Select(long.Parse)
            .ToArray();

        continue;
    }

    if (inputLines[i].Contains("map"))
    {
        if (currentMap is not null) maps.Add(currentMap);
        currentMap = new GardenMap(inputLines[i]);
        continue;
    }

    currentMap.Maps.Add(new MapSet(
        long.Parse(inputLines[i].Split(' ')[0]),
        long.Parse(inputLines[i].Split(' ')[1]),
        long.Parse(inputLines[i].Split(' ')[2])
    ));
}
if (currentMap is not null) maps.Add(currentMap);

long minLocation = long.MaxValue;
var timer = Stopwatch.StartNew();
foreach (long seed in seeds)
{
    var id = GetId(GardenComponent.Seed, seed, GardenComponent.Location);
    minLocation = Math.Min(id, minLocation);
    Console.WriteLine($"Seed {seed}: Location {id}");
}
timer.Stop();
Console.WriteLine($"The lowest location is {minLocation}");
Console.WriteLine($"Process time was {timer.Elapsed}");

Console.WriteLine("End of Program");



long GetId(GardenComponent startingComponent, long componentId, GardenComponent destinationComponent)
{
    var map = maps.Where(m => m.SourceComponent == startingComponent).First();
    var mapSet = map.Maps
        .Where(m => componentId >= m.SourceStart && componentId < m.SourceStart + m.Length)
        .FirstOrDefault();

    long destinationId;
    if (mapSet.Length == 0)
    {
        destinationId = componentId;
    }
    else
    {
        destinationId = componentId - mapSet.SourceStart + mapSet.DestinationStart;
    }

    if (destinationComponent == map.DestinationComponent)
    {
        return destinationId;
    }

    return GetId(map.DestinationComponent, destinationId, destinationComponent);
}
