string filePath = "input.txt";
string[] lines = File.ReadAllLines(filePath);

Dictionary<int, Game> games = new();

int possibleIds = 0;


foreach (string line in lines)
{

    var gameEvent = Game.ConvertToGame(line);
    if (gameEvent.IsPossible(12, 13, 14))
    {
        possibleIds += gameEvent.GameId;
    }
}

Console.WriteLine($"Sum of Ids: {possibleIds}");