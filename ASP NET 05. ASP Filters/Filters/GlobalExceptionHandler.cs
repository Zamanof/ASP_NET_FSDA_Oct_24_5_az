using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP_NET_05._ASP_Filters.Filters;

public class GlobalExceptionHandler : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var ex = context.Exception;
        if (ex is KeyNotFoundException || ex is NullReferenceException)
        {
            context.Result = new LocalRedirectResult("/home/error");
        }
        else if(ex is DivideByZeroException)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status418ImATeapot);
        }
    }
}
