using Swiss.FCh.MasterData.Configuration;
using Swiss.FCh.MasterData.Extensions;
using Swiss.FCh.MasterData.Services;
using Swiss.FCh.Sparql.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Swiss.FCh.MasterData.Tests.Services;

[TestFixture]
internal sealed class CommitteeServiceTests
{
    private CommitteeService _service = null!;

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

        _service = new CommitteeService(new MasterDataClient(factory));
    }

    [TearDown]
    public void TearDown()
    {
        _scope.Dispose();
    }

    [Test]
    public async Task GetCommittees_ShouldReturnData()
    {
        var result = (await _service.GetCommittees(CancellationToken.None)).ToList();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.GreaterThan(100));

        var example = result.SingleOrDefault(c => c.Uri == "https://politics.ld.admin.ch/council/committee/17");

        Assert.That(example, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(example!.NameDe, Is.EqualTo("Aussenpolitische Kommission Ständerat"));
            Assert.That(example.NameFr, Is.Not.Empty);
            Assert.That(example.NameIt, Is.Not.Empty);
        });
    }
}
