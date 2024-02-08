public class CoordinateSet
{
    public CoordinateSet(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; set; } = 0;
    public double Y { get; set; } = 0;


    public double DistanceTo(CoordinateSet otherCoordinateSet) => Math.Sqrt(Math.Pow(otherCoordinateSet.X - X, 2) + Math.Pow(otherCoordinateSet.Y - Y, 2));
    public CoordinateSet Clone() => new(this.X, this.Y);

    public bool Equals(CoordinateSet otherCoordinateSet) => this.X == otherCoordinateSet.X && this.Y == otherCoordinateSet.Y;
    public static bool operator ==(CoordinateSet one, CoordinateSet two)
    {
        return one.Equals(two);
    }

    public static bool operator !=(CoordinateSet one, CoordinateSet two)
    {
        return !one.Equals(two);
    }

    public override string ToString()
    {
        return $"{X.ToString().PadLeft(3)},{Y.ToString().PadLeft(3)}";
    }
}