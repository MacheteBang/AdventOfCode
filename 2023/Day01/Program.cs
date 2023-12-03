string filePath = "./calibration.txt";
string[] fileContents = File.ReadAllLines(filePath);

int calibration = 0;

foreach (string line in fileContents)
{
    calibration += GetNumber(line);
}

Console.WriteLine(calibration);







static int GetNumber(string input)
{
    bool hasFirstDigit = false; int firstDigit = -1;
    bool hasLastDigit = false; int lastDigit = -1;

    for (int index = 0; index < input.Length; index++)
    {
        if (!hasFirstDigit)
        {
            hasFirstDigit = NumberFinder.TryGetDigitFromString(input.Substring(index), out firstDigit);
        }

        if (!hasLastDigit)
        {
            int indexFromEnd = input.Length - 1 - index;
            hasLastDigit = NumberFinder.TryGetDigitFromString(input.Substring(indexFromEnd), out lastDigit);
        }

        if (hasFirstDigit && hasLastDigit)
        {
            break;
        }
    }

    string finalNumber = hasFirstDigit ? firstDigit.ToString() : string.Empty;
    finalNumber += hasLastDigit ? lastDigit.ToString() : string.Empty;

    Console.WriteLine(finalNumber);
    return Convert.ToInt16(finalNumber);
}
