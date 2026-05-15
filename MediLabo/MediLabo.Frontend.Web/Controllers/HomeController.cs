using System.Diagnostics;
using MediLabo.Frontend.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediLabo.Frontend.Web.Controllers;

public class HomeController : Controller
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}