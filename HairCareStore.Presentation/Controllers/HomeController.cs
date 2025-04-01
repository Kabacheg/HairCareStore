using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hair_Care_Store.Core.Models;
using Hair_Care_Store.Core.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Hair_Care_Store.Presentation.Controllers;



public class HomeController : Controller
{

    public IActionResult Index()
    {
        ViewBag.ShowMenu = false;
        ViewBag.ShowFooter = false;
        return View();
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "Admin")]
    
    public IActionResult Products()
    {
        return View();
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "Admin")]
    public IActionResult Tutorials()
    {
        ViewBag.ShowFooter = false;
        return View();
    }
}
