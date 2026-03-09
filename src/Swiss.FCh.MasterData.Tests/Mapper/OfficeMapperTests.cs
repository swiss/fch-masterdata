using Swiss.FCh.MasterData.Exceptions;
using Swiss.FCh.MasterData.Mapper;
using Swiss.FCh.MasterData.Models;

namespace Swiss.FCh.MasterData.Tests.Mapper;

[TestFixture]
internal sealed class OfficeMapperTests
{
    [TestCase(null, null, null)]
    [TestCase("test_name_de", null, null)]
    [TestCase("test_name_de", "test_name_fr", null)]
    [TestCase("", "test_name_fr", "test_name_it")]
    [TestCase("test_name_de", "", "test_name_it")]
    [TestCase("test_name_de", "test_name_fr", "")]
    public void MapToOffice_WithoutNames_ShouldThrowMasterDataException(string? nameDe, string? nameFr, string? nameIt)
    {
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTermSet = "test_term_set",
            DefinedTerm = "test_term",
            NameDe = nameDe,
            NameFr = nameFr,
            NameIt = nameIt
        };

        Assert.Throws<MasterDataException>(() => OfficeMapper.MapToOffice(masterData));
    }

    [Test]
    public void MapToOffice_WithoutAdditionalAttributes_ShouldThrowMasterDataException()
    {
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTermSet = "test_term_set",
            DefinedTerm = "test_term",
            NameDe = "test_name_de",
            NameFr = "test_name_fr",
            NameIt = "test_name_it"
        };

        Assert.Throws<MasterDataException>(() => OfficeMapper.MapToOffice(masterData));
    }

    [Test]
    public void MapToOffice_WithoutEmptyAdditionalAttributes_ShouldThrowMasterDataException()
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

        Assert.Throws<MasterDataException>(() => OfficeMapper.MapToOffice(masterData));
    }

    [Test]
    public void MapToOffice_WithoutDepartmentUriAdditionalAttributes_ShouldThrowMasterDataException()
    {
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTermSet = "test_term_set",
            DefinedTerm = "test_term",
            NameDe = "test_name_de",
            NameFr = "test_name_fr",
            NameIt = "test_name_it",
            AdditionalAttributes = new Dictionary<string, string?> { { OfficeConstants.GeneralSecretariatIdentifier, "test_value" } }
        };

        Assert.Throws<MasterDataException>(() => OfficeMapper.MapToOffice(masterData));
    }

    [Test]
    public void MapToOffice_WithEmptyDepartmentUriAdditionalAttributes_ShouldThrowMasterDataException()
    {
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTermSet = "test_term_set",
            DefinedTerm = "test_term",
            NameDe = "test_name_de",
            NameFr = "test_name_fr",
            NameIt = "test_name_it",
            AdditionalAttributes = new Dictionary<string, string?> { { OfficeConstants.DepartmentUriIdentifier, "" } }
        };

        Assert.Throws<MasterDataException>(() => OfficeMapper.MapToOffice(masterData));
    }

    [Test]
    public void MapToOffice_WithoutGeneralSecretariatAdditionalAttributes_ShouldThrowMasterDataException()
    {
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTermSet = "test_term_set",
            DefinedTerm = "test_term",
            NameDe = "test_name_de",
            NameFr = "test_name_fr",
            NameIt = "test_name_it",
            AdditionalAttributes = new Dictionary<string, string?>
            {
                { OfficeConstants.DepartmentUriIdentifier, "test_department_uri" },
                { OfficeConstants.CentralFederalAdministrationIdentifier, "test_central_administration_uri" }
            }
        };

        Assert.Throws<MasterDataException>(() => OfficeMapper.MapToOffice(masterData));
    }

    [Test]
    public void MapToOffice_WithoutCentralAdministrationAdditionalAttributes_ShouldThrowMasterDataException()
    {
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTermSet = "test_term_set",
            DefinedTerm = "test_term",
            NameDe = "test_name_de",
            NameFr = "test_name_fr",
            NameIt = "test_name_it",
            AdditionalAttributes = new Dictionary<string, string?>
            {
                { OfficeConstants.DepartmentUriIdentifier, "test_department_uri" },
                { OfficeConstants.GeneralSecretariatIdentifier, "test_general_secretariat_uri" }
            }
        };

        Assert.Throws<MasterDataException>(() => OfficeMapper.MapToOffice(masterData));
    }

    [Test]
    public void MapToOffice_WithValidMasterData_ShouldMapCorrectly()
    {
        const string departmentUri = "test_department_uri";
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
            Start = DateOnly.FromDateTime(DateTime.Today),
            AdditionalAttributes = new Dictionary<string, string?>
            {
                { OfficeConstants.DepartmentUriIdentifier, departmentUri },
                { OfficeConstants.GeneralSecretariatIdentifier, OfficeConstants.GeneralSecretariatUri },
                { OfficeConstants.CentralFederalAdministrationIdentifier, OfficeConstants.DecentralisedFederalAdministrationUri }
            }
        };

        var office = OfficeMapper.MapToOffice(masterData);

        Assert.That(office, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(office.Uri, Is.EqualTo(masterData.DefinedTerm));
            Assert.That(office.Position, Is.EqualTo(masterData.Position));
            Assert.That(office.NameDe, Is.EqualTo(masterData.NameDe));
            Assert.That(office.NameFr, Is.EqualTo(masterData.NameFr));
            Assert.That(office.NameIt, Is.EqualTo(masterData.NameIt));
            Assert.That(office.ShortNameDe, Is.EqualTo(masterData.ShortNameDe));
            Assert.That(office.ShortNameFr, Is.EqualTo(masterData.ShortNameFr));
            Assert.That(office.ShortNameIt, Is.EqualTo(masterData.ShortNameIt));
            Assert.That(office.DepartmentUri, Is.EqualTo(departmentUri));
            Assert.That(office.IsGeneralSecretariat, Is.True);
            Assert.That(office.IsCentralFederalAdministration, Is.False);
            Assert.That(office.StartDate, Is.EqualTo(masterData.Start));
            Assert.That(office.EndDate, Is.Null);
        });
    }

    [Test]
    public void MapToOffice_WithoutStartDate_ShouldMapToMinDate()
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
                { OfficeConstants.DepartmentUriIdentifier, "test_department_uri" },
                { OfficeConstants.GeneralSecretariatIdentifier, OfficeConstants.GeneralSecretariatUri },
                { OfficeConstants.CentralFederalAdministrationIdentifier, OfficeConstants.DecentralisedFederalAdministrationUri }
            }
        };

        var office = OfficeMapper.MapToOffice(masterData);

        Assert.That(office, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(office.StartDate, Is.EqualTo(DateOnly.MinValue));
            Assert.That(office.EndDate, Is.Null);
        });
    }

    [Test]
    public void MapToOffice_WithoutShortName_ShouldMapShortNameToName()
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
                { OfficeConstants.DepartmentUriIdentifier, "test_department_uri" },
                { OfficeConstants.GeneralSecretariatIdentifier, OfficeConstants.GeneralSecretariatUri },
                { OfficeConstants.CentralFederalAdministrationIdentifier, OfficeConstants.CentralFederalAdministrationUri }
            }
        };

        var office = OfficeMapper.MapToOffice(masterData);

        Assert.That(office, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(office.ShortNameDe, Is.EqualTo(masterData.NameDe));
            Assert.That(office.ShortNameFr, Is.EqualTo(masterData.NameFr));
            Assert.That(office.ShortNameIt, Is.EqualTo(masterData.NameIt));
        });
    }
}
