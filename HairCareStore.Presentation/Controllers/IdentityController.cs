using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using Hair_Care_Store.Core.Models;
using Hair_Care_Store.Core.Repositories;
using Hair_Care_Store.Core.Responses;
using HairCareStore.Core.Dtos;
using HairCareStore.Core.Models;
using HairCareStore.Core.Services;
using HairCareStore.Core.Validators;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ValidationException = Hair_Care_Store.Core.Excpetions.ValidationException;

namespace HairCareStore.Presentation.Controllers;

[Route("[controller]/")]
[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(NotFoundResponse))]
[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResponse))]
[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponse))]
[ProducesResponseType(200)]
public class IdentityController : Controller
{

    private readonly IUserRepository userRepository;
    private readonly IHttpLogger logger;
    private readonly UserValidator userValidator;
    private readonly IDataProtector dataProtector;


    public IdentityController(IUserRepository userRepository, IHttpLogger logger, UserValidator userValidator, IDataProtectionProvider dataProtectionProvider)
    {
        this.userRepository = userRepository;
        this.logger = logger;
        this.userValidator = userValidator;
        this.dataProtector = dataProtectionProvider.CreateProtector("Identity");
    }

    [HttpGet("[action]")]
    public ActionResult Registration()
    {
        ViewBag.ShowMenu = false;
        return base.View();
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> Registration([FromForm] UserDetailsForRegistration userDetailsForRegistration)
    {
        if (!ModelState.IsValid) 
        {
            return View(userDetailsForRegistration);
        }

            var newUser = new User
        {
            Name = userDetailsForRegistration.Name,
            Surname = userDetailsForRegistration.Surname,
            Mail = userDetailsForRegistration.Mail,
            Password = userDetailsForRegistration.Password
        };

        var UserAvatar = userDetailsForRegistration.UserAvatar;
        var extension = new FileInfo(UserAvatar.FileName).Extension[1..];
        var userMailForPath = newUser.Mail;
        string filepath = $"wwwroot/UserAvatars/{userMailForPath}.{extension}";

        using var avatarFileStream = System.IO.File.Create(filepath);
        await UserAvatar.CopyToAsync(avatarFileStream);

        userRepository.AddUser(newUser);
        return base.RedirectToAction(nameof(Login));
    }

    [HttpGet("[action]")]
    public ActionResult Login()
    {   
        ViewBag.ShowMenu = false;
        return base.View();
    }


    [HttpPost("[action]")]
    public async  Task<ActionResult> Login([FromForm] UserDetailsForLoginDto dto)
    {


        var foundUser = userRepository.GetAllUsers().FirstOrDefault(u => u.Mail == dto.Mail && u.Password == dto.Password) ;

        if (foundUser == null){
            ViewData["LoginError"] = "No account found with these credentials.";
            return View();
        }
        var isUser = false;
        var claims = new List<Claim>() {
           new Claim(ClaimTypes.NameIdentifier, foundUser.Id.ToString()),
           new Claim(ClaimTypes.Name, foundUser.Name),
           new Claim(ClaimTypes.Surname, foundUser.Surname),
           new Claim(ClaimTypes.Email, foundUser.Mail),
           new Claim(ClaimTypes.Role, isUser ? "User" : "Admin")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        
        await base.HttpContext.SignInAsync(
           scheme: CookieAuthenticationDefaults.AuthenticationScheme, 
           principal: claimsPrincipal);

        if(isUser == true)
            return base.Redirect("/ProductsStore");
        else
            return base.Redirect("/Products");
    }

    [HttpGet("[action]")]
    [Authorize]

    public async Task<ActionResult> LogOut(){
        await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return base.RedirectToAction(controllerName: "Home", actionName: "Index");
    }
}
