using Swiss.FCh.MasterData.Models;

namespace Swiss.FCh.MasterData.Services;

public interface ICountryService
{
    Task<IEnumerable<Country>> GetCountries(CancellationToken ct);
}
