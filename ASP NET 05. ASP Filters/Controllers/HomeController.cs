using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASP_NET_05._ASP_Filters.Models;
using ASP_NET_05._ASP_Filters.Filters;

namespace ASP_NET_05._ASP_Filters.Controllers;
//[TypeFilter(typeof(ApiKeyQueryFilter))]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    [LastEnterDate]
    public IActionResult Privacy()
    {
        
        int a = 0;

        return View(35/a);
    }

    [TypeFilter(typeof(MyAuthorizationFilter))]
    public IActionResult Welcome()
    {
        //throw new KeyNotFoundException();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
