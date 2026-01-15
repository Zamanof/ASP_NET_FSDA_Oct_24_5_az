using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP_NET_05._ASP_Filters.Filters;

public class ApiKeyQueryFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var isAuthorized = context
            .HttpContext
            .Request
            .Query
            .Any(q => q.Key == "apiKey" && q.Value == "654321");
        if (!isAuthorized)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
