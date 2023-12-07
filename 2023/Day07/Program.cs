string inputFilePath = "./input.txt";
string[] inputLines = File.ReadAllLines(inputFilePath);

Dictionary<char, uint> cardValues = new()
{
    {'2', 0},
    {'3', 1},
    {'4', 2},
    {'5', 3},
    {'6', 4},
    {'7', 5},
    {'8', 6},
    {'9', 7},
    {'T', 8},
    {'J', 9},
    {'Q', 10},
    {'K', 11},
    {'A', 12}
};

List<Hand> hands = [];

foreach (string inputLine in inputLines)
{
    string[] inputParts = inputLine.Split(' ');

    List<Card> rawCards = [];
    foreach (char card in inputParts[0])
    {
        rawCards.Add(new()
        {
            Face = card,
            Value = cardValues[card]
        });
    }

    hands.Add(new()
    {
        Bid = uint.Parse(inputParts[1]),
        Cards = rawCards.ToArray(),//.OrderByDescending(c => c.Value).ToArray(),
        Rank = GetRankType(rawCards.OrderByDescending(c => c.Value).ToList())
    });
}

var orderedHands = hands.OrderBy(h => h.Rank)
    .ThenBy(h => h.Cards[0].Value)
    .ThenBy(h => h.Cards[1].Value)
    .ThenBy(h => h.Cards[2].Value)
    .ThenBy(h => h.Cards[3].Value)
    .ThenBy(h => h.Cards[4].Value)
    .ToArray();


File.WriteAllLines("results.txt", orderedHands.Select(h => string.Join("", h.Cards.Select(c => c.Face).ToArray()) + " " + h.Bid));

uint totalWinnings = 0;
for (uint i = 0; i < orderedHands.Length; i++)
{
    totalWinnings += (i + 1) * orderedHands[i].Bid;
}

Console.WriteLine($"Total Winnings: {totalWinnings}");
Console.WriteLine("End of Program");

RankType GetRankType(List<Card> cards)
{
    if (cards.Count != 5) throw new Exception("Card amount != 5 during Rank Determination");
    List<char> cardfaces = cards.Select(c => c.Face).ToList();

    // Five of a Kind
    if (cardfaces.Distinct().Count() == 1) return RankType.FiveOfAKind;

    var groupings = cardfaces.GroupBy(k => k).Select(g => new { Face = g, Count = g.Count() });

    // Four of a Kind
    if (groupings.Where(g => g.Count == 4).Any()) return RankType.FourOfAKind;

    // Full House
    if (groupings.Where(g => g.Count == 3).Any() && groupings.Where(g => g.Count == 2).Any()) return RankType.FullHouse;

    // Three of a Kind
    if (groupings.Where(g => g.Count == 3).Any()) return RankType.ThreeOfAKind;

    // Two Pair
    if (groupings.Where(g => g.Count == 2).Count() == 2) return RankType.TwoPair;

    // One Pair
    if (groupings.Where(g => g.Count == 2).Any()) return RankType.OnePair;

    return RankType.HighCard;
}