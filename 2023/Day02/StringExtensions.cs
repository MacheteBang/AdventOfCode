public static class StringExtensions
{
    public static int ToIntValue(this string? value)
    {
        if (string.IsNullOrEmpty(value)) return 0;

        string numericString = string.Empty;

        foreach (char c in value)
        {
            if (char.IsDigit(c))
            {
                numericString += c;
            }
        }

        if (int.TryParse(numericString, out int returnValue))
        {
            return returnValue;
        }

        return 0;
    }
}