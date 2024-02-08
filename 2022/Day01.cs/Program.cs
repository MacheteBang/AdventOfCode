int currentCalorieCount = 0;
List<int> elves = new();

foreach (string? s in System.IO.File.ReadAllLines("input.txt"))
{
    if (string.IsNullOrEmpty(s)) {
        elves.Add(currentCalorieCount);
        currentCalorieCount = 0;
        continue;
    }

    currentCalorieCount += Int32.Parse(s);

}

int top3 = elves.OrderByDescending(e => e).Take(3).Sum(e => e);

Console.WriteLine($"Sum of Top 3 is: {top3}");