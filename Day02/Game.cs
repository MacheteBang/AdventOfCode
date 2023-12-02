public class Game
{
    public int GameId { get; init; }
    public int Red { get; private set; }
    public int Green { get; private set; }
    public int Blue { get; private set; }

    private List<Game> _gameEvents = new();

    private void AddEvent(Game gameEvent)
    {
        _gameEvents.Add(gameEvent);
        Red = gameEvent.Red > Red ? gameEvent.Red : Red;
        Green = gameEvent.Green > Green ? gameEvent.Green : Green;
        Blue = gameEvent.Blue > Blue ? gameEvent.Blue : Blue;
    }

    public bool IsPossible(int red, int green, int blue)
    {
        return Red <= red
            && Green <= green
            && Blue <= blue;
    }

    public static Game ConvertToGame(string data)
    {
        if (string.IsNullOrEmpty(data)) throw new ArgumentNullException(nameof(data));
        if (!data.Contains(':')) throw new ArgumentException("Missing ':' delimiter");
        if (!data.Contains(';')) throw new ArgumentException("Missing ';' delimiter");

        string gameInfo = data.Split(":")[0];
        string[] gameData = data.Split(":")[1].Split(";");

        Game returnGame = new Game { GameId = gameInfo.ToIntValue() };

        foreach (string eventString in gameData)
        {

            string[] cubePicks = eventString.Split(',');
            int red = cubePicks.FirstOrDefault(d => d.Contains("red", StringComparison.OrdinalIgnoreCase)).ToIntValue();
            int green = cubePicks.FirstOrDefault(d => d.Contains("green", StringComparison.OrdinalIgnoreCase)).ToIntValue();
            int blue = cubePicks.FirstOrDefault(d => d.Contains("blue", StringComparison.OrdinalIgnoreCase)).ToIntValue();

            returnGame.AddEvent(new()
            {
                GameId = gameInfo.ToIntValue(),
                Red = red,
                Green = green,
                Blue = blue
            });
        }

        return returnGame;
    }
}