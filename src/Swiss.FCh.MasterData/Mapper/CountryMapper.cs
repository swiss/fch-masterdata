using Swiss.FCh.MasterData.Exceptions;
using Swiss.FCh.MasterData.Models;

namespace Swiss.FCh.MasterData.Mapper;

internal static class CountryMapper
{
    internal static Country MapToCountry(Sparql.Models.MasterData masterData)
    {
        if (string.IsNullOrWhiteSpace(masterData.NameDe))
        {
            throw new MasterDataException($"{nameof(Sparql.Models.MasterData.NameDe)} is null or empty. CountryUri={masterData.DefinedTerm}");
        }

        if (string.IsNullOrWhiteSpace(masterData.NameFr))
        {
            throw new MasterDataException($"{nameof(Sparql.Models.MasterData.NameFr)} is null or empty. CountryUri={masterData.DefinedTerm}");
        }

        if (string.IsNullOrWhiteSpace(masterData.NameIt))
        {
            throw new MasterDataException($"{nameof(Sparql.Models.MasterData.NameIt)} is null or empty. CountryUri={masterData.DefinedTerm}");
        }

        if (masterData.AdditionalAttributes is null || masterData.AdditionalAttributes.Count == 0)
        {
            throw new MasterDataException($"{nameof(Sparql.Models.MasterData.AdditionalAttributes)} is null or empty. CountryUri={masterData.DefinedTerm}");
        }

        if (!masterData.AdditionalAttributes.TryGetValue(CantonConstants.ShortNameIdentifier, out var shortName))
        {
            throw new MasterDataException($"{CantonConstants.ShortNameIdentifier} not provided. CountryUri={masterData.DefinedTerm}");
        }

        return new Country
        {
            Uri = masterData.DefinedTerm,
            NameDe = masterData.NameDe,
            NameFr = masterData.NameFr,
            NameIt = masterData.NameIt,
            ShortNameDe = !string.IsNullOrWhiteSpace(shortName) ? shortName : masterData.NameDe,
            ShortNameFr = !string.IsNullOrWhiteSpace(shortName) ? shortName : masterData.NameFr,
            ShortNameIt = !string.IsNullOrWhiteSpace(shortName) ? shortName : masterData.NameIt,
            StartDate = masterData.Start.GetValueOrDefault(DateOnly.MinValue),
            EndDate = masterData.End
        };
    }
}
