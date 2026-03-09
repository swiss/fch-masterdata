using Swiss.FCh.MasterData.Configuration;
using Swiss.FCh.MasterData.Extensions;
using Swiss.FCh.MasterData.Services;
using Swiss.FCh.Sparql.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Swiss.FCh.MasterData.Tests.Services;

[TestFixture]
internal sealed class SessionServiceTests
{
    private SessionService _service = null!;

    private IServiceScope _scope;

    [SetUp]
    public void SetUp()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddMasterDataService(new SwissFChMasterDataSettings
        {
            //set proxy here, if needed
        });

        _scope = serviceCollection.BuildServiceProvider().CreateScope();
        var factory = _scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();

        _service = new SessionService(new MasterDataClient(factory));
    }

    [TearDown]
    public void TearDown()
    {
        _scope.Dispose();
    }

    [Test]
    public async Task GetSessions_ShouldReturnData()
    {
        var result = (await _service.GetSessions(CancellationToken.None)).ToList();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.GreaterThan(100));

        var fallSession23 = result.SingleOrDefault(s => s.Uri == "https://politics.ld.admin.ch/vocabulary/session/2023-09-11");

        Assert.That(fallSession23, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(fallSession23!.NameDe, Is.EqualTo("Herbstsession 2023"));
            Assert.That(fallSession23.NameFr, Is.Not.Empty);
            Assert.That(fallSession23.NameIt, Is.Not.Empty);
            Assert.That(fallSession23.StartDate.Year, Is.EqualTo(2023));
            Assert.That(fallSession23.EndDate.Year, Is.EqualTo(2023));
        });
    }
}
