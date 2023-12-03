string input = "./input.txt";
string[] presentDimensions = File.ReadAllLines(input);

uint totalWrappingPaper = 0;
uint totalRibbon = 0;

foreach (string d in presentDimensions)
{
    string[] dimensions = d.Split("x");
    Box present = new Box(Convert.ToUInt32(dimensions[0]), Convert.ToUInt32(dimensions[1]), Convert.ToUInt32(dimensions[2]));

    totalWrappingPaper += present.GetSurfaceArea() + present.GetSurfaceAreaOfSmallestSide();
    totalRibbon += present.GetVolume() + present.GetMinPerimeter();
}

Console.WriteLine($"Total paper needed: {totalWrappingPaper}");
Console.WriteLine($"Total ribbon needed: {totalRibbon}");