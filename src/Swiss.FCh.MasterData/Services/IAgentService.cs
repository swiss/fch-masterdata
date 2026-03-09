using Swiss.FCh.MasterData.Models;

namespace Swiss.FCh.MasterData.Services;

public interface IAgentService
{
    Task<IEnumerable<Agent>> GetAgents(
        IEnumerable<string> uris,
        CancellationToken ct);
}
