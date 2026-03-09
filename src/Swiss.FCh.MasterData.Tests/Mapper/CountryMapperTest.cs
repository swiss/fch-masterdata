using Swiss.FCh.MasterData.Exceptions;
using Swiss.FCh.MasterData.Mapper;
using Swiss.FCh.MasterData.Models;

namespace Swiss.FCh.MasterData.Tests.Mapper;

[TestFixture]
internal sealed class CountryMapperTest
{
    [TestCase(null, null, null)]
    [TestCase("test_name_de", null, null)]
    [TestCase("test_name_de", "test_name_fr", null)]
    [TestCase("", "test_name_fr", "test_name_it")]
    [TestCase("test_name_de", "", "test_name_it")]
    [TestCase("test_name_de", "test_name_fr", "")]
    public void MapToCountry_WithoutNames_ShouldThrowMasterDataException(string? nameDe, string? nameFr, string? nameIt)
    {
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTermSet = "test_term_set",
            DefinedTerm = "test_term",
            NameDe = nameDe,
            NameFr = nameFr,
            NameIt = nameIt
        };

        Assert.Throws<MasterDataException>(() => CountryMapper.MapToCountry(masterData));
    }

    [Test]
    public void MapToCountry_WithoutAdditionalAttributes_ShouldThrowMasterDataException()
    {
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTermSet = "test_term_set",
            DefinedTerm = "test_term",
            NameDe = "test_name_de",
            NameFr = "test_name_fr",
            NameIt = "test_name_it"
        };

        Assert.Throws<MasterDataException>(() => CountryMapper.MapToCountry(masterData));
    }

    [Test]
    public void MapToCountry_WithoutEmptyAdditionalAttributes_ShouldThrowMasterDataException()
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

        Assert.Throws<MasterDataException>(() => CountryMapper.MapToCountry(masterData));
    }

    [Test]
    public void MapToCountry_WithoutShortNameSchemaAdditionalAttributes_ShouldThrowMasterDataException()
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

        Assert.Throws<MasterDataException>(() => CountryMapper.MapToCountry(masterData));
    }

    [Test]
    public void MapToCountry_WithValidMasterData_ShouldMapCorrectly()
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
                { Country.CountryConstants.ShortNameIdentifier, shortName }
            }
        };

        var country = CountryMapper.MapToCountry(masterData);

        Assert.That(country, Is.Not.Null);

        masterData.AdditionalAttributes.TryGetValue(Country.CountryConstants.ShortNameIdentifier, out var alternateName);

        Assert.Multiple(() =>
        {
            Assert.That(country.Uri, Is.EqualTo(masterData.DefinedTerm));
            Assert.That(country.NameDe, Is.EqualTo(masterData.NameDe));
            Assert.That(country.NameFr, Is.EqualTo(masterData.NameFr));
            Assert.That(country.NameIt, Is.EqualTo(masterData.NameIt));
            Assert.That(country.ShortNameDe, Is.EqualTo(alternateName));
            Assert.That(country.ShortNameFr, Is.EqualTo(alternateName));
            Assert.That(country.ShortNameIt, Is.EqualTo(alternateName));
            Assert.That(country.StartDate, Is.EqualTo(masterData.Start));
            Assert.That(country.EndDate, Is.Null);
        });
    }

    [Test]
    public void MapToCountry_WithoutStartDate_ShouldMapToMinDate()
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
                { Country.CountryConstants.ShortNameIdentifier, "test_short_name" }
            }
        };

        var country = CountryMapper.MapToCountry(masterData);

        Assert.That(country, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(country.StartDate, Is.EqualTo(DateOnly.MinValue));
            Assert.That(country.EndDate, Is.Null);
        });
    }

    [Test]
    public void MapToCountry_WithoutShortName_ShouldMapShortNameToName()
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
                { Country.CountryConstants.ShortNameIdentifier, null }
            }
        };

        var country = CountryMapper.MapToCountry(masterData);

        Assert.That(country, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(country.ShortNameDe, Is.EqualTo(masterData.NameDe));
            Assert.That(country.ShortNameFr, Is.EqualTo(masterData.NameFr));
            Assert.That(country.ShortNameIt, Is.EqualTo(masterData.NameIt));
        });
    }
}
