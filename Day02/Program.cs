string filePath = "input.txt";
string[] lines = File.ReadAllLines(filePath);

int possibleIds = 0;
int totalPower = 0;

foreach (string line in lines)
{

    var gameEvent = Game.ConvertToGame(line);
    if (gameEvent.IsPossible(12, 13, 14))
    {
        possibleIds += gameEvent.GameId;
    }

    totalPower += gameEvent.GetPower();
}

Console.WriteLine($"Sum of Ids: {possibleIds}");
Console.WriteLine($"Sum of Total Power: {totalPower}");