using System.Text;

public class Plane<T>
{
    private uint _columns;
    private uint _rows;
    T[,] _values;

    public uint Width => _columns;
    public uint Height => _rows;

    public Plane(uint columns, uint rows)
    {
        if (columns == 0) throw new ArgumentException("Width must be greater than 0.", nameof(columns));
        if (rows == 0) throw new ArgumentException("Height must be greater than 0.", nameof(rows));

        _columns = columns;
        _rows = rows;
        _values = new T[columns, rows];
    }

    public T Get(uint x, uint y)
    {
        ThrowIfOutOfRange(x, y);
        return _values[x, y];
    }
    public T Get(Coordinates coordinates)
    {
        ThrowIfOutOfRange(coordinates.X, coordinates.Y);
        return _values[coordinates.X, coordinates.Y];
    }
    public void Set(uint x, uint y, T value)
    {
        ThrowIfOutOfRange(x, y);
        _values[x, y] = value;
    }
    public void Set(Coordinates coordinates, T value)
    {
        ThrowIfOutOfRange(coordinates.X, coordinates.Y);
        _values[coordinates.X, coordinates.Y] = value;
    }
    public T[] To1DArray(string axis, uint lockedCoordinate)
    {
        if (!"XY".Contains(axis)) throw new ArgumentException($"Only X or Y are valid values for {nameof(axis)}");

        T[] newArray;

        if (axis.Equals("x", StringComparison.OrdinalIgnoreCase))
        {
            newArray = new T[_rows];

            for (uint y = 0; y < _rows; y++)
            {
                newArray[y] = _values[lockedCoordinate, y];
            }

            return newArray;
        }

        newArray = new T[_columns];

        for (uint x = 0; x < _columns; x++)
        {
            newArray[x] = _values[x, lockedCoordinate];
        }

        return newArray;
    }
    public string ToString(int padding = 0)
    {
        StringBuilder sb = new();

        for (int y = 0; y < _rows; y++)
        {
            for (int x = 0; x < _columns; x++)
            {
                sb.Append('|');
                sb.Append(_values[x, y].ToString().PadLeft(padding));
                sb.Append(' ');
            }
            sb.AppendLine("|");
        }

        return sb.ToString();
    }


    private void ThrowIfOutOfRange(uint x, uint y)
    {
        if (x > _columns - 1) throw new IndexOutOfRangeException($"{nameof(x)} is out of range. ");
        if (y > _rows - 1) throw new IndexOutOfRangeException($"{nameof(y)} is out of range. ");
    }
}
