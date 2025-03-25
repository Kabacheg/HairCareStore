using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hair_Care_Store.Core.Models;
using Hair_Care_Store.Core.Repositories;

namespace Hair_Care_Store.Presentation.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Tutorials()
    {
        ViewBag.ShowFooter = false;
        return View();
    }
}
