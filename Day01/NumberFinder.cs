public static class NumberFinder
{
    public static bool TryGetDigitFromString(string input, out int digit)
    {
        digit = -1;

        if (string.IsNullOrEmpty(input))
        {
            return false;
        }

        switch (input)
        {
            case string s when s.StartsWith("zero", StringComparison.OrdinalIgnoreCase):
                digit = 0;
                return true;
            case string s when s.StartsWith("one", StringComparison.OrdinalIgnoreCase):
                digit = 1;
                return true;
            case string s when s.StartsWith("two", StringComparison.OrdinalIgnoreCase):
                digit = 2;
                return true;
            case string s when s.StartsWith("three", StringComparison.OrdinalIgnoreCase):
                digit = 3;
                return true;
            case string s when s.StartsWith("four", StringComparison.OrdinalIgnoreCase):
                digit = 4;
                return true;
            case string s when s.StartsWith("five", StringComparison.OrdinalIgnoreCase):
                digit = 5;
                return true;
            case string s when s.StartsWith("six", StringComparison.OrdinalIgnoreCase):
                digit = 6;
                return true;
            case string s when s.StartsWith("seven", StringComparison.OrdinalIgnoreCase):
                digit = 7;
                return true;
            case string s when s.StartsWith("eight", StringComparison.OrdinalIgnoreCase):
                digit = 8;
                return true;
            case string s when s.StartsWith("nine", StringComparison.OrdinalIgnoreCase):
                digit = 9;
                return true;
            default:
                break;
        }

        if (input.Length >= 1 && char.IsDigit(Convert.ToChar(input[0])))
        {
            digit = Convert.ToInt32(input[0].ToString());
            return true;
        }

        return false;
    }
}