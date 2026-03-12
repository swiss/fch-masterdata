using Swiss.FCh.MasterData.Configuration;
using Swiss.FCh.MasterData.Extensions;
using Swiss.FCh.MasterData.Services;
using Swiss.FCh.Sparql.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Swiss.FCh.MasterData.Tests.Services;

[TestFixture]
internal sealed class CountryServiceTests
{
    private CountryService _service = null!;

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

        _service = new CountryService(new MasterDataClient(factory));
    }

    [TearDown]
    public void TearDown()
    {
        _scope.Dispose();
    }

    [Test]
    public async Task GetCountries_ShouldReturnData()
    {
        var result = (await _service.GetCountries(CancellationToken.None).ConfigureAwait(false)).ToList();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.GreaterThanOrEqualTo(8));

        var liechtenstein = result.SingleOrDefault(x => x.Uri == "https://ld.admin.ch/country/LIE");

        Assert.That(liechtenstein, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(liechtenstein.NameDe, Is.EqualTo("Liechtenstein"));
            Assert.That(liechtenstein.NameFr, Is.EqualTo("Liechtenstein"));
            Assert.That(liechtenstein.NameIt, Is.EqualTo("Liechtenstein"));
            Assert.That(liechtenstein.ShortNameDe, Is.EqualTo("LI"));
            Assert.That(liechtenstein.ShortNameFr, Is.EqualTo("LI"));
            Assert.That(liechtenstein.ShortNameIt, Is.EqualTo("LI"));
            Assert.That(liechtenstein.StartDate, Is.LessThan(DateOnly.FromDateTime(DateTime.Today)));
        });
    }
}
