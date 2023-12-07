public class Hand
{
    public Card[] Cards { get; set; } = [];
    public uint Bid { get; set; }
    public RankType? Rank { get; set; } = null;
}