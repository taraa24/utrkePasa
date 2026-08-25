namespace UtrkePasa.Api.Services;


[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class RequestLimitAttribute : Attribute
{

    public int Limit {get;}

    public RequestLimitAttribute(int limit)
    {
        Limit = limit;
    }

}