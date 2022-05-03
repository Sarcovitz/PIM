namespace PimApi.Exceptions;

public class KeyAlreadyExistsException : Exception
{
    public KeyAlreadyExistsException(string message) : base(message) { }
}
