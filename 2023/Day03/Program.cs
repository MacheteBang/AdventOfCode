using System.Drawing;

var filePath = "input.txt";
string[] lines = File.ReadAllLines(filePath);

char[,] matrix = ConvertLinesToMatrix(lines);
var partNumbers = GetPartNumbersFromMatrix(matrix);
partNumbers.ForEach(p => p.IsValid = IsValidPartNumber(p, matrix));


int sumOfPartValidPartNumbers = partNumbers.Where(p => p.IsValid).Sum(p => p.Value);
int sumOfGearRatios = 0;

for (int x = 0; x < matrix.GetLength(0); x++)
{
    for (int y = 0; y < matrix.GetLength(1); y++)
    {
        if (matrix[x, y] == '*')
        {
            var partsOnGear = partNumbers.Where(p => p.IsValid).Where(p => p.IsPointAdjacent(new Point(x, y))).ToList();
            if (partsOnGear.Count() == 2)
            {
                sumOfGearRatios += partsOnGear.Aggregate(1, (a, b) => a * b.Value);
            }

        }
    }
}


Console.WriteLine($"Sum of ValidPartNumbers: {sumOfPartValidPartNumbers}");
Console.WriteLine($"Sum of Gear Ratios: {sumOfGearRatios}");







bool IsValidPartNumber(Part part, char[,] matrix)
{
    // TOP
    if (part.HasTop)
    {
        char[] slice = Enumerable.Range(part.TL.X, part.TR.X - part.TL.X + 1).Select(x => matrix[x, part.TL.Y]).ToArray();
        if (DoesSliceHaveCode(slice)) return true;
    }

    // RIGHT
    if (part.HasRight)
    {
        char[] slice = Enumerable.Range(part.TR.Y, part.BR.Y - part.TR.Y + 1).Select(y => matrix[part.TR.X, y]).ToArray();
        if (DoesSliceHaveCode(slice)) return true;
    }

    // BOTTOM
    if (part.HasBottom)
    {
        char[] slice = Enumerable.Range(part.BL.X, part.BR.X - part.BL.X + 1).Select(x => matrix[x, part.BL.Y]).ToArray();
        if (DoesSliceHaveCode(slice)) return true;
    }

    // LEFT
    if (part.HasLeft)
    {
        char[] slice = Enumerable.Range(part.TL.Y, part.BL.Y - part.TL.Y + 1).Select(y => matrix[part.TL.X, y]).ToArray();
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

List<Part> GetPartNumbersFromMatrix(char[,] metrix)
{
    List<Part> returnValue = new();

    Part? currentPartNumber = default;

    for (int y = 0; y < matrix.GetLength(1); y++)
    {
        for (int x = 0; x < matrix.GetLength(0); x++)
        {
            if (char.IsDigit(matrix[x, y]))
            {
                currentPartNumber = currentPartNumber ?? new() { StartLocation = new Point(x, y), MaxBounds = new Point(matrix.GetLength(0) - 1, matrix.GetLength(1) - 1) };
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

static Part? ResetPartNumber(List<Part> returnValue, Part? currentPartNumber, int y, int x)
{
    if (currentPartNumber is not null)
    {
        currentPartNumber.EndLocation = new Point(x, y);
        returnValue.Add(currentPartNumber);
        currentPartNumber = null;
    }

    return currentPartNumber;
}