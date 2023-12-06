string inputFile = "./input.txt";
string[] lines = File.ReadAllLines(inputFile);

bool[,] lightArray = new bool[1000, 1000];

foreach (string line in lines)
{
    string[] sides = line.Split(" through ");
    Coordinate end = new() { X = int.Parse(sides[1].Split(',')[0]), Y = int.Parse(sides[1].Split(',')[1]) };

    string action = string.Empty;
    action = sides[0].Contains("on") ? "On" : (sides[0].Contains("off") ? "Off" : "Toggle");
    Coordinate start = new() { X = int.Parse(sides[0].Split(" ").Last().Split(',')[0]), Y = int.Parse(sides[0].Split(" ").Last().Split(',')[1]) };

    UpdateGrid(ref lightArray, action, start, end);
}

int countOfLightsOn = lightArray.Cast<bool>().Count(l => l);

Console.WriteLine($"Lights On: {countOfLightsOn}");
Console.WriteLine("End of Program");


void UpdateGrid(ref bool[,] grid, string action, Coordinate start, Coordinate end)
{
    for (int x = start.X; x <= end.X; x++)
    {
        for (int y = start.Y; y <= end.Y; y++)
        {
            grid[x, y] = action == "On" ? true : (action == "Off" ? false : !grid[x, y]);
        }
    }

}