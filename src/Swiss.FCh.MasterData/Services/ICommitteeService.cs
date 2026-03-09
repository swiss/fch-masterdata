using Swiss.FCh.MasterData.Models;

namespace Swiss.FCh.MasterData.Services;

public interface ICommitteeService
{
    Task<IEnumerable<Committee>> GetCommittees(
        CancellationToken ct);
}
