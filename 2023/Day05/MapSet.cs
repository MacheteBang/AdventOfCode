public readonly struct MapSet
{
    public long DestinationStart { get; }
    public long SourceStart { get; }
    public long Length { get; }

    public MapSet(long destinationStart, long sourceStart, long length)
    {
        DestinationStart = destinationStart;
        SourceStart = sourceStart;
        Length = length;
    }
}