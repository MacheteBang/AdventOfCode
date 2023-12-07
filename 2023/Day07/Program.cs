string inputFilePath = "./input.txt";
string[] inputLines = File.ReadAllLines(inputFilePath);

Dictionary<char, uint> cardValues = new()
{
    {'J', 0}, // Joker
    {'2', 1},
    {'3', 2},
    {'4', 3},
    {'5', 4},
    {'6', 5},
    {'7', 6},
    {'8', 7},
    {'9', 8},
    {'T', 9},
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


File.WriteAllLines("results.txt", orderedHands.Select(h => string.Join("", h.Cards.Select(c => c.Face).ToArray()) + " " + h.Bid + " " + h.Rank.ToString()));

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

    // Five of a Kind (natural)
    if (cardfaces.Distinct().Count() == 1) return RankType.FiveOfAKind;

    var groupings = cardfaces.GroupBy(k => k).Select(g => new { Face = g.Key, Count = g.Count() }).ToList();


    // Five of kind (w/ Joker)
    if (groupings.Where(g => g.Count == 4 && g.Face != 'J').Any() && groupings.Where(g => g.Count == 1 && g.Face == 'J').Any()) return RankType.FiveOfAKind;
    if (groupings.Where(g => g.Count == 3 && g.Face != 'J').Any() && groupings.Where(g => g.Count == 2 && g.Face == 'J').Any()) return RankType.FiveOfAKind;
    if (groupings.Where(g => g.Count == 2 && g.Face != 'J').Any() && groupings.Where(g => g.Count == 3 && g.Face == 'J').Any()) return RankType.FiveOfAKind;
    if (groupings.Where(g => g.Count == 1 && g.Face != 'J').Any() && groupings.Where(g => g.Count == 4 && g.Face == 'J').Any()) return RankType.FiveOfAKind;

    // Four of a Kind
    if (groupings.Where(g => g.Count == 4).Any()) return RankType.FourOfAKind;
    if (groupings.Where(g => g.Count == 3 && g.Face != 'J').Any() && groupings.Where(g => g.Count == 1 && g.Face == 'J').Any()) return RankType.FourOfAKind;
    if (groupings.Where(g => g.Count == 2 && g.Face != 'J').Any() && groupings.Where(g => g.Count == 2 && g.Face == 'J').Any()) return RankType.FourOfAKind;
    if (groupings.Where(g => g.Count == 1 && g.Face != 'J').Any() && groupings.Where(g => g.Count == 3 && g.Face == 'J').Any()) return RankType.FourOfAKind;

    // Full House
    if (groupings.Where(g => g.Count == 3).Any() && groupings.Where(g => g.Count == 2).Any()) return RankType.FullHouse;
    if (groupings.Where(g => g.Count == 2 && g.Face != 'J').Count() == 2 && groupings.Where(g => g.Count == 1 && g.Face == 'J').Any()) return RankType.FullHouse;

    // Three of a Kind
    if (groupings.Where(g => g.Count == 3).Any()) return RankType.ThreeOfAKind;
    if (groupings.Where(g => g.Count == 2 && g.Face != 'J').Any() && groupings.Where(g => g.Count == 1 && g.Face == 'J').Any()) return RankType.ThreeOfAKind;
    if (groupings.Where(g => g.Count == 1 && g.Face != 'J').Any() && groupings.Where(g => g.Count == 2 && g.Face == 'J').Any()) return RankType.ThreeOfAKind;

    // Two Pair
    if (groupings.Where(g => g.Count == 2).Count() == 2) return RankType.TwoPair;
    if (groupings.Where(g => g.Count == 2 && g.Face != 'J').Any() && groupings.Where(g => g.Count == 1 && g.Face == 'J').Any()) return RankType.TwoPair;

    // One Pair
    if (groupings.Where(g => g.Count == 2).Any()) return RankType.OnePair;
    if (groupings.Where(g => g.Face == 'J').Any()) return RankType.OnePair;

    return RankType.HighCard;
}