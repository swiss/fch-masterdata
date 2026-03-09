using Swiss.FCh.MasterData.Models;

namespace Swiss.FCh.MasterData.Services;

public interface IOfficeService
{
    Task<IEnumerable<Office>> GetOffices(CancellationToken ct);
}
