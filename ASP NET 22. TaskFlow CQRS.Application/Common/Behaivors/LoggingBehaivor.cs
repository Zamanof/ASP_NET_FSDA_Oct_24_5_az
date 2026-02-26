using MediatR;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace ASP_NET_22._TaskFlow_CQRS.Application.Common.Behaivors;

public class LoggingBehaivor<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest: IRequest<TResponse>
{

    private static readonly string[] SensitiveNames = { "Password", "RefreshToken", "Token", "Secret" };
    private readonly ILogger<LoggingBehaivor<TRequest, TResponse>> _logger;

    public LoggingBehaivor(ILogger<LoggingBehaivor<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        var safeDescription = GetSafeRequestDescription(request);
        _logger.LogInformation("Handling {RequestName}: {Description}", requestName, safeDescription);
        var start = DateTimeOffset.UtcNow;
        try
        {
            var response = await next();
            var ellapsed = (DateTimeOffset.UtcNow - start).TotalMilliseconds;
            _logger.LogInformation("Handled {RequestName} in {EllapsedMS} ms", requestName, ellapsed);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Handling {RequestName}", requestName);
            throw;
        }
    }

    private static string GetSafeRequestDescription(TRequest request)
    {
        if (request is null) return "(null)";
        var type = request.GetType();
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var parts = new List<string>();
        foreach (var property in properties)
        {
            if(SensitiveNames.Any(s=> property.Name.IndexOf(s, StringComparison.OrdinalIgnoreCase)>= 0))
            {
                parts.Add($"{property.Name}=***");
                continue;
            }
            try
            {
                var value = property.GetValue(request);
                parts.Add($"{property.Name}={value}");
            }
            catch (Exception)
            {
                parts.Add($"{property.Name}=?");
            }
        }
        return string.Join(", ", parts);
    }
}
