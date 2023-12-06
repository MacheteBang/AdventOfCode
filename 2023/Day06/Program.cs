string inputFilePath = "./input.txt";
string[] inputLines = File.ReadAllLines(inputFilePath);

List<Race> races = ConvertRaces(inputLines);

UInt64 marginOfError = 1;
foreach (Race race in races)
{
    UInt64[] waysToWin = GetWaysToWin(race);
    marginOfError *= Convert.ToUInt64(waysToWin.Length);
}

Console.WriteLine($"Margin of Error: {marginOfError}");
Console.WriteLine("End of Program");





List<Race> ConvertRaces(string[] raceLines)
{
    UInt64[] times = [UInt64.Parse(inputLines[0].Split(':')[1].Trim().Replace(" ", ""))];
    UInt64[] distances = [UInt64.Parse(inputLines[1].Split(':')[1].Trim().Replace(" ", ""))];

    List<Race> races = [];

    for (int i = 0; i < times.Length; i++)
    {
        races.Add(new Race { Time = times[i], Distance = distances[i] });
    }

    return races;
}

UInt64[] GetWaysToWin(Race race)
{
    List<UInt64> winningButtonPresses = [];

    for (UInt64 buttonPress = 0; buttonPress <= race.Time; buttonPress++)
    {
        var distance = (race.Time - buttonPress) * buttonPress;
        if (distance > race.Distance) winningButtonPresses.Add(buttonPress);
    }

    return winningButtonPresses.ToArray();
}