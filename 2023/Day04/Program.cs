string filePath = "./input-test.txt";
string[] lines = File.ReadAllLines(filePath);

List<ScratchCard> scratchCards = lines.Select(ScratchCard.ToScratchCard).ToList();

int countOfOriginalScratchCards = scratchCards.Max(c => c.CardId);

int countOfScratchCards = 0;
foreach (var c in scratchCards)
{
    countOfScratchCards += GetCountOfWinningCards(scratchCards, c);
}



Console.WriteLine($"Total cards: {countOfScratchCards}");


int GetCountOfWinningCards(List<ScratchCard> masterList, ScratchCard card)
{
    if (card.CountOfWinningNumbers == 0) return 1;
    int subCardTotal = 1;

    var listOfCopies = masterList
        .Where(c => c.CardId > card.CardId && c.CardId <= card.CardId + card.CountOfWinningNumbers);

    foreach (var sc in listOfCopies)
    {
        subCardTotal += GetCountOfWinningCards(masterList, sc);
    }

    return subCardTotal;
}