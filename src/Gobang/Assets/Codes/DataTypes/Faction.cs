internal enum Faction : byte
{
    None = 0,
    White = 1,
    Black = 2
}

internal static class FactionExtensions
{
    public static Faction GetOpponent(this Faction faction) => 3 - faction;
}