namespace Swiss.FCh.MasterData.Models;

public class Canton
{
    public required string Uri { get; init; }
    public required string NameDe { get; init; }
    public required string NameFr { get; init; }
    public required string NameIt { get; init; }
    public required string ShortNameDe { get; init; }
    public required string ShortNameFr { get; init; }
    public required string ShortNameIt { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
}

internal static class CantonConstants
{
    public const string ShortNameSchema = "http://schema.org/alternateName";
    public const string ShortNameIdentifier = "alternateName";
}
