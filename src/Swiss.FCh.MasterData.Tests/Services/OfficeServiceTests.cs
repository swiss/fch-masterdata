using Swiss.FCh.MasterData.Configuration;
using Swiss.FCh.MasterData.Extensions;
using Swiss.FCh.MasterData.Services;
using Swiss.FCh.Sparql.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Swiss.FCh.MasterData.Tests.Services;

[TestFixture]
internal sealed class OfficeServiceTests
{
    private OfficeService _service = null!;

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

        _service = new OfficeService(new MasterDataClient(factory));
    }

    [TearDown]
    public void TearDown()
    {
        _scope.Dispose();
    }

    [Test]
    public async Task GetOffices_ShouldReturnData()
    {
        var result = (await _service.GetOffices(CancellationToken.None).ConfigureAwait(false)).ToList();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.GreaterThan(50));

        var exampleCentral = result.SingleOrDefault(o => o.Uri == "https://ld.admin.ch/office/II.1.2");

        Assert.That(exampleCentral, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(exampleCentral!.NameDe, Is.EqualTo("Eidgenössisches Büro für die Gleichstellung von Frau und Mann"));
            Assert.That(exampleCentral.Position, Is.EqualTo("2.1.02"));
            Assert.That(exampleCentral.NameFr, Is.Not.Empty);
            Assert.That(exampleCentral.NameIt, Is.Not.Empty);
            Assert.That(exampleCentral.ShortNameDe, Is.EqualTo("EBG"));
            Assert.That(exampleCentral.ShortNameFr, Is.Not.Empty);
            Assert.That(exampleCentral.ShortNameIt, Is.Not.Empty);
            Assert.That(exampleCentral.StartDate, Is.EqualTo(new DateOnly(2025, 11, 6)));
            Assert.That(exampleCentral.EndDate, Is.Null);
            Assert.That(exampleCentral.DepartmentUri, Is.EqualTo("https://ld.admin.ch/department/II"));
            Assert.That(exampleCentral.IsGeneralSecretariat, Is.False);
            Assert.That(exampleCentral.IsCentralFederalAdministration, Is.True);
        });

        var exampleGs = result.SingleOrDefault(o => o.Uri == "https://ld.admin.ch/office/I.1.1");

        Assert.That(exampleGs, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(exampleGs!.NameDe, Is.EqualTo("Generalsekretariat"));
            Assert.That(exampleGs.Position, Is.EqualTo("1.1.01"));
            Assert.That(exampleGs.NameFr, Is.Not.Empty);
            Assert.That(exampleGs.NameIt, Is.Not.Empty);
            Assert.That(exampleGs.ShortNameDe, Is.EqualTo("GS-EDA"));
            Assert.That(exampleGs.ShortNameFr, Is.Not.Empty);
            Assert.That(exampleGs.ShortNameIt, Is.Not.Empty);
            Assert.That(exampleGs.StartDate, Is.EqualTo(new DateOnly(2025, 10, 18)));
            Assert.That(exampleGs.EndDate, Is.Null);
            Assert.That(exampleGs.DepartmentUri, Is.EqualTo("https://ld.admin.ch/department/I"));
            Assert.That(exampleGs.IsGeneralSecretariat, Is.True);
            Assert.That(exampleGs.IsCentralFederalAdministration, Is.True);
        });

        var exampleDecentralised = result.SingleOrDefault(o => o.Uri == "https://ld.admin.ch/office/III.2.2.2");

        Assert.That(exampleDecentralised, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(exampleDecentralised!.NameDe, Is.EqualTo("Eidgenössisches Institut für Geistiges Eigentum"));
            Assert.That(exampleDecentralised.Position, Is.EqualTo("3.2.02.02"));
            Assert.That(exampleDecentralised.NameFr, Is.Not.Empty);
            Assert.That(exampleDecentralised.NameIt, Is.Not.Empty);
            Assert.That(exampleDecentralised.ShortNameDe, Is.EqualTo("IGE"));
            Assert.That(exampleDecentralised.ShortNameFr, Is.Not.Empty);
            Assert.That(exampleDecentralised.ShortNameIt, Is.Not.Empty);
            Assert.That(exampleDecentralised.StartDate, Is.EqualTo(new DateOnly(2025, 10, 18)));
            Assert.That(exampleDecentralised.EndDate, Is.Null);
            Assert.That(exampleDecentralised.DepartmentUri, Is.EqualTo("https://ld.admin.ch/department/III"));
            Assert.That(exampleDecentralised.IsGeneralSecretariat, Is.False);
            Assert.That(exampleDecentralised.IsCentralFederalAdministration, Is.False);
        });
    }
}
