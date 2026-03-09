using Swiss.FCh.MasterData.Mapper;
using Swiss.FCh.MasterData.Models;
using Swiss.FCh.Sparql.Client;

namespace Swiss.FCh.MasterData.Services;

internal class AgentService : IAgentService
{
    private readonly IAgentClient _agentClient;

    public AgentService(IAgentClient agentClient)
    {
        _agentClient = agentClient;
    }

    public async Task<IEnumerable<Agent>> GetAgents(
        IEnumerable<string> uris,
        CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var queryResult = await _agentClient.GetAgents(uris, ct);

        return queryResult.Select(AgentMapper.MapToAgent);
    }
}
