using Swiss.FCh.MasterData.Models;

namespace Swiss.FCh.MasterData.Services;

public interface IDepartmentService
{
    Task<IEnumerable<Department>> GetDepartments(CancellationToken ct);
}
