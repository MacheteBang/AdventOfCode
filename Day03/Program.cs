using System.Drawing;

var filePath = "input.txt";
string[] lines = File.ReadAllLines(filePath);

char[,] matrix = ConvertLinesToMatrix(lines);
var partNumbers = GetPartNumbersFromMatrix(matrix);
partNumbers.ForEach(p => p.IsValid = IsValidPartNumber(p, matrix));


int sumOfPartValidPartNumbers = partNumbers.Where(p => p.IsValid).Sum(p => p.Value);


Console.WriteLine(sumOfPartValidPartNumbers);







bool IsValidPartNumber(PartNumber part, char[,] matrix)
{
    Point TL = new Point(Math.Max(part.StartLocation.X - 1, 0), Math.Max(part.StartLocation.Y - 1, 0));
    Point TR = new Point(Math.Min(part.EndLocation.X + 1, matrix.GetLength(0) - 1), Math.Max(part.EndLocation.Y - 1, 0));
    Point BL = new Point(Math.Max(part.StartLocation.X - 1, 0), Math.Min(part.StartLocation.Y + 1, matrix.GetLength(1) - 1));
    Point BR = new Point(Math.Min(part.EndLocation.X + 1, matrix.GetLength(0) - 1), Math.Min(part.EndLocation.Y + 1, matrix.GetLength(1) - 1));

    // TOP
    if (TL.Y < part.StartLocation.Y && TR.Y < part.EndLocation.Y)
    {
        char[] slice = Enumerable.Range(TL.X, TR.X - TL.X + 1).Select(x => matrix[x, TL.Y]).ToArray();
        if (DoesSliceHaveCode(slice)) return true;
    }

    // RIGHT
    if (TR.X > part.StartLocation.X && BR.X > part.EndLocation.X)
    {
        char[] slice = Enumerable.Range(TR.Y, BR.Y - TR.Y + 1).Select(y => matrix[TR.X, y]).ToArray();
        if (DoesSliceHaveCode(slice)) return true;
    }

    // BOTTOM
    if (BL.Y > part.StartLocation.Y && BR.Y > part.EndLocation.Y)
    {
        char[] slice = Enumerable.Range(BL.X, BR.X - BL.X + 1).Select(x => matrix[x, BL.Y]).ToArray();
        if (DoesSliceHaveCode(slice)) return true;
    }

    // LEFT
    if (TL.X < part.StartLocation.X && BL.X < part.EndLocation.X)
    {
        char[] slice = Enumerable.Range(TL.Y, BL.Y - TL.Y + 1).Select(y => matrix[TL.X, y]).ToArray();
        if (DoesSliceHaveCode(slice)) return true;
    }

    return false;
}

bool DoesSliceHaveCode(char[] slice)
{
    foreach (char c in slice)
    {
        if (!char.IsDigit(c) && c != '.')
        {
            return true;
        }
    }

    return false;
}

char[,] ConvertLinesToMatrix(string[] lines)
{

    int ySize = lines.Length;
    int xSize = lines.Max(l => l.Length);

    char[,] returnValue = new char[xSize, ySize];

    int x = 0, y = 0;

    foreach (string l in lines)
    {
        foreach (char c in l)
        {
            returnValue[x, y] = c;

            x++;
        }

        x = 0;
        y++;
    }

    return returnValue;
}

List<PartNumber> GetPartNumbersFromMatrix(char[,] metrix)
{
    List<PartNumber> returnValue = new();

    PartNumber? currentPartNumber = default;

    for (int y = 0; y < matrix.GetLength(1); y++)
    {
        for (int x = 0; x < matrix.GetLength(0); x++)
        {
            if (char.IsDigit(matrix[x, y]))
            {
                currentPartNumber = currentPartNumber ?? new() { StartLocation = new Point(x, y) };
                currentPartNumber.ConcatDigit(matrix[x, y].ToString());
            }
            else
            {
                currentPartNumber = ResetPartNumber(returnValue, currentPartNumber, y, x - 1);
            }
            if (currentPartNumber is not null && x == matrix.GetLength(0) - 1)
            {
                currentPartNumber = ResetPartNumber(returnValue, currentPartNumber, y, x);
            }
        }
    }

    return returnValue;
}

static PartNumber? ResetPartNumber(List<PartNumber> returnValue, PartNumber? currentPartNumber, int y, int x)
{
    if (currentPartNumber is not null)
    {
        currentPartNumber.EndLocation = new Point(x, y);

        PartNumber newPart = new()
        {
            StartLocation = currentPartNumber.StartLocation,
            EndLocation = currentPartNumber.EndLocation,
        };
        newPart.ConcatDigit(currentPartNumber.Value.ToString());

        returnValue.Add(newPart);
        currentPartNumber = null;
    }

    return currentPartNumber;
}