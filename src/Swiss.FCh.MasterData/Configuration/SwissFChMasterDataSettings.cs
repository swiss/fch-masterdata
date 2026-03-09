namespace Swiss.FCh.MasterData.Configuration;

public class SwissFChMasterDataSettings
{
    public static SwissFChMasterDataSettings Default = new();

    public int RequestTimeoutMs { get; init; } = 30000;
    public string? ProxyAddress { get; init; }
    public string? UserName { get; init; }
    public string? Password { get; init; }
}
