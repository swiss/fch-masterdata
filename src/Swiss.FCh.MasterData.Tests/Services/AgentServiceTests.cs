using Swiss.FCh.MasterData.Configuration;
using Swiss.FCh.MasterData.Extensions;
using Swiss.FCh.MasterData.Services;
using Swiss.FCh.Sparql.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Swiss.FCh.MasterData.Tests.Services;

[TestFixture]
internal sealed class AgentServiceTests
{
    private AgentService _service = null!;

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

        _service = new AgentService(new AgentClient(factory));
    }

    [TearDown]
    public void TearDown()
    {
        _scope.Dispose();
    }

    [Test]
    public async Task GetAgents_WithValidUris_ShouldReturnData()
    {
        const string nationalCouncilUri = "https://politics.ld.admin.ch/council/N";
        const string councilOfStatesUri = "https://politics.ld.admin.ch/council/S";

        var uris = new List<string> { nationalCouncilUri, councilOfStatesUri };

        var result = (await _service.GetAgents(uris, CancellationToken.None)).ToList();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(2));

        var nationalCouncil = result.SingleOrDefault(r => r.Uri == nationalCouncilUri);
        var councilOfStates = result.SingleOrDefault(r => r.Uri == councilOfStatesUri);

        Assert.That(nationalCouncil, Is.Not.Null);
        Assert.That(nationalCouncil!.TextDe, Is.EqualTo("Nationalrat"));

        Assert.That(councilOfStates, Is.Not.Null);
        Assert.That(councilOfStates!.TextDe, Is.EqualTo("Ständerat"));
    }
}
