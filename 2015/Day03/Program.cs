using System.Drawing;

string input = "./input.txt";
string directions = File.ReadAllText(input);

Point santaLocation = new Point(0, 0);
Point roboLocation = new Point(0, 0);

List<Point> housesWithPresents = new() { santaLocation, roboLocation };

for (int i = 0; i < directions.Length; i++)
{
    var isSantasTurn = i % 2 == 0;

    switch (directions[i])
    {
        case '<': if (isSantasTurn) santaLocation.X--; else roboLocation.X--; break;
        case '>': if (isSantasTurn) santaLocation.X++; else roboLocation.X++; break;
        case '^': if (isSantasTurn) santaLocation.Y++; else roboLocation.Y++; break;
        case 'v': if (isSantasTurn) santaLocation.Y--; else roboLocation.Y--; break;
        default: break;
    }

    housesWithPresents.Add(isSantasTurn ? santaLocation : roboLocation);
}

var multiHouseVisits = housesWithPresents
    .GroupBy(v => v)
    .Select(v => new { Location = v.First(), Visits = v.Count() })
    .Where(v => v.Visits >= 1);

int housesWithMultipleVisits = multiHouseVisits.Count();
Console.WriteLine($"Houses with multiple visits: {housesWithMultipleVisits}");