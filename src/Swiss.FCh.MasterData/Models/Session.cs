namespace Swiss.FCh.MasterData.Models;

public class Session
{
    public required string Uri { get; init; }

    public string? NameDe { get; init; }

    public string? NameFr { get; init; }

    public string? NameIt { get; init; }

    public DateOnly StartDate { get; init; }

    public DateOnly EndDate { get; init; }
}
