string inputData = File.ReadAllText("input-test.txt");
string[] dataFields = inputData.Split(Environment.NewLine + Environment.NewLine);

List<Plane<bool>> fields = [];

foreach (string f in dataFields)
{
    string[] fieldRows = f.Split(Environment.NewLine);
    Plane<bool> field = new(Convert.ToUInt32(fieldRows.Max(r => r.Length)), Convert.ToUInt32(fieldRows.Length));
    for (int r = 0; r < fieldRows.Length; r++)
    {
        for (int c = 0; c < fieldRows[r].Length; c++)
        {
            field.Set((uint)c, (uint)r, fieldRows[r][c] == '#');
        }
    }

    for (int y = 0; y < field.Width - 1; y++)
    {

    }

}





Console.WriteLine("End of Program");