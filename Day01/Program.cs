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
    string firstDigit = string.Empty;
    string lastDigit = string.Empty;
    for (int index = 0; index < input.Length; index++)
    {
        if (string.IsNullOrEmpty(firstDigit) && char.IsDigit(input[index]))
        {
            firstDigit = Convert.ToString(input[index]);
        }

        int indexFromEnd = input.Length - 1 - index;
        if (string.IsNullOrEmpty(lastDigit) && char.IsDigit(input[indexFromEnd]))
        {
            lastDigit = Convert.ToString(input[indexFromEnd]);
        }

        if (!string.IsNullOrEmpty(firstDigit) && !string.IsNullOrEmpty(lastDigit))
        {
            break;
        }
    }

    return Convert.ToInt16($"{firstDigit}{lastDigit}");
}