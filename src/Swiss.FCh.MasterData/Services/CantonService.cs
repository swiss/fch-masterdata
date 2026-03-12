using Swiss.FCh.MasterData.Mapper;
using Swiss.FCh.MasterData.Models;
using Swiss.FCh.Sparql.Client;
using Swiss.FCh.Sparql.Client.Configuration;
using Swiss.FCh.Sparql.Models;

namespace Swiss.FCh.MasterData.Services;

public class CantonService : ICantonService
{
    private readonly IMasterDataClient _masterDataClient;

    public CantonService(IMasterDataClient masterDataClient)
    {
        _masterDataClient = masterDataClient;
    }

    public async Task<IEnumerable<Canton>> GetCantons(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var additionalAttributes = new List<AdditionalAttribute>
        {
            new() { Term = CantonConstants.ShortNameSchema, Identifier = CantonConstants.ShortNameIdentifier }
        };

        var queryResult = await _masterDataClient.GetMasterData(
            DefinedTermsets.Cantons,
            ct,
            additionalAttributes: additionalAttributes,
            endpointUrl: SparqlEndpoints.LdAdmin,
            dataModel: DataModels.SchemaOrg).ConfigureAwait(false);

        return queryResult.Select(CantonMapper.MapToCanton);
    }
}
