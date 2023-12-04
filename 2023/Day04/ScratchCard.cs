public class ScratchCard
{
    public int[] WinningNumbers { get; set; } = [];
    public int[] PlayerNumbers { get; set; } = [];

    public bool IsWinner()
    {
        throw new NotImplementedException();
    }

    public int WinningPoints()
    {
        var winningNumbers = PlayerNumbers.Count(p => WinningNumbers.Contains(p));
        return Convert.ToInt32(Math.Pow(2, winningNumbers - 1));
    }

    public static ScratchCard ToScratchCard(string cardInfo)
    {
        string[] sections = cardInfo.Split(':');
        string cardNameString = sections[0].Trim();
        string[] numbersString = sections[1].Trim().Split('|');

        return new()
        {
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