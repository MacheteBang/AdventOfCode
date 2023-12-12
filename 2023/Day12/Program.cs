string inputFilePath = "input.txt";
string[] inputLines = File.ReadAllLines(inputFilePath);


int totalArrangementCount = 0;

foreach (string i in inputLines)
{
    string[] maps = i.Split(' ');
    totalArrangementCount += GetArrangementCount(maps[0], maps[1]);
}


Console.WriteLine($"Total arrangement count: {totalArrangementCount}");
Console.WriteLine("End of Program");


static int GetArrangementCount(string map, string targetSequence)
{
    int arrangementCount = 0;

    int qCount = map.Count(m => m == '?');
    int tracker = Convert.ToInt32(Math.Pow(2, qCount));
    for (int i = 0; i < tracker; i++)
    {
        string binary = Convert.ToString(i, 2).PadLeft(qCount, '0').Replace('0', '.').Replace('1', '#');
        string newMap = map;

        var qIndex = 0;
        for (int j = 0; j < qCount; j++)
        {
            qIndex = map.IndexOf('?', qIndex);
            newMap = newMap.Remove(qIndex, 1).Insert(qIndex, binary[j].ToString());
            qIndex++;
        }

        if (Satisfy(newMap, targetSequence)) arrangementCount++;
    }

    return arrangementCount;
}

static bool Satisfy(string map, string sequence)
{
    string[] split = map.Split('.').Where(m => !string.IsNullOrEmpty(m)).ToArray();
    string result = string.Join(',', split.Select(s => s.Length));

    return result == sequence;
}