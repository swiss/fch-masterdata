using Swiss.FCh.MasterData.Models;

namespace Swiss.FCh.MasterData.Mapper;

internal static class CommitteeMapper
{
    internal static Committee MapToCommittee(Sparql.Models.MasterData masterData)
    {
        return new Committee
        {
            Uri = masterData.DefinedTerm,
            NameDe = masterData.NameDe,
            NameFr = masterData.NameFr,
            NameIt = masterData.NameIt,
            StartDate = masterData.Start.GetValueOrDefault(DateOnly.MinValue),
            EndDate = masterData.End.GetValueOrDefault(DateOnly.MaxValue)
        };
    }
}
