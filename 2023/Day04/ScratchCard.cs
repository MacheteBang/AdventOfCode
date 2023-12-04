public class ScratchCard
{
    public int CardId { get; set; }
    public int[] WinningNumbers { get; set; } = [];
    public int[] PlayerNumbers { get; set; } = [];


    public int CountOfWinningNumbers => PlayerNumbers.Count(p => WinningNumbers.Contains(p));
    public int WinningPoints => int.Parse(Math.Pow(2, CountOfWinningNumbers - 1));


    public bool IsWinner()
    {
        throw new NotImplementedException();
    }

    public static ScratchCard ToScratchCard(string cardInfo)
    {
        string[] sections = cardInfo.Split(':');
        string cardNameString = sections[0].Trim();
        string[] numbersString = sections[1].Trim().Split('|');

        return new()
        {
            CardId = int.Parse(cardNameString.Replace("Card", "").Trim()),
            WinningNumbers = numbersString[0]
                .Trim()
                .Split(' ')
                .Where(n => !string.IsNullOrEmpty(n))
                .Select(int.Parse)
                .ToArray(),
            PlayerNumbers = numbersString[1]
                .Trim()
                .Split(' ')
                .Where(n => !string.IsNullOrEmpty(n))
                .Select(int.Parse).ToArray()
        };

    }
}