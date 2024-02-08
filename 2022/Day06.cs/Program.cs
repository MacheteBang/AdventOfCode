string input = File.ReadAllText("input.example.txt");

int marker = 14;

for (int i = (marker - 1); i < input.Length; i++)
{
    if (input.Skip(i - (marker - 1)).Take(marker).Distinct().Count() == marker)
    {
        Console.WriteLine($"Characters: {i + 1}");
        return;
    }
}