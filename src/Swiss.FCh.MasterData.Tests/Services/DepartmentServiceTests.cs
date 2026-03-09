using Swiss.FCh.MasterData.Configuration;
using Swiss.FCh.MasterData.Extensions;
using Swiss.FCh.MasterData.Services;
using Swiss.FCh.Sparql.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Swiss.FCh.MasterData.Tests.Services;

[TestFixture]
internal sealed class DepartmentServiceTests
{
    private DepartmentService _service = null!;

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

        _service = new DepartmentService(new MasterDataClient(factory));
    }

    [TearDown]
    public void TearDown()
    {
        _scope.Dispose();
    }

    [Test]
    public async Task GetDepartments_ShouldReturnData()
    {
        var result = (await _service.GetDepartments(CancellationToken.None)).ToList();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.GreaterThanOrEqualTo(8));

        var bk = result.SingleOrDefault(x => x.Uri == "https://ld.admin.ch/FCh");

        Assert.That(bk, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(bk!.ShortNameDe, Is.EqualTo("BK"));
            Assert.That(bk.ShortNameFr, Is.EqualTo("ChF"));
            Assert.That(bk.ShortNameIt, Is.EqualTo("CaF"));
            Assert.That(bk.NameDe, Is.EqualTo("Bundeskanzlei"));
            Assert.That(bk.NameFr, Is.EqualTo("Chancellerie fédérale"));
            Assert.That(bk.NameIt, Is.EqualTo("Cancelleria federale"));
            Assert.That(bk.StartDate, Is.LessThan(DateOnly.FromDateTime(DateTime.Today)));
        });
    }
}
