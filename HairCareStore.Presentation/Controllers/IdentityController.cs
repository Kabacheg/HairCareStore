using System.ComponentModel.DataAnnotations;
using System.Net;
using Hair_Care_Store.Core.Models;
using Hair_Care_Store.Core.Repositories;
using Hair_Care_Store.Core.Responses;
using HairCareStore.Core.Dtos;
using HairCareStore.Core.Models;
using HairCareStore.Core.Services;
using HairCareStore.Core.Validators;
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
        TempData["AvatarFilePath"] = filepath;

        using var avatarFileStream = System.IO.File.Create(filepath);
        await UserAvatar.CopyToAsync(avatarFileStream);

        userRepository.AddUser(newUser);
        return base.RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public ActionResult Login()
    {
        return base.View();
    }

    [HttpGet("[action]")]
    public ActionResult UserPage()
    {
        return base.View();
    }

    [HttpPost]
    public ActionResult Login([FromForm] UserDetailsForLoginDto dto)
    {
        //TODO in FINAL:
        //1.Proper design for login and registration
        //2.add store for normal users

        var foundUser = userRepository.GetAllUsers().FirstOrDefault(u => u.Mail == dto.Mail && u.Password == dto.Password);

        if (foundUser == null){
            return base.RedirectToAction(nameof(Login));
        }
        var idHash = dataProtector.Protect(foundUser.Id.ToString());
        base.HttpContext.Response.Cookies.Append("Identity", idHash);
        ViewBag.AvatarPath = TempData["AvatarFilePath"];
            
        return base.View(viewName: nameof(UserPage), model: foundUser);
    }

    [HttpGet("[action]")]

    public ActionResult LogOut(){
        base.HttpContext.Response.Cookies.Delete("Identity");

        return base.RedirectToAction(controllerName: "Home", actionName: "Index");
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> UploadProfilePicture([FromForm]  IFormFile profilePicture){
        return base.RedirectToAction(nameof(Login));
    }
}
