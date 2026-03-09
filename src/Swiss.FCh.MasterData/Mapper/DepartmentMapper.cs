using Swiss.FCh.MasterData.Exceptions;
using Swiss.FCh.MasterData.Models;

namespace Swiss.FCh.MasterData.Mapper;

internal static class DepartmentMapper
{
    internal static Department MapToDepartment(Sparql.Models.MasterData masterData)
    {
        if (string.IsNullOrWhiteSpace(masterData.NameDe))
        {
            throw new MasterDataException($"{nameof(Sparql.Models.MasterData.NameDe)} is null or empty. DepartmentUri={masterData.DefinedTerm}");
        }

        if (string.IsNullOrWhiteSpace(masterData.NameFr))
        {
            throw new MasterDataException($"{nameof(Sparql.Models.MasterData.NameFr)} is null or empty. DepartmentUri={masterData.DefinedTerm}");
        }

        if (string.IsNullOrWhiteSpace(masterData.NameIt))
        {
            throw new MasterDataException($"{nameof(Sparql.Models.MasterData.NameIt)} is null or empty. DepartmentUri={masterData.DefinedTerm}");
        }

        return new Department
        {
            Uri = masterData.DefinedTerm,
            Position = masterData.Position,
            NameDe = masterData.NameDe,
            NameFr = masterData.NameFr,
            NameIt = masterData.NameIt,
            ShortNameDe = !string.IsNullOrWhiteSpace(masterData.ShortNameDe) ? masterData.ShortNameDe : masterData.NameDe,
            ShortNameFr = !string.IsNullOrWhiteSpace(masterData.ShortNameFr) ? masterData.ShortNameFr : masterData.NameFr,
            ShortNameIt = !string.IsNullOrWhiteSpace(masterData.ShortNameIt) ? masterData.ShortNameIt : masterData.NameIt,
            StartDate = masterData.Start.GetValueOrDefault(DateOnly.MinValue),
            EndDate = masterData.End
        };
    }
}
