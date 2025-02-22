using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hair_Care_Store.Models;
using Hair_Care_Store.Repositories.Base;

namespace Hair_Care_Store.Controllers;

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
