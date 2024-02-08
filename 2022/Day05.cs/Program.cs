string[] lines = File.ReadAllLines("input.txt");

string[] stackDiagram = lines.Take(Array.IndexOf(lines, "") - 1).ToArray();
string[] instructions = lines.Skip(Array.IndexOf(lines, "") + 1).ToArray();

Stack<char>[] stacks = { new(), new(), new(), new(), new(), new(), new(), new(), new() };


for (int l = stackDiagram.Length - 1; l >= 0; l--)
{
    string line = stackDiagram[l];
    for (int j = 1; j < line.Length; j += 4)
    {
        int crateLocation = j;
        int stackNumber = j / 4;
        if (!char.IsWhiteSpace(line[crateLocation])) stacks[stackNumber].Push(line[crateLocation]);
    }
}

foreach (string line in instructions)
{
    string[] instructionLines = line.Split(" ");
    int crateCount = Convert.ToInt32(line.Split(" ")[1]);
    int sourceStack = Convert.ToInt32(line.Split(" ")[3]);
    int destinationStack = Convert.ToInt32(line.Split(" ")[5]);


    var tempStack = new Stack<char>();

    for (int i = 0; i < crateCount; i++)
        tempStack.Push(stacks[sourceStack - 1].Pop());

    for (int i = 0; i < crateCount; i++)
        stacks[destinationStack - 1].Push(tempStack.Pop());

}

var result = string.Join("", stacks.Where(s => s.Count != 0).Select(s => s.Peek()));

Console.WriteLine(result);