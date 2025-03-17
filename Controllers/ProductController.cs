using Hair_Care_Store.Models;
using Hair_Care_Store.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using Hair_Care_Store.Excpetions;
using System.Net;
using HairCareStore.Services.Base;
using Hair_Care_Store.Responses;
namespace Hair_Care_Store.Controllers;

[Route("[controller]/")]
[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(NotFoundResponse))]
[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(InternalServerErrorResponse))]
[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(BadRequestResponse))]
[ProducesResponseType(200)]
public class ProductController : Controller
{
    private readonly IProductRepository productRepository;
    private readonly IHttpLogger logger;
    public ProductController(IProductRepository productRepository, IHttpLogger logger)
    {
        this.productRepository = productRepository;
        this.logger = logger;
    }


    [HttpPost("[action]")]
    public ActionResult CreateProduct([FromForm]Product product) {

        var validationException = new ValidationException();
        if(string.IsNullOrWhiteSpace(product.Name)) {
            validationException.validationResponseItems.Add(new ValidationResponse(nameof(product.Name), $"{nameof(product.Name)} can not be empty"));
        }
        if(product.Price < 0) {
            validationException.validationResponseItems.Add(new ValidationResponse(nameof(product.Price), $"{nameof(product.Price)} can not be less than 0"));
        }
        if(validationException.validationResponseItems.Any()) {
            throw validationException;
        }
        productRepository.AddProduct(product);
        return base.Redirect("/");
    }


    [HttpPut("[action]")]
    public ActionResult UpdateProduct([FromBody]Product product) {
        var validationException = new ValidationException();
        if(string.IsNullOrWhiteSpace(product.Name)) {
            validationException.validationResponseItems.Add(new ValidationResponse(nameof(product.Name), $"{nameof(product.Name)} can not be empty"));
        }
        if(product.Price < 0) {
            validationException.validationResponseItems.Add(new ValidationResponse(nameof(product.Price), $"{nameof(product.Price)} can not be less than 0"));
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

