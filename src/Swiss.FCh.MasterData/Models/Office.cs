namespace Swiss.FCh.MasterData.Models;

public class Office
{
    public required string Uri { get; init; }
    public string? Position { get; init; }
    public required string NameDe { get; init; }
    public required string NameFr { get; init; }
    public required string NameIt { get; init; }
    public required string ShortNameDe { get; init; }
    public required string ShortNameFr { get; init; }
    public required string ShortNameIt { get; init; }
    public required string DepartmentUri { get; init; }
    public bool IsGeneralSecretariat { get; init; }
    public bool IsCentralFederalAdministration { get; init; }
    public required DateOnly StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
}

internal static class OfficeConstants
{
#pragma warning disable S1075
    public const string DepartmentSchema = "http://schema.org/parentOrganization";
    public const string DepartmentUriIdentifier = "departmentUri";

    public const string AdditionalTypeSchema = "http://schema.org/additionalType";
    public const string GeneralSecretariatIdentifier = "generalSecretariat";
    public const string CentralFederalAdministrationIdentifier = "centralFederalAdministration";

    public const string GeneralSecretariatUri = "https://register.ld.admin.ch/termdat/441817";
    public const string CentralFederalAdministrationUri = "https://register.ld.admin.ch/termdat/57178";
    public const string DecentralisedFederalAdministrationUri = "https://register.ld.admin.ch/termdat/57179";
#pragma warning restore S1075
}
