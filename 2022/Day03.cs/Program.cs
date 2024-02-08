var lines = File.ReadAllLines("input.txt");

int setCount = 3;


int totalPriority = 0;
int badgePriority = 0;

for (int i = 0; i < lines.Length; i++)
{
    string[] splitBackpacks = { lines[i].Substring(0, lines[i].Length / 2), lines[i].Substring(lines[i].Length / 2) };

    totalPriority += GetValueFromCharacter(FindCommonCharacter(splitBackpacks));

    if ((i + 1) % setCount == 0)
    {
        var lastSet = new ArraySegment<string>(lines, i - setCount + 1, setCount);
        badgePriority += GetValueFromCharacter(FindCommonCharacter(lastSet.ToArray()));
    }


    // Console.WriteLine(line);
    // Console.WriteLine(left);
    // Console.WriteLine(right);
    // Console.WriteLine(FindCommonCharacter(left, right));
    // Console.WriteLine(GetValueFromCharacter(FindCommonCharacter(left, right)));
    // Console.WriteLine();
}

Console.WriteLine($"Total Priority when two compartments per backpack: {totalPriority}");
Console.WriteLine($"Total Priority when one backpack per three elves: {badgePriority}");


char FindCommonCharacter(string[] compartments)
{
    if (compartments.Length == 0) return '-';
    int matchesRequired = compartments.Length - 1;

    return compartments[0]
        .FirstOrDefault(c => compartments.Skip(1).Count(s => s.Contains(c)) == matchesRequired);
}




int GetValueFromCharacter(char character) => character switch
{
    >= 'a' and <= 'z' => (int)character - 96,
    >= 'A' and <= 'Z' => (int)character - 38,
    _ => 0
};