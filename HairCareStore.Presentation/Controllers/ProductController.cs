using Hair_Care_Store.Core.Models;
using Hair_Care_Store.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Hair_Care_Store.Core.Excpetions;
using System.Net;
using HairCareStore.Core.Services;
using Hair_Care_Store.Core.Responses;
using FluentValidation;
using System.Threading.Tasks;
using ValidationException = Hair_Care_Store.Core.Excpetions.ValidationException;
namespace Hair_Care_Store.Presentation.Controllers;

[Route("[controller]/")]
[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(NotFoundResponse))]
[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResponse))]
[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponse))]
[ProducesResponseType(200)]
public class ProductController : Controller
{
    private readonly IProductRepository productRepository;
    private readonly IHttpLogger logger;
    private readonly IValidator<Product> productValidator;

    public ProductController(IProductRepository productRepository, IHttpLogger logger, IValidator<Product> productValidator)
    {
        this.productRepository = productRepository;
        this.logger = logger;
        this.productValidator = productValidator;
    }


    [HttpPost("[action]")]
    public async Task<ActionResult> CreateProduct([FromForm]Product product) {

        var validationResult = await this.productValidator.ValidateAsync(product);

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
        productRepository.AddProduct(product);
        return base.Redirect("/");
    }


    [HttpPut("[action]")]
    public async Task<ActionResult> UpdateProduct([FromBody]Product product) {

        var validationResult = await this.productValidator.ValidateAsync(product);

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
        productRepository.UpdateProduct(product);
        return base.Ok();
    }


    [HttpDelete("[action]/{id:int}")]
    public ActionResult DeleteProduct(int id) {
        var products = productRepository.GetAllProducts();
        var foundProduct = products.FirstOrDefault(p => p.Id == id);
        if(foundProduct == null) {
            return base.NotFound();
        }
        productRepository.DeleteProduct(id);
        return base.Ok();
    }


    [HttpGet]
    [ProducesResponseType(200, Type = typeof(Product))]
    public ActionResult<IEnumerable<Product>> GetProducts() {
        var products = productRepository.GetAllProducts();
        if(products == null) {
            return base.NotFound();
        }
        return base.Ok(products);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(Product))]
    public ActionResult<Product> GetProductsById(int id) {
        var productToShow = productRepository.GetAllProducts().First(p => p.Id == id);
        if(productToShow == null) {
            return base.NotFound();
        }
        return base.View(viewName: "ProductInfo", model: productToShow);
    }
}

