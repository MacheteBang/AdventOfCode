string inputFilePath = "./input.txt";
string[] inputLines = File.ReadAllLines(inputFilePath);

int sumOfPrevValues = 0;
int sumOfNextValues = 0;

for (int j = 0; j < inputLines.Length; j++)
{
    Stack<int[]> readingsStack = new();
    int[] readings = inputLines[j].Split(" ").Select(l => Convert.ToInt32(l)).ToArray();

    readingsStack.Push(readings);

    int[] next = readings;
    while (!(next.Distinct().Count() == 1 && next.First() == 0))
    {
        next = GetDifferencReadings(next);

        if (!(next.Distinct().Count() == 1 && next.First() == 0)) readingsStack.Push(next);
    }


    int prevValue = 0;
    int nextValue = 0;
    while (readingsStack.Any())
    {
        var stack = readingsStack.Pop();
        prevValue = stack.First() - prevValue;
        nextValue = stack.Last() + nextValue;
    }

    sumOfPrevValues += prevValue;
    sumOfNextValues += nextValue;

    Console.WriteLine($"{prevValue.ToString().PadLeft(10, ' ')} | {nextValue.ToString().PadRight(10, ' ')}: {inputLines[j]}");
}

Console.WriteLine($"Sum of New Values: {sumOfPrevValues}");
Console.WriteLine($"Sum of New Values: {sumOfNextValues}");
Console.WriteLine("End of Program");




int[] GetDifferencReadings(int[] readings)
{
    int[] difference = new int[readings.Length - 1];

    for (int i = 0; i < readings.Length - 1; i++)
    {
        difference[i] = readings[i + 1] - readings[i];
    }

    return difference;
}

