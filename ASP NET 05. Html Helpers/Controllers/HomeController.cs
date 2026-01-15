using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASP_NET_05._Html_Helpers.Models;

namespace ASP_NET_05._Html_Helpers.Controllers;

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

    public IActionResult Privacy(int id, string login, string password)
    {
        return View(new User { Id=id, Login=login, Password=password});
    }
    [HttpPost]
    public IActionResult Privacy(User user)
    {
        return View(user);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
