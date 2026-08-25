namespace UtrkePasa.Api.Middleware;

public class TooManyRequestsExcetions : Exception
{

    public TooManyRequestsExcetions() : base("previse requestova")
    {
        
    }
}