using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using XeroTechnicalTest.Domain.Exceptions;
using XeroTechnicalTest.Models;

namespace XeroTechnicalTest.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }
        
        public override void OnException(ExceptionContext context)
        {
            ErrorResponse response;

            if (context.Exception is ObjectNotFoundException)
            {
                response = new ErrorResponse
                {
                    StatusCode = StatusCodes.Status404NotFound.ToString()
                };
                
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else
            {
                // Unhandled exceptions
                response = new ErrorResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError.ToString(),
                    Message = "Internal Server Error"
                };
                
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            
            _logger.LogError(context.Exception.ToString());

            context.HttpContext.Response.ContentType = "application/json";
            context.Result = new JsonResult(response);
            
            base.OnException(context);
        }
    }
}