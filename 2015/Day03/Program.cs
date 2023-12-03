using System.Drawing;

string input = "./input.txt";
string directions = File.ReadAllText(input);

Point location = new Point(0, 0);
List<Point> housesWithPresents = new() { location };

foreach (char d in directions)
{
    switch (d)
    {
        case '<': location.X--; break;
        case '>': location.X++; break;
        case '^': location.Y++; break;
        case 'v': location.Y--; break;
        default: break;
    }

    housesWithPresents.Add(location);
}

var multiHouseVisits = housesWithPresents
    .GroupBy(v => v)
    .Select(v => new { Location = v.First(), Visits = v.Count() })
    .Where(v => v.Visits >= 1);

int housesWithMultipleVisits = multiHouseVisits.Count();
Console.WriteLine($"Houses with multiple visits: {housesWithMultipleVisits}");