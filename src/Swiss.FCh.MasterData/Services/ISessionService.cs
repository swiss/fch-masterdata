using Swiss.FCh.MasterData.Models;

namespace Swiss.FCh.MasterData.Services;

public interface ISessionService
{
    Task<IEnumerable<Session>> GetSessions(CancellationToken ct);
    Task<IEnumerable<Session>> GetSessionsByDateRange(DateOnly startDate, DateOnly endDate, CancellationToken ct);
}
