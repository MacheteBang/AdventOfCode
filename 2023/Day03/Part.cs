using System.Diagnostics.Contracts;
using System.Drawing;

public class Part
{
    private string _stringValue = string.Empty;

    public Point StartLocation { get; set; }
    public Point EndLocation { get; set; }
    public bool IsValid { get; set; }
    public Point MaxBounds { get; set; }

    public Point TL => new Point(Math.Max(StartLocation.X - 1, 0), Math.Max(StartLocation.Y - 1, 0));
    public Point TR => new Point(Math.Min(EndLocation.X + 1, MaxBounds.X), Math.Max(EndLocation.Y - 1, 0));
    public Point BL => new Point(Math.Max(StartLocation.X - 1, 0), Math.Min(StartLocation.Y + 1, MaxBounds.Y));
    public Point BR => new Point(Math.Min(EndLocation.X + 1, MaxBounds.X), Math.Min(EndLocation.Y + 1, MaxBounds.Y));

    public bool HasTop => TL.Y < StartLocation.Y && TR.Y < EndLocation.Y;
    public bool HasRight => TR.X > StartLocation.X && BR.X > EndLocation.X;
    public bool HasBottom => BL.Y > StartLocation.Y && BR.Y > EndLocation.Y;
    public bool HasLeft => TL.X < StartLocation.X && BL.X < EndLocation.X;

    public int Value => _stringValue == string.Empty ? 0 : int.Parse(_stringValue);

    public void ConcatDigit(string digit) => _stringValue += digit;

    public bool IsPointAdjacent(Point point)
    {
        if (HasTop && point.X >= TL.X && point.X <= TR.X && point.Y == TR.Y) return true;
        if (HasRight && point.Y >= TR.Y && point.Y <= BR.Y && point.X == TR.X) return true;
        if (HasBottom && point.X >= BL.X && point.X <= BR.X && point.Y == BL.Y) return true;
        if (HasLeft && point.Y >= TL.Y && point.Y <= BL.Y && point.X == BL.X) return true;

        return false;
    }

}