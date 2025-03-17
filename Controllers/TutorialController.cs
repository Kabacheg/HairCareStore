using System.ComponentModel.DataAnnotations;
using Hair_Care_Store.Models;
using Hair_Care_Store.Excpetions;
using Hair_Care_Store.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ValidationException = Hair_Care_Store.Excpetions.ValidationException;
using HairCareStore.Services.Base;
using Hair_Care_Store.Responses;
namespace Hair_Care_Store.Controllers;

[Route("[controller]/")]
[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(NotFoundResponse))]
[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResponse))]
[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponse))]
[ProducesResponseType(200)]
public class TutorialsController : Controller
{
    private readonly ITutorialsRepository tutorialsRepository;
    private readonly IHttpLogger logger;
    public TutorialsController(ITutorialsRepository productRepository, IHttpLogger logger)
    {
        this.tutorialsRepository= productRepository;
        this.logger = logger;
    }

    [HttpPost("[action]")]
    public ActionResult CreateTutorial([FromForm]Tutorial tutorial) {
        var validationException = new ValidationException();
        if(string.IsNullOrWhiteSpace(tutorial.Topic)) {
            validationException.validationResponseItems.Add(new ValidationResponse(nameof(tutorial.Topic), $"{nameof(tutorial.Topic)} can not be empty"));
        }
        if(string.IsNullOrWhiteSpace(tutorial.Instruction)) {
            validationException.validationResponseItems.Add(new ValidationResponse(nameof(tutorial.Instruction), $"{nameof(tutorial.Instruction)}  can not be empty"));
        }
        if(validationException.validationResponseItems.Any()) {
            throw validationException;
        }
        tutorialsRepository.AddTutorial(tutorial);
        return base.Redirect("/Home/Tutorials/");
        
    }


    [HttpPut("[action]")]
    public ActionResult UpdateTutorial([FromBody]Tutorial tutorial) {
        var validationException = new ValidationException();
        if(string.IsNullOrWhiteSpace(tutorial.Topic)) {
            validationException.validationResponseItems.Add(new ValidationResponse(nameof(tutorial.Topic), $"{nameof(tutorial.Topic)} can not be empty"));
        }
        if(string.IsNullOrWhiteSpace(tutorial.Instruction)) {
            validationException.validationResponseItems.Add(new ValidationResponse(nameof(tutorial.Instruction), $"{nameof(tutorial.Instruction)}  can not be empty"));
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
