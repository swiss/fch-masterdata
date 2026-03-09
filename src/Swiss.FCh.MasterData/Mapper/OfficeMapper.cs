using Swiss.FCh.MasterData.Exceptions;
using Swiss.FCh.MasterData.Models;

namespace Swiss.FCh.MasterData.Mapper;

internal static class OfficeMapper
{
    internal static Office MapToOffice(Sparql.Models.MasterData masterData)
    {
        if (string.IsNullOrWhiteSpace(masterData.NameDe))
        {
            throw new MasterDataException($"{nameof(Sparql.Models.MasterData.NameDe)} is null or empty. OfficeUri={masterData.DefinedTerm}");
        }

        if (string.IsNullOrWhiteSpace(masterData.NameFr))
        {
            throw new MasterDataException($"{nameof(Sparql.Models.MasterData.NameFr)} is null or empty. OfficeUri={masterData.DefinedTerm}");
        }

        if (string.IsNullOrWhiteSpace(masterData.NameIt))
        {
            throw new MasterDataException($"{nameof(Sparql.Models.MasterData.NameIt)} is null or empty. OfficeUri={masterData.DefinedTerm}");
        }

        if (masterData.AdditionalAttributes is null || masterData.AdditionalAttributes.Count == 0)
        {
            throw new MasterDataException($"{nameof(Sparql.Models.MasterData.AdditionalAttributes)} is null or empty. OfficeUri={masterData.DefinedTerm}");
        }

        if (!masterData.AdditionalAttributes.TryGetValue(OfficeConstants.DepartmentUriIdentifier, out var departmentUri))
        {
            throw new MasterDataException($"{OfficeConstants.DepartmentUriIdentifier} not provided. OfficeUri={masterData.DefinedTerm}");
        }

        if (string.IsNullOrWhiteSpace(departmentUri))
        {
            throw new MasterDataException($"{OfficeConstants.DepartmentUriIdentifier} null or empty. OfficeUri={masterData.DefinedTerm}");
        }

        if (!masterData.AdditionalAttributes.TryGetValue(OfficeConstants.GeneralSecretariatIdentifier, out var generalSecretariatUri))
        {
            throw new MasterDataException($"{OfficeConstants.GeneralSecretariatIdentifier} not provided. OfficeUri={masterData.DefinedTerm}");
        }

        if (!masterData.AdditionalAttributes.TryGetValue(OfficeConstants.CentralFederalAdministrationIdentifier, out var centralAdministrationUri))
        {
            throw new MasterDataException($"{OfficeConstants.CentralFederalAdministrationIdentifier} not provided. OfficeUri={masterData.DefinedTerm}");
        }

        return new Office
        {
            Uri = masterData.DefinedTerm,
            Position = masterData.Position,
            NameDe = masterData.NameDe,
            NameFr = masterData.NameFr,
            NameIt = masterData.NameIt,
            ShortNameDe = !string.IsNullOrWhiteSpace(masterData.ShortNameDe) ? masterData.ShortNameDe : masterData.NameDe,
            ShortNameFr = !string.IsNullOrWhiteSpace(masterData.ShortNameFr) ? masterData.ShortNameFr : masterData.NameFr,
            ShortNameIt = !string.IsNullOrWhiteSpace(masterData.ShortNameIt) ? masterData.ShortNameIt : masterData.NameIt,
            StartDate = masterData.Start.GetValueOrDefault(DateOnly.MinValue),
            EndDate = masterData.End,
            DepartmentUri = departmentUri,
            IsGeneralSecretariat = !string.IsNullOrWhiteSpace(generalSecretariatUri) && generalSecretariatUri == OfficeConstants.GeneralSecretariatUri,
            IsCentralFederalAdministration = !string.IsNullOrWhiteSpace(centralAdministrationUri) && centralAdministrationUri == OfficeConstants.CentralFederalAdministrationUri
        };
    }
}
