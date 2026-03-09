using Swiss.FCh.MasterData.Mapper;

namespace Swiss.FCh.MasterData.Tests.Mapper;

[TestFixture]
internal sealed class AgentMapperTests
{
    [Test]
    public void MapToAgent_WithValidMasterData_ShouldMapCorrectly()
    {
        var masterData = new Swiss.FCh.Sparql.Models.MasterData
        {
            DefinedTerm = "uri",
            DefinedTermSet = "",
            NameDe = "de",
            NameFr = "fr",
            NameIt = "it"
        };

        var agent = AgentMapper.MapToAgent(masterData);

        Assert.That(agent, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(agent.Uri, Is.EqualTo("uri"));
            Assert.That(agent.TextDe, Is.EqualTo("de"));
            Assert.That(agent.TextFr, Is.EqualTo("fr"));
            Assert.That(agent.TextIt, Is.EqualTo("it"));
        });
    }
}
