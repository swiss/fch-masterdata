using Swiss.FCh.MasterData.Mapper;

namespace Swiss.FCh.MasterData.Tests.Mapper;

[TestFixture]
internal sealed class SessionMapperTests
{
    [Test]
    public void MapToSession_WithValidMasterData_ShouldMapCorrectly()
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

        var session = SessionMapper.MapToSession(masterData);

        Assert.That(session, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(session.Uri, Is.EqualTo("uri"));
            Assert.That(session.NameDe, Is.EqualTo("de"));
            Assert.That(session.NameFr, Is.EqualTo("fr"));
            Assert.That(session.NameIt, Is.EqualTo("it"));
            Assert.That(session.StartDate, Is.EqualTo(new DateOnly(2000, 1, 1)));
            Assert.That(session.EndDate, Is.EqualTo(new DateOnly(2000, 2, 2)));
        });
    }
}
