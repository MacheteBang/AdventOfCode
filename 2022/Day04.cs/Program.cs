string[] exampleLines = File.ReadAllLines("input.example.txt");
string[] lines = File.ReadAllLines("input.txt");

int fullyOverlappingAssignments = 0;
int anyOverlappingAssignments = 0;

foreach (string line in lines)
{
    Assignment assignment1 = new Assignment(line.Split(",")[0]);
    Assignment assignment2 = new Assignment(line.Split(",")[1]);

    if (assignment1.FullyOverlaps(assignment2) || assignment2.FullyOverlaps(assignment1)) fullyOverlappingAssignments++;
    if (assignment1.AnyOverlaps(assignment2) || assignment2.AnyOverlaps(assignment1)) anyOverlappingAssignments++;
}

Console.WriteLine($"Fully overlapping assignments: {fullyOverlappingAssignments}");
Console.WriteLine($"Any overlapping assignments: {anyOverlappingAssignments}");