string filePath = "./input.txt";
string[] inputLines = File.ReadAllLines(filePath);

string[] _vowels = ["a", "e", "i", "o", "u"];
string[] _badStrings = ["ab", "cd", "pq", "xy"];

int niceKids = 0;

foreach (string inputLine in inputLines)
{
    // Part 01
    int vowelCount = _vowels.Sum(v => inputLine.Count(i => i == Convert.ToChar(v)));
    int doubleLetterCount = GetDoubleLetters(inputLine);
    int badStringCount = _badStrings.Count(inputLine.Contains);

    // Part 02
    bool hasPairs = HasPairs(inputLine);
    bool hasMiddleLetter = HasMiddleLetter(inputLine);


    if (hasPairs && hasMiddleLetter) niceKids++;
}

Console.WriteLine($"Count of Nice Kids: {niceKids}");
Console.WriteLine("End of Program");


// bool IsNiceKid(int vowelCount, int doubleLetterCount, int badStringCount)
// {
//     int minVowels = 3;
//     int minDoubleLetters = 1;
//     int maxBadStrings = 0;

//     return vowelCount >= minVowels
//         && doubleLetterCount >= minDoubleLetters
//         && badStringCount == maxBadStrings;
// }

bool HasPairs(string str)
{
    for (int i = 0; i < str.Length - 1; i++)
    {
        string pair = str[i..(i + 2)];
        string rangeBefore = "";
        if (i > 1)
        {
            rangeBefore = str[(i - 2)..i];
        }

        string rangeAfter = str[(i + 2)..];

        if (rangeBefore.Contains(pair) || rangeAfter.Contains(pair))
        {
            return true;
        }
    }

    return false;
}
bool HasMiddleLetter(string str)
{
    for (int i = 0; i < str.Length - 2; i++)
    {
        if (str[i] == str[i + 2]) return true;
    }

    return false;
}


int GetDoubleLetters(string str)
{
    int doubleLetterCount = 0;

    for (int i = 1; i < str.Length; i++)
    {
        if (str[i] == str[i - 1]) doubleLetterCount++;
    }

    return doubleLetterCount;
}