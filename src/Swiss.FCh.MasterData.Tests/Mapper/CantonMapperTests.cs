using Swiss.FCh.MasterData.Exceptions;
using Swiss.FCh.MasterData.Mapper;
using Swiss.FCh.MasterData.Models;

namespace Swiss.FCh.MasterData.Tests.Mapper;

[TestFixture]
internal sealed class CantonMapperTests
{
    [TestCase(null, null, null)]
    [TestCase("test_name_de", null, null)]
    [TestCase("test_name_de", "test_name_fr", null)]
    [TestCase("", "test_name_fr", "test_name_it")]
    [TestCase("test_name_de", "", "test_name_it")]
    [TestCase("test_name_de", "test_name_fr", "")]
    public void MapToCanton_WithoutNames_ShouldThrowMasterDataException(string? nameDe, string? nameFr, string? nameIt)
    {
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTermSet = "test_term_set",
            DefinedTerm = "test_term",
            NameDe = nameDe,
            NameFr = nameFr,
            NameIt = nameIt
        };

        Assert.Throws<MasterDataException>(() => CantonMapper.MapToCanton(masterData));
    }

    [Test]
    public void MapToCanton_WithoutAdditionalAttributes_ShouldThrowMasterDataException()
    {
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTermSet = "test_term_set",
            DefinedTerm = "test_term",
            NameDe = "test_name_de",
            NameFr = "test_name_fr",
            NameIt = "test_name_it"
        };

        Assert.Throws<MasterDataException>(() => CantonMapper.MapToCanton(masterData));
    }

    [Test]
    public void MapToCanton_WithoutEmptyAdditionalAttributes_ShouldThrowMasterDataException()
    {
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTermSet = "test_term_set",
            DefinedTerm = "test_term",
            NameDe = "test_name_de",
            NameFr = "test_name_fr",
            NameIt = "test_name_it",
            AdditionalAttributes = new Dictionary<string, string?>()
        };

        Assert.Throws<MasterDataException>(() => CantonMapper.MapToCanton(masterData));
    }

    [Test]
    public void MapToCanton_WithoutShortNameSchemaAdditionalAttributes_ShouldThrowMasterDataException()
    {
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTermSet = "test_term_set",
            DefinedTerm = "test_term",
            NameDe = "test_name_de",
            NameFr = "test_name_fr",
            NameIt = "test_name_it",
            AdditionalAttributes = new Dictionary<string, string?> { { "some_identifier", "test_value" } }
        };

        Assert.Throws<MasterDataException>(() => CantonMapper.MapToCanton(masterData));
    }

    [Test]
    public void MapToCanton_WithValidMasterData_ShouldMapCorrectly()
    {
        const string shortName = "test_short_name";
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTerm = "uri",
            DefinedTermSet = "",
            NameDe = "de",
            NameFr = "fr",
            NameIt = "it",
            Start = DateOnly.FromDateTime(DateTime.Today),
            AdditionalAttributes = new Dictionary<string, string?>
            {
                { CantonConstants.ShortNameIdentifier, shortName }
            }
        };

        var canton = CantonMapper.MapToCanton(masterData);

        Assert.That(canton, Is.Not.Null);

        masterData.AdditionalAttributes.TryGetValue(CantonConstants.ShortNameIdentifier, out var alternateName);

        Assert.Multiple(() =>
        {
            Assert.That(canton.Uri, Is.EqualTo(masterData.DefinedTerm));
            Assert.That(canton.NameDe, Is.EqualTo(masterData.NameDe));
            Assert.That(canton.NameFr, Is.EqualTo(masterData.NameFr));
            Assert.That(canton.NameIt, Is.EqualTo(masterData.NameIt));
            Assert.That(canton.ShortNameDe, Is.EqualTo(alternateName));
            Assert.That(canton.ShortNameFr, Is.EqualTo(alternateName));
            Assert.That(canton.ShortNameIt, Is.EqualTo(alternateName));
            Assert.That(canton.StartDate, Is.EqualTo(masterData.Start));
            Assert.That(canton.EndDate, Is.Null);
        });
    }

    [Test]
    public void MapToCanton_WithoutStartDate_ShouldMapToMinDate()
    {
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTermSet = string.Empty,
            DefinedTerm = string.Empty,
            NameDe = "test_name_de",
            NameFr = "test_name_fr",
            NameIt = "test_name_it",
            AdditionalAttributes = new Dictionary<string, string?>
            {
                { CantonConstants.ShortNameIdentifier, "test_short_name" }
            }
        };

        var canton = CantonMapper.MapToCanton(masterData);

        Assert.That(canton, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(canton.StartDate, Is.EqualTo(DateOnly.MinValue));
            Assert.That(canton.EndDate, Is.Null);
        });
    }

    [Test]
    public void MapToCanton_WithoutShortName_ShouldMapShortNameToName()
    {
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTermSet = string.Empty,
            DefinedTerm = string.Empty,
            NameDe = "test_name_de",
            NameFr = "test_name_fr",
            NameIt = "test_name_it",
            AdditionalAttributes = new Dictionary<string, string?>
            {
                { CantonConstants.ShortNameIdentifier, null }
            }
        };

        var canton = CantonMapper.MapToCanton(masterData);

        Assert.That(canton, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(canton.ShortNameDe, Is.EqualTo(masterData.NameDe));
            Assert.That(canton.ShortNameFr, Is.EqualTo(masterData.NameFr));
            Assert.That(canton.ShortNameIt, Is.EqualTo(masterData.NameIt));
        });
    }
}
