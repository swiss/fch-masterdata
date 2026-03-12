using Swiss.FCh.MasterData.Mapper;
using Swiss.FCh.MasterData.Models;
using Swiss.FCh.Sparql.Client;
using Swiss.FCh.Sparql.Client.Configuration;
using Swiss.FCh.Sparql.Models;

namespace Swiss.FCh.MasterData.Services;

public class CountryService : ICountryService
{
    private readonly IMasterDataClient _masterDataClient;

    public CountryService(IMasterDataClient masterDataClient)
    {
        _masterDataClient = masterDataClient;
    }

    public async Task<IEnumerable<Country>> GetCountries(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var additionalAttributes = new List<AdditionalAttribute>
        {
            new() { Term = Country.CountryConstants.ShortNameSchema, Identifier = CantonConstants.ShortNameIdentifier }
        };

        var queryResult = await _masterDataClient.GetMasterData(
            DefinedTermsets.Countries,
            ct,
            additionalAttributes: additionalAttributes,
            endpointUrl: SparqlEndpoints.LdAdmin,
            dataModel: DataModels.SchemaOrg).ConfigureAwait(false);

        return queryResult.Select(CountryMapper.MapToCountry);
    }
}
