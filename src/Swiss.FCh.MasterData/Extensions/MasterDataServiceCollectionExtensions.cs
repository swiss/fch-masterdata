using System.Net;
using Swiss.FCh.MasterData.Configuration;
using Swiss.FCh.MasterData.Services;
using Swiss.FCh.Sparql;
using Swiss.FCh.Sparql.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Swiss.FCh.MasterData.Extensions;

public static class MasterDataServiceCollectionExtensions
{
    public static void AddMasterDataService(this IServiceCollection services)
    {
        services.AddMasterDataService(SwissFChMasterDataSettings.Default);
    }

    public static void AddMasterDataService(
        this IServiceCollection services,
        SwissFChMasterDataSettings settings)
    {
        services.AddSparqlClient();

        services.AddHttpClient(SwissFChSparqlConstants.SparqlHttpClientName)
            .ConfigureHttpClient(httpClient =>
            {
                httpClient.Timeout = TimeSpan.FromMinutes(settings.RequestTimeoutMs);
            })
            .ConfigurePrimaryHttpMessageHandler(
                () =>
                {
                    var handler = new HttpClientHandler();

                    if (!string.IsNullOrWhiteSpace(settings.UserName))
                    {
                        handler.Credentials = new NetworkCredential(settings.UserName, settings.Password);
                    }

                    if (!string.IsNullOrWhiteSpace(settings.ProxyAddress))
                    {
                        handler.Proxy = new WebProxy(settings.ProxyAddress, false);
                        handler.UseProxy = true;
                    }

                    return handler;
                });

        services.AddTransient<IAgentService, AgentService>();
        services.AddTransient<ICommitteeService, CommitteeService>();
        services.AddTransient<ISessionService, SessionService>();
        services.AddTransient<IOfficeService, OfficeService>();
        services.AddTransient<IDepartmentService, DepartmentService>();
        services.AddTransient<ICantonService, CantonService>();
        services.AddTransient<ICountryService, CountryService>();
    }
}
