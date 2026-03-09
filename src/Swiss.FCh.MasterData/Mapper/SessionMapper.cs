using Swiss.FCh.MasterData.Models;

namespace Swiss.FCh.MasterData.Mapper;

internal class SessionMapper
{
    internal static Session MapToSession(Sparql.Models.MasterData masterData)
    {
        return new Session
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
