using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hair_Care_Store.Models;
using Hair_Care_Store.Repositories.Base;

namespace Hair_Care_Store.Controllers;

public class ErrorController : Controller
{
    [HttpGet("Error/ErrorInfo")]
    public IActionResult ErrorInfo()
    {
        var exceptionMessage = HttpContext.Items["message"] as string;
        int statusCode = HttpContext.Items["statusCode"] as int? ?? 500;

        var model = new ErrorViewModel
        {
            StatusCode = statusCode,
            Message = exceptionMessage ?? "Failed to obtain error message"
        };

        return View(model);
    }
}
