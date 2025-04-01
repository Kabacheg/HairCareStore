using System.Net;
using Hair_Care_Store.Core.Repositories;
using Hair_Care_Store.Core.Responses;
using HairCareStore.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authorization;
using HairCareStore.Core.Models;
using System.Security.Claims;

namespace HairCareStore.Presentation.Controllers;

[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(NotFoundResponse))]
[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResponse))]
[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponse))]
[ProducesResponseType(200)]
public class StoreController : Controller
{
    private readonly IProductRepository productRepository;
    private readonly IHttpLogger logger;
    private readonly IDataProtector dataProtector;

    public StoreController(IProductRepository productRepository, IHttpLogger logger, IDataProtectionProvider dataProtectionProvider)
    {
        this.productRepository = productRepository;
        this.logger = logger;
        this.dataProtector = dataProtectionProvider.CreateProtector("Identity");

    }


    [HttpGet("[action]")]
    [Authorize]
    public ActionResult ProductsStore()
    {   
        ViewBag.ShowMenu = false;
        ViewBag.ShowProductMenu = true;
        return base.View(productRepository.GetAllProducts().ToList());
    }

    [HttpGet("[action]")]
    [Authorize]
    public ActionResult Cart(){
        ViewBag.ShowMenu = false;
        ViewBag.ShowProductMenu = true;
        return base.View();
    }
    
    [HttpGet("[action]")]
    [Authorize]
    public ActionResult UserInfo(){
        ViewBag.ShowMenu = false;
        ViewBag.ShowProductMenu = true;

        var user = new User {
            Id = int.Parse(base.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value!),
            Name = base.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value!,
            Surname = base.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Surname)?.Value!,
            Mail = base.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value!,
        };

        return base.View(user);
    }

    [HttpGet("[action]")]
    [Authorize]
    public ActionResult TutorialsToRead(){
        ViewBag.ShowMenu = false;
        ViewBag.ShowProductMenu = true;

        return base.View();
    }
}
