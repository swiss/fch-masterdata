using Swiss.FCh.MasterData.Mapper;
using Swiss.FCh.MasterData.Models;
using Swiss.FCh.Sparql.Client;
using Swiss.FCh.Sparql.Client.Configuration;
using Swiss.FCh.Sparql.Models;

namespace Swiss.FCh.MasterData.Services;

internal class OfficeService : IOfficeService
{
    private readonly IMasterDataClient _masterDataClient;

    public OfficeService(IMasterDataClient masterDataClient)
    {
        _masterDataClient = masterDataClient;
    }

    public async Task<IEnumerable<Office>> GetOffices(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var additionalAttributes = new List<AdditionalAttribute>
        {
            new() { Term = OfficeConstants.DepartmentSchema, Identifier = OfficeConstants.DepartmentUriIdentifier },
            new() { Term = OfficeConstants.AdditionalTypeSchema, Identifier = OfficeConstants.GeneralSecretariatIdentifier, Values = new[] { OfficeConstants.GeneralSecretariatUri } },
            new() { Term = OfficeConstants.AdditionalTypeSchema, Identifier = OfficeConstants.CentralFederalAdministrationIdentifier, Values = new[] { OfficeConstants.CentralFederalAdministrationUri, OfficeConstants.DecentralisedFederalAdministrationUri } }
        };

        var queryResult = await _masterDataClient.GetMasterData(
            DefinedTermsets.Offices,
            ct,
            endpointUrl: SparqlEndpoints.LdAdmin,
            dataModel: DataModels.SchemaOrg,
            additionalAttributes: additionalAttributes);

        return queryResult.Select(OfficeMapper.MapToOffice);
    }
}
