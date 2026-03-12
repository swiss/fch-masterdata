using Swiss.FCh.MasterData.Mapper;
using Swiss.FCh.MasterData.Models;
using Swiss.FCh.Sparql.Client;
using Swiss.FCh.Sparql.Client.Configuration;

namespace Swiss.FCh.MasterData.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IMasterDataClient _masterDataClient;

    public DepartmentService(IMasterDataClient masterDataClient)
    {
        _masterDataClient = masterDataClient;
    }

    public async Task<IEnumerable<Department>> GetDepartments(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var queryResult = await _masterDataClient.GetMasterData(
            DefinedTermsets.Departments,
            ct,
            endpointUrl: SparqlEndpoints.LdAdmin,
            dataModel: DataModels.SchemaOrg).ConfigureAwait(false);

        return queryResult.Select(DepartmentMapper.MapToDepartment);
    }
}
