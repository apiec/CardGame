namespace CardGame.Shared.Domain.Exceptions;

public abstract class DomainException : Exception
{
    public DomainException(string statusCode)
    {
        StatusCode = statusCode;
    }

    public string StatusCode { get; }
}
