string[] lines = File.ReadAllLines("input.example.txt");

int segments = 10;

CoordinateSet[] snake = new CoordinateSet[segments];
snake[0] = new CoordinateSet(0, 0);

double maxDistance = Math.Sqrt(2);

List<CoordinateSet> tailHistory = new() { new CoordinateSet(0, 0) };

foreach (string line in lines)
{
    string headDirection = line.Split(" ")[0];
    int headDistance = Convert.ToInt32(line.Split(" ")[1]);

    for (int i = 1; i <= headDistance; i++)
    {
        CoordinateSet previousSegment;
        previousSegment = snake[0].Clone();
        MoveSegment(ref snake[0], headDirection);

        for (int j = 1; j <= segments - 1; j++)
        {
            snake[j] = snake[j] ?? new CoordinateSet(0, 0);

            if (snake[j].DistanceTo(snake[j - 1]) > maxDistance)
            {
                CoordinateSet tmp = snake[j].Clone();
                snake[j] = previousSegment;
                previousSegment = tmp;
            }
        }

        if (tailHistory.FirstOrDefault(t => t.X == snake[segments - 1].X && t.Y == snake[segments-1].Y) is null)
            tailHistory.Add(snake[segments - 1].Clone());


        PrintMap(snake);
        var a = 1;
    }
}

Console.WriteLine(tailHistory.Count());
Console.WriteLine("Done!");



void MoveSegment(ref CoordinateSet segment, string direction)
{
    switch (direction)
    {
        case "U":
            segment.Y--;
            break;
        case "D":
            segment.Y++;
            break;
        case "R":
            segment.X++;
            break;
        case "L":
            segment.X--;
            break;
        default:
            break;
    }
}






void PrintMap(CoordinateSet[] snake)
{
    string map = "";

    int gridDimension = 10;
    Console.Clear();
    for (int y = -gridDimension; y <= gridDimension; y++)
    {
        for (int x = -gridDimension; x <= gridDimension; x++)
        {
            CoordinateSet? j = snake.FirstOrDefault(s => x == s.X && y == s.Y);

            if (j is not null)
            {
                map += Array.IndexOf(snake, j);
                continue;
            }

            map += ".";

        }
        map += Environment.NewLine;
    }

    Console.WriteLine(map);
}