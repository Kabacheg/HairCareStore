using Hair_Care_Store.Core.Models;
using Hair_Care_Store.Core.Excpetions;
using Hair_Care_Store.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ValidationException = Hair_Care_Store.Core.Excpetions.ValidationException;
using HairCareStore.Core.Services;
using Hair_Care_Store.Core.Responses;
using FluentValidation;
namespace Hair_Care_Store.Presentation.Controllers;

[Route("[controller]/")]
[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(NotFoundResponse))]
[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResponse))]
[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponse))]
[ProducesResponseType(200)]
public class TutorialsController : Controller
{
    private readonly ITutorialsRepository tutorialsRepository;
    private readonly IHttpLogger logger;
    private readonly IValidator<Tutorial> tutorialValidator;
    public TutorialsController(ITutorialsRepository productRepository, IHttpLogger logger, IValidator<Tutorial> tutorialValidator)
    {
        this.tutorialsRepository= productRepository;
        this.logger = logger;
        this.tutorialValidator = tutorialValidator;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> CreateTutorial([FromForm]Tutorial tutorial) {
        var validationResult = await this.tutorialValidator.ValidateAsync(tutorial);

        var validationException = new ValidationException();
        
        if(validationResult.IsValid == false) {
            foreach (var errorMessage in validationResult.Errors)
            {
               validationException.validationResponseItems.Add(new ValidationResponse( $"{errorMessage}")); 
            } 
        }
        if(validationException.validationResponseItems.Any()) {
            throw validationException;
        }
        tutorialsRepository.AddTutorial(tutorial);
        return base.Redirect("/Home/Tutorials/");
        
    }


    [HttpPut("[action]")]
    public async Task<ActionResult> UpdateTutorial([FromBody]Tutorial tutorial) {
        var validationResult = await this.tutorialValidator.ValidateAsync(tutorial);

        var validationException = new ValidationException();
        
        if(validationResult.IsValid == false) {
            foreach (var errorMessage in validationResult.Errors)
            {
               validationException.validationResponseItems.Add(new ValidationResponse( $"{errorMessage}")); 
            } 
        }
        if(validationException.validationResponseItems.Any()) {
            throw validationException;
        }
        tutorialsRepository.UpdateTutorial(tutorial);
        return base.Ok();

    }


    [HttpDelete("[action]/{id:int}")]
    public ActionResult DeleteTutorial(int id) {
        var tutorials = tutorialsRepository.GetAllTutorials();
        var foundTutorial = tutorials.FirstOrDefault(t => t.Id == id);
        if(foundTutorial == null) {
            return base.NotFound();
        }
        tutorialsRepository.DeleteTutorial(id);
        return base.Ok();
    }


    [HttpGet]
    [ProducesResponseType(200, Type = typeof(Tutorial))]
    public ActionResult<IEnumerable<Tutorial>> GetTutorials() {
        var tutorials = tutorialsRepository.GetAllTutorials();
        if(tutorials == null) {
            return base.NotFound();
        }
        return base.Ok(tutorials);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(Tutorial))]
    public ActionResult<Product> GetProductsById(int id) {
        var tutorialToShow = tutorialsRepository.GetAllTutorials().First(t => t.Id == id);
        if(tutorialToShow == null) {
            return base.NotFound();
        }
        return base.View(viewName: "TutorialInfo", model: tutorialToShow);
    }
}
