string inputFilePath = "./input.txt";
string[] inputLines = File.ReadAllLines(inputFilePath);

char[,] mapMatrix = LoadMatrix(inputLines, out (int x, int y) startingPoint);
int[,] distanceMatrix = new int[mapMatrix.GetLength(0), mapMatrix.GetLength(1)];

Queue<(int x, int y)> paths = new();
paths.Enqueue(startingPoint);

while (paths.Count > 0)
{
    (int x, int y) thisPoint = paths.Dequeue();
    List<(int x, int y)> exits = GetExits(mapMatrix, thisPoint);
    foreach (var exit in exits)
    {
        if (distanceMatrix[exit.x, exit.y] == 0)
        {
            paths.Enqueue(exit);
            distanceMatrix[exit.x, exit.y] = distanceMatrix[thisPoint.x, thisPoint.y] + 1;
        }
    }
}



PrintMatrix(distanceMatrix, 2);
Console.WriteLine($"Max distance is {GetMax(distanceMatrix)}");
Console.WriteLine("End of Program");




static char[,] LoadMatrix(string[] rawData, out (int x, int y) startingPoint)
{
    (int x, int y) start = (0, 0);

    int width = rawData.Max(l => l.Length);
    int height = rawData.Length;
    char[,] matrix = new char[width, height];

    int y = 0;
    foreach (string line in rawData)
    {
        int x = 0;
        foreach (char letter in line)
        {
            matrix[x, y] = letter;

            if (letter == 'S') start = (x, y);

            x++;
        }
        y++;
    }

    startingPoint = start;
    return matrix;
}

static List<(int x, int y)> GetExits(char[,] matrix, (int x, int y) currentLocation)
{
    List<(int x, int y)> exits = [];

    (int x, int y) up = (-1, -1);
    if ("S|JL".Contains(matrix[currentLocation.x, currentLocation.y])) up = (currentLocation.x, currentLocation.y - 1);

    (int x, int y) down = (-1, -1);
    if ("S|7F".Contains(matrix[currentLocation.x, currentLocation.y])) down = (currentLocation.x, currentLocation.y + 1);

    (int x, int y) left = (-1, -1);
    if ("S-7J".Contains(matrix[currentLocation.x, currentLocation.y])) left = (currentLocation.x - 1, currentLocation.y);

    (int x, int y) right = (-1, -1);
    if ("S-FL".Contains(matrix[currentLocation.x, currentLocation.y])) right = (currentLocation.x + 1, currentLocation.y);

    if (IsInBounds(up, matrix) && "|7F".Contains(matrix[up.x, up.y])) exits.Add(up);
    if (IsInBounds(down, matrix) && "|JL".Contains(matrix[down.x, down.y])) exits.Add(down);
    if (IsInBounds(left, matrix) && "-FL".Contains(matrix[left.x, left.y])) exits.Add(left);
    if (IsInBounds(right, matrix) && "-7J".Contains(matrix[right.x, right.y])) exits.Add(right);

    return exits;
}

static int GetMax(int[,] distanceMatrix)
{
    int returnValue = 0;

    for (int i = 0; i < distanceMatrix.GetLength(0); i++)
    {
        for (int j = 0; j < distanceMatrix.GetLength(1); j++)
        {
            returnValue = Math.Max(distanceMatrix[i, j], returnValue);
        }
    }

    return returnValue;
}

static bool IsInBounds<T>((int x, int y) point, T[,] matrix)
{
    return point.x >= 0
        && point.y >= 0
        && point.x < matrix.GetLength(0)
        && point.y < matrix.GetLength(1);
}

static void PrintMatrix<T>(T[,] distanceMatrix, int padding = 0)
{
    Console.WriteLine("┌─────");

    for (int i = 0; i < distanceMatrix.GetLength(0); i++)
    {
        for (int j = 0; j < distanceMatrix.GetLength(1); j++)
        {
            Console.Write(distanceMatrix[j, i].ToString().PadLeft(padding));
        }
        Console.WriteLine();
    }
}