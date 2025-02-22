using Hair_Care_Store.Models;
using Hair_Care_Store.Repositories.Base;
using Microsoft.AspNetCore.Mvc;
using Hair_Care_Store.Excpetions;
using System.Net;
namespace Hair_Care_Store.Controllers;

[Route("[controller]/")]
[ProducesResponseType(400, Type = typeof(List<ValidationResponse>))]
[ProducesResponseType(500)]
public class ProductController : Controller
{
    private readonly IProductRepository productRepository;
    public ProductController(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }


    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(200)]
    public ActionResult CreateProduct([FromForm]Product product) {
        try {
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
    public ActionResult UpdateProduct([FromBody]Product product) {
        try {
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
    public ActionResult DeleteProduct(int id) {
        var products = productRepository.GetAllProducts();
        var foundProduct = products.FirstOrDefault(p => p.Id == id);
        if(foundProduct == null) {
            return base.NotFound();
        }
        productRepository.DeleteProduct(id);
        return base.Ok(products);
    }


    [HttpGet]
    [ProducesResponseType(200, Type = typeof(Product))]
    public ActionResult<Product> GetProducts() {
        var products = productRepository.GetAllProducts();
        if(products == null) {
            return base.NotFound();
        }
        return base.Ok(products);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(200, Type = typeof(Product))]
    public ActionResult<Product> GetProductsById(int id) {
        var productToShow = productRepository.GetAllProducts().First(p => p.Id == id);
        if(productToShow == null) {
            return base.NotFound();
        }
        return base.View(viewName: "ProductInfo", model: productToShow);
    }
}

