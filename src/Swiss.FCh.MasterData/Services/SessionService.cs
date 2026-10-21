using Swiss.FCh.MasterData.Mapper;
using Swiss.FCh.MasterData.Models;
using Swiss.FCh.Sparql.Client;
using Swiss.FCh.Sparql.Client.Configuration;

namespace Swiss.FCh.MasterData.Services;

internal class SessionService : ISessionService
{
    private readonly IMasterDataClient _masterDataClient;

    public SessionService(IMasterDataClient masterDataClient)
    {
        _masterDataClient = masterDataClient;
    }

    public async Task<IEnumerable<Session>> GetSessions(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var queryResult = await _masterDataClient.GetMasterData(
            DefinedTermsets.Sessions,
            ct,
            endpointUrl:SparqlEndpoints.LdAdmin,
            dataModel: DataModels.SchemaOrg).ConfigureAwait(false);

        return queryResult.Select(SessionMapper.MapToSession);
    }

    public async Task<IEnumerable<Session>> GetSessionsByDateRange(DateOnly startDate, DateOnly endDate, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var sessions = await GetSessions(ct).ConfigureAwait(false);

        return sessions.Where(s => s.StartDate >= startDate && s.EndDate <= endDate);
    }
}
