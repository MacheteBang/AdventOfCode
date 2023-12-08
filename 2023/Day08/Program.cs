string inputFile = "./input.txt";
string[] inputLines = File.ReadAllLines(inputFile);

string instructions = inputLines[0];
Dictionary<string, DestinationSet> map = LoadMap(inputLines[2..]);
List<int> primes = [];

var startingPoints = map.Where(m => m.Key.EndsWith("A")).Select(m => m.Key);
foreach (var startingPoint in startingPoints)
{
    int steps = NavigateToEnd(map, startingPoint, "Z", instructions);
    primes.AddRange(GetPrimeFactors(steps));
}

ulong minSteps = primes.Distinct().Aggregate(Convert.ToUInt64(1), (acc, p) => Convert.ToUInt64(p) * acc);

Console.WriteLine($"Steps to end: {minSteps}");
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
    while (!currentLocation.EndsWith(end))
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

int[] GetPrimeFactors(int n)
{
    List<int> results = [];

    while (n % 2 == 0)
    {
        results.Add(2);
        n /= 2;
    }

    for (int i = 3; i <= Math.Sqrt(n); i += 2)
    {
        while (n % i == 0)
        {
            results.Add(i);
            n /= i;
        }
    }

    if (n > 2) results.Add(n);

    return results.ToArray();
}