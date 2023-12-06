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

Parallel.ForEach(Enumerable.Range(0, seeds.Length / 2).Select(i => i * 2),
    i =>
    {
        Console.WriteLine($"Starting Seed Set {seeds[i]}");
        Parallel.For(seeds[i], seeds[i] + seeds[i + 1],
        j =>
        {
            var id = GetId(GardenComponent.Seed, j, GardenComponent.Location);
            minLocation = Math.Min(id, minLocation);

            if (j % 100000 == 0) Console.WriteLine($"Seed Set {seeds[i]}: {Math.Round(100 * Convert.ToDouble(j - seeds[i]) / Convert.ToDouble(seeds[i + 1]))}");
        });
        Console.WriteLine($"Finished Seed Set {seeds[i]}");
        GC.Collect();
    }
);

// for (int i = 0; i < seeds.Length; i += 2)
// {
//     Parallel.For(seeds[i], seeds[i] + seeds[i + 1],
//         j =>
//         {
//             var id = GetId(GardenComponent.Seed, j, GardenComponent.Location);
//             minLocation = Math.Min(id, minLocation);

//         }
//     );

//     for (long j = seeds[i]; j < seeds[i] + seeds[i + 1]; j++)
//     {
//         var id = GetId(GardenComponent.Seed, j, GardenComponent.Location);
//         minLocation = Math.Min(id, minLocation);
//         Console.WriteLine($"Seed {j}: Location {id}");
//     }
// }

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
