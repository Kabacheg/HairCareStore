using System.ComponentModel.DataAnnotations;
using Hair_Care_Store.Models;
using Hair_Care_Store.Excpetions;
using Hair_Care_Store.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ValidationException = Hair_Care_Store.Excpetions.ValidationException;
namespace Hair_Care_Store.Controllers;

[Route("[controller]/")]
[ProducesResponseType(400, Type = typeof(List<ValidationResponse>))]
[ProducesResponseType(500)]
public class TutorialsController : Controller
{
    private readonly ITutorialsRepository tutorialsRepository;
    public TutorialsController(ITutorialsRepository tutorialsRepository)
    {
        this.tutorialsRepository = tutorialsRepository;
    }

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(200)]
    public ActionResult CreateTutorial([FromForm]Tutorial tutorial) {
        try {
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
        catch(ValidationException validationException) {
            return base.BadRequest(validationException.validationResponseItems);
        }
        catch(Exception) {
            return base.StatusCode((int)HttpStatusCode.InternalServerError);
        }

        
    }


    [HttpPut]
    [Route("[action]")]
    [ProducesResponseType(200)]
    public ActionResult UpdateTutorial([FromBody]Tutorial tutorial) {
        try {
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
        catch(ValidationException validationException) {
            return base.BadRequest(validationException.validationResponseItems);   
        }
        catch(Exception) {
            return base.StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }


    [HttpDelete]
    [Route("[action]/{id:int}")]
    [ProducesResponseType(200)]
    public ActionResult DeleteTutorial(int id) {
        var tutorials = tutorialsRepository.GetAllTutorials();
        var foundTutorial = tutorials.FirstOrDefault(t => t.Id == id);
        if(foundTutorial == null) {
            return base.NotFound();
        }
        tutorialsRepository.DeleteTutorial(id);
        return base.Ok(tutorials);
    }


    [HttpGet]
    [ProducesResponseType(200, Type = typeof(Tutorial))]
    public ActionResult<Tutorial> GetTutorials() {
        var tutorials = tutorialsRepository.GetAllTutorials();
        if(tutorials == null) {
            return base.NotFound();
        }
        return base.Ok(tutorials);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(200, Type = typeof(Tutorial))]
    public ActionResult<Product> GetProductsById(int id) {
        var tutorialToShow = tutorialsRepository.GetAllTutorials().First(t => t.Id == id);
        if(tutorialToShow == null) {
            return base.NotFound();
        }
        return base.View(viewName: "TutorialInfo", model: tutorialToShow);
    }
}
