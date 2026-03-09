namespace Swiss.FCh.MasterData.Models;

public class Committee
{
    public required string Uri { get; init; }

    public string? NameDe { get; init; }

    public string? NameFr { get; init; }

    public string? NameIt { get; init; }

    public DateOnly StartDate { get; init; }

    public DateOnly EndDate { get; init; }
}
