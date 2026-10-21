namespace Swiss.FCh.MasterData.Exceptions;

public class MasterDataException : Exception
{
    public MasterDataException(string message) : base(message)
    {
    }

    public MasterDataException()
    {
    }

    public MasterDataException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
