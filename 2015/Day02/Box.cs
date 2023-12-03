public class Box
{
    public uint Width { get; set; }
    public uint Length { get; set; }
    public uint Depth { get; set; }

    private uint _face1SurfaceArea => Width * Length;
    private uint _face2SurfaceArea => Width * Depth;
    private uint _face3SurfaceArea => Length * Depth;

    public Box(uint width, uint length, uint depth)
    {
        Width = width;
        Length = length;
        Depth = depth;
    }

    public uint GetSurfaceArea() => (2 * _face1SurfaceArea) + (2 * _face2SurfaceArea) + (2 * _face3SurfaceArea);
    public uint GetVolume() => Width * Depth * Length;
    public uint GetSurfaceAreaOfSmallestSide() => Math.Min(Math.Min(_face1SurfaceArea, _face2SurfaceArea), _face3SurfaceArea);
    public uint GetMinPerimeter()
    {
        uint min = Math.Min(Width, Math.Min(Depth, Length));
        uint min2 = 0;
        if (min == Width) min2 = Math.Min(Depth, Length);
        if (min == Depth) min2 = Math.Min(Width, Length);
        if (min == Length) min2 = Math.Min(Depth, Width);

        return min + min + min2 + min2;
    }
}