string inputFilePath = "./input.txt";
string[] inputLines = File.ReadAllLines(inputFilePath);

string[] expandedUniverse = ExpandUniverse(inputLines);
expandedUniverse = PivotStrings(ExpandUniverse(PivotStrings(expandedUniverse)));
int[,] universe = PopulateUniverse(expandedUniverse, out int maxGalaxyId);


int totalDistance = 0;
for (int i = 1; i <= maxGalaxyId; i++)
{
    for (int j = i + 1; j <= maxGalaxyId; j++)
    {
        var galaxy1 = GetGalaxyCoordinate(universe, i);
        var galaxy2 = GetGalaxyCoordinate(universe, j);
        int distance = Math.Abs(galaxy2.y - galaxy1.y) + Math.Abs(galaxy2.x - galaxy1.x);
        // Console.WriteLine($"Galaxy {i} to Galaxy {j} distance is: {distance}");

        totalDistance += distance;

    }
}







PrintMatrix(universe, 2);
Console.WriteLine($"Total Distance: {totalDistance}");
Console.WriteLine("End of Program");

static string[] ExpandUniverse(string[] universe)
{
    int newLength = universe.Length
        + universe.Where(l => l.Distinct().Count() == 1).Count();

    string[] expandedUniverse = new string[newLength];

    int i = 0;
    foreach (string line in universe)
    {
        expandedUniverse[i] = line;
        // Console.WriteLine(expandedUniverse[i]);
        i++;


        if (line.Distinct().Count() == 1)
        {
            expandedUniverse[i] = new string('.', line.Length);
            // Console.WriteLine(expandedUniverse[i]);
            i++;
        }
    }


    return expandedUniverse;

}

static int[,] PopulateUniverse(string[] data, out int maxGalaxyId)
{
    int[,] universe = new int[data.Max(d => d.Length), data.Length];
    int galaxyId = 1;

    for (int y = 0; y < data.Length; y++)
    {
        for (int x = 0; x < data[y].Length; x++)
        {
            if (data[y][x] == '.')
            {
                universe[x, y] = 0;
                continue;
            }

            universe[x, y] = galaxyId++;
        }
    }

    maxGalaxyId = galaxyId = galaxyId - 1;
    return universe;
}

static (int x, int y) GetGalaxyCoordinate(int[,] universe, int galaxyId)
{
    for (int x = 0; x < universe.GetLength(0); x++)
        for (int y = 0; y < universe.GetLength(1); y++)
            if (universe[x, y] == galaxyId) return (x, y);

    throw new Exception("Galaxy not found");
}

static string[] PivotStrings(string[] source)
{
    string[] pivot = new string[source.Max(l => l.Length)];

    for (int i = 0; i < pivot.Length; i++)
    {
        for (int j = 0; j < source.Length; j++) pivot[i] += source[j][i];
    }

    return pivot;
}

static void PrintMatrix<T>(T[,] distanceMatrix, int padding = 0)
{
    Console.WriteLine("┌─────");

    for (int y = 0; y < distanceMatrix.GetLength(1); y++)
    {
        for (int x = 0; x < distanceMatrix.GetLength(0); x++)
        {
            Console.Write(distanceMatrix[x, y].ToString().PadLeft(padding));
        }
        Console.WriteLine();
    }
}
