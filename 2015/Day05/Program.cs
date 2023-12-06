string filePath = "./input.txt";
string[] inputLines = File.ReadAllLines(filePath);

string[] _vowels = ["a", "e", "i", "o", "u"];
string[] _badStrings = ["ab", "cd", "pq", "xy"];

int niceKids = 0;

foreach (string inputLine in inputLines)
{
    int vowelCount = _vowels.Sum(v => inputLine.Count(i => i == Convert.ToChar(v)));
    int doubleLetterCount = GetDoubleLetters(inputLine);
    int badStringCount = _badStrings.Count(inputLine.Contains);

    if (IsNiceKid(vowelCount, doubleLetterCount, badStringCount)) niceKids++;
}

Console.WriteLine($"Count of Nice Kids: {niceKids}");
Console.WriteLine("End of Program");

int GetDoubleLetters(string str)
{
    int doubleLetterCount = 0;

    for (int i = 1; i < str.Length; i++)
    {
        if (str[i] == str[i - 1]) doubleLetterCount++;
    }

    return doubleLetterCount;
}
bool IsNiceKid(int vowelCount, int doubleLetterCount, int badStringCount)
{
    int minVowels = 3;
    int minDoubleLetters = 1;
    int maxBadStrings = 0;

    return vowelCount >= minVowels
        && doubleLetterCount >= minDoubleLetters
        && badStringCount == maxBadStrings;
}
