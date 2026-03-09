namespace Swiss.FCh.MasterData.Models;

public class Department
{
    public required string Uri { get; init; }
    public string? Position { get; init; }
    public required string NameDe { get; init; }
    public required string NameFr { get; init; }
    public required string NameIt { get; init; }
    public required string ShortNameDe { get; init; }
    public required string ShortNameFr { get; init; }
    public required string ShortNameIt { get; init; }
    public required DateOnly StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
}
