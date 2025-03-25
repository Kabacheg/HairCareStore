using System.ComponentModel.DataAnnotations;
using System.Net;
using Hair_Care_Store.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using ValidationException = Hair_Care_Store.Core.Excpetions.ValidationException;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate next;
    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext) {
        try {
            await this.next.Invoke(httpContext);
        }
        catch(ValidationException ex) {
    
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await httpContext.Response.WriteAsJsonAsync(new BadRequestResponse(message: ex.CombineExceptionMessages()));

            httpContext.Items["exception"] = ex.ToString();
        }
        catch (KeyNotFoundException ex) {
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await httpContext.Response.WriteAsJsonAsync(new NotFoundResponse(message: ex.Message));

            httpContext.Items["exception"] = ex.ToString();
        }
        catch(Exception ex) {
            System.Console.WriteLine(ex.Message);
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new InternalServerErrorResponse(message: "Internal server error"));
            
            httpContext.Items["exception"] = ex.ToString();
        }
    }
}