using Swiss.FCh.MasterData.Configuration;
using Swiss.FCh.MasterData.Extensions;
using Swiss.FCh.MasterData.Services;
using Swiss.FCh.Sparql.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Swiss.FCh.MasterData.Tests.Services;

[TestFixture]
internal sealed class CantonServiceTests
{
    private CantonService _service = null!;

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

        _service = new CantonService(new MasterDataClient(factory));
    }

    [TearDown]
    public void TearDown()
    {
        _scope.Dispose();
    }

    [Test]
    public async Task GetCantons_ShouldReturnData()
    {
        var result = (await _service.GetCantons(CancellationToken.None).ConfigureAwait(false)).ToList();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.GreaterThanOrEqualTo(8));

        var zurich = result.SingleOrDefault(x => x.Uri == "https://ld.admin.ch/canton/1");

        Assert.That(zurich, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(zurich.NameDe, Is.EqualTo("Zürich"));
            Assert.That(zurich.NameFr, Is.EqualTo("Zurich"));
            Assert.That(zurich.NameIt, Is.EqualTo("Zurigo"));
            Assert.That(zurich.ShortNameDe, Is.EqualTo("ZH"));
            Assert.That(zurich.ShortNameFr, Is.EqualTo("ZH"));
            Assert.That(zurich.ShortNameIt, Is.EqualTo("ZH"));
            Assert.That(zurich.StartDate, Is.LessThan(DateOnly.FromDateTime(DateTime.Today)));
        });
    }
}
