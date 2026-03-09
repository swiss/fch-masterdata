using Swiss.FCh.MasterData.Models;

namespace Swiss.FCh.MasterData.Services;

public interface ICantonService
{
    Task<IEnumerable<Canton>> GetCantons(CancellationToken ct);
}
