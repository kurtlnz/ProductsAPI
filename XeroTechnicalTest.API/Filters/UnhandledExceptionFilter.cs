using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using XeroTechnicalTest.Models;

namespace XeroTechnicalTest.Filters
{
    public class UnhandledExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<UnhandledExceptionFilter> _logger;

        public UnhandledExceptionFilter(ILogger<UnhandledExceptionFilter> logger)
        {
            _logger = logger;
        }
        
        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception.ToString());
            
            context.HttpContext.Response.StatusCode = 500;
            context.HttpContext.Response.ContentType = "application/json";
            
            context.Result = new JsonResult(new ErrorResponse
            {
                StatusCode = "500",
                Message = "Internal Server Error"
            });
            
            base.OnException(context);
        }
    }
}