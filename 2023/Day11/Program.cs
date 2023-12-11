string inputFilePath = "./input.txt";
string[] inputLines = File.ReadAllLines(inputFilePath);

List<int> expandedRows = GetExpandedRowCol(inputLines);
List<int> expandedCols = GetExpandedRowCol(PivotStrings(inputLines));

int[,] universe = PopulateUniverse(inputLines, out int maxGalaxyId);
// PrintMatrix(universe);

int expansionAmount = 1000000;
long totalDistance = 0;
for (int i = 1; i <= maxGalaxyId; i++)
{
    for (int j = i + 1; j <= maxGalaxyId; j++)
    {
        var galaxy1 = GetGalaxyCoordinate(universe, i);
        var galaxy2 = GetGalaxyCoordinate(universe, j);
        int distance = Math.Abs(galaxy2.y - galaxy1.y) + Math.Abs(galaxy2.x - galaxy1.x);
        for (int y = Math.Min(galaxy1.y, galaxy2.y) + 1; y < Math.Max(galaxy1.y, galaxy2.y); y++)
        {
            if (expandedRows.Contains(y)) distance += Math.Max(expansionAmount - 1, 1);
        }
        for (int x = Math.Min(galaxy1.x, galaxy2.x) + 1; x < Math.Max(galaxy1.x, galaxy2.x); x++)
        {
            if (expandedCols.Contains(x)) distance += Math.Max(expansionAmount - 1, 1);
        }
        totalDistance += distance;
    }
}







// PrintMatrix(universe, 2);
Console.WriteLine($"Total Distance: {totalDistance}");
Console.WriteLine("End of Program");

static List<int> GetExpandedRowCol(string[] data)
{
    List<int> expandedRowCol = [];

    for (int i = 0; i < data.Length; i++)
    {
        if (data[i].First() == '.' && data[i].Distinct().Count() == 1)
        {
            expandedRowCol.Add(i);
        }
    }

    return expandedRowCol;
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
