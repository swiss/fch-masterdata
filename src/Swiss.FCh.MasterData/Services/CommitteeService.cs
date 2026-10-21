using Swiss.FCh.MasterData.Mapper;
using Swiss.FCh.MasterData.Models;
using Swiss.FCh.Sparql.Client;
using Swiss.FCh.Sparql.Client.Configuration;

namespace Swiss.FCh.MasterData.Services;

internal class CommitteeService : ICommitteeService
{
    private readonly IMasterDataClient _masterDataClient;

    public CommitteeService(IMasterDataClient masterDataClient)
    {
        _masterDataClient = masterDataClient;
    }

    public async Task<IEnumerable<Committee>> GetCommittees(CancellationToken ct)
    {
        var queryResult = await _masterDataClient.GetMasterData(
            DefinedTermsets.Committees,
            ct,
            endpointUrl:SparqlEndpoints.LdAdmin,
            dataModel: DataModels.SchemaOrg).ConfigureAwait(false);

        return queryResult.Select(CommitteeMapper.MapToCommittee);
    }
}
