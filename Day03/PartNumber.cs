using System.Drawing;

public class PartNumber
{
    private string _stringValue = string.Empty;

    public Point StartLocation { get; set; }
    public Point EndLocation { get; set; }
    public bool IsValid { get; set; }


    public int Value => _stringValue == string.Empty ? 0 : int.Parse(_stringValue);

    public void ConcatDigit(string digit) => _stringValue += digit;
}