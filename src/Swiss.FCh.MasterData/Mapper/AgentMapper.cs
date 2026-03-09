using Swiss.FCh.MasterData.Models;

namespace Swiss.FCh.MasterData.Mapper;

internal static class AgentMapper
{
    internal static Agent MapToAgent(Sparql.Models.MasterData masterData)
    {
        return new Agent
        {
            Uri = masterData.DefinedTerm,
            TextDe = masterData.NameDe,
            TextFr = masterData.NameFr,
            TextIt = masterData.NameIt
        };
    }
}
