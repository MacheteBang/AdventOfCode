string inputFile = "./input.txt";
string[] inputLines = File.ReadAllLines(inputFile);

string instructions = inputLines[0];
Dictionary<string, DestinationSet> map = LoadMap(inputLines[2..]);
int stepsToNavigate = NavigateToEnd(map, "AAA", "ZZZ", instructions);


Console.WriteLine($"Steps to end: {stepsToNavigate}");
Console.WriteLine("End of Program");






Dictionary<string, DestinationSet> LoadMap(string[] lines)
{
    Dictionary<string, DestinationSet> returnMap = [];

    foreach (string line in lines)
    {
        string[] splitLine = line.Split('=');
        string[] splitDestinations = splitLine[1].Split(',');

        returnMap.Add(
            splitLine[0].Trim(),
            new DestinationSet
            {
                Left = splitDestinations[0].Replace("(", "").Trim(),
                Right = splitDestinations[1].Replace(")", "").Trim()
            }
        );
    }

    return returnMap;
}

int NavigateToEnd(Dictionary<string, DestinationSet> map, string start, string end, string instructions)
{
    int steps = 0;

    string currentLocation = start;
    DestinationSet currentDestinationSet = map[currentLocation];
    int stepPosition = 0;
    while (currentLocation != end)
    {
        if (stepPosition == instructions.Length) stepPosition = 0;

        char thisInstruction = instructions[stepPosition];
        stepPosition++;

        currentLocation = thisInstruction == 'L' ? currentDestinationSet.Left : currentDestinationSet.Right;
        currentDestinationSet = map[currentLocation];
        steps++;
    }

    return steps;
}