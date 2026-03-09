using Swiss.FCh.MasterData.Exceptions;
using Swiss.FCh.MasterData.Mapper;

namespace Swiss.FCh.MasterData.Tests.Mapper;

[TestFixture]
internal sealed class DepartmentMapperTests
{
    [TestCase(null, null, null)]
    [TestCase("test_name_de", null, null)]
    [TestCase("test_name_de", "test_name_fr", null)]
    [TestCase("", "test_name_fr", "test_name_it")]
    [TestCase("test_name_de", "", "test_name_it")]
    [TestCase("test_name_de", "test_name_fr", "")]
    public void MapToDepartment_WithoutNames_ShouldThrowMasterDataException(string? nameDe, string? nameFr, string? nameIt)
    {
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTermSet = "test_term_set",
            DefinedTerm = "test_term",
            NameDe = nameDe,
            NameFr = nameFr,
            NameIt = nameIt
        };

        Assert.Throws<MasterDataException>(() => DepartmentMapper.MapToDepartment(masterData));
    }

    [Test]
    public void MapToDepartment_WithValidMasterData_ShouldMapCorrectly()
    {
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTerm = "uri",
            DefinedTermSet = "",
            Position = "test_position",
            NameDe = "de",
            NameFr = "fr",
            NameIt = "it",
            ShortNameDe = "sde",
            ShortNameFr = "sfr",
            ShortNameIt = "sit",
            Start = DateOnly.FromDateTime(DateTime.Today)
        };

        var department = DepartmentMapper.MapToDepartment(masterData);

        Assert.That(department, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(department.Uri, Is.EqualTo(masterData.DefinedTerm));
            Assert.That(department.Position, Is.EqualTo(masterData.Position));
            Assert.That(department.NameDe, Is.EqualTo(masterData.NameDe));
            Assert.That(department.NameFr, Is.EqualTo(masterData.NameFr));
            Assert.That(department.NameIt, Is.EqualTo(masterData.NameIt));
            Assert.That(department.ShortNameDe, Is.EqualTo(masterData.ShortNameDe));
            Assert.That(department.ShortNameFr, Is.EqualTo(masterData.ShortNameFr));
            Assert.That(department.ShortNameIt, Is.EqualTo(masterData.ShortNameIt));
            Assert.That(department.StartDate, Is.EqualTo(masterData.Start));
            Assert.That(department.EndDate, Is.Null);
        });
    }
}
