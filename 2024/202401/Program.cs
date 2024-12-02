string[] puzzleInput = File.ReadAllLines("part1.txt");

List<int> left = [];
List<int> right = [];

foreach (string line in puzzleInput)
{
    left.Add(Convert.ToInt32(line.Split("   ")[0]));
    right.Add(Convert.ToInt32(line.Split("   ")[1]));
}

left.Sort();
right.Sort();

int totalDistance = 0;
for (int i = 0; i < left.Count; i++)
{
    totalDistance += Math.Abs(left[i] - right[i]);
}

Console.WriteLine($"Part 1 Output: {totalDistance}");