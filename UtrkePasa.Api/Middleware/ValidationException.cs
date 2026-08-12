namespace UtrkePasa.Api.Middleware;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
    }
}