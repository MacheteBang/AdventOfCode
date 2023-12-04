string filePath = "./input-test.txt";
string[] lines = File.ReadAllLines(filePath);

List<ScratchCard> scratchCards = lines.Select(ScratchCard.ToScratchCard).ToList();
int totalPoints = scratchCards.Sum(c => c.WinningPoints());

Console.WriteLine($"Total points are: {totalPoints}");