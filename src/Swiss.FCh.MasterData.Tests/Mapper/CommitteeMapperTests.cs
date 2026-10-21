using Swiss.FCh.MasterData.Mapper;

namespace Swiss.FCh.MasterData.Tests.Mapper;

[TestFixture]
internal sealed class CommitteeMapperTests
{
    [Test]
    public void MapToCommittee_WithValidMasterData_ShouldMapCorrectly()
    {
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTerm = "uri",
            DefinedTermSet = "",
            NameDe = "de",
            NameFr = "fr",
            NameIt = "it",
            Start = new DateOnly(2000, 1, 1),
            End = new DateOnly(2000, 2, 2)
        };

        var committee = CommitteeMapper.MapToCommittee(masterData);

        Assert.That(committee, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(committee.Uri, Is.EqualTo("uri"));
            Assert.That(committee.NameDe, Is.EqualTo("de"));
            Assert.That(committee.NameFr, Is.EqualTo("fr"));
            Assert.That(committee.NameIt, Is.EqualTo("it"));
            Assert.That(committee.StartDate, Is.EqualTo(new DateOnly(2000, 1, 1)));
            Assert.That(committee.EndDate, Is.EqualTo(new DateOnly(2000, 2, 2)));
        });
    }
}
