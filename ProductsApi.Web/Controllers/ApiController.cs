using Microsoft.AspNetCore.Mvc;
using XeroTechnicalTest.Filters;

namespace XeroTechnicalTest.Controllers
{
    [TypeFilter(typeof(ApiExceptionFilter))]
    public class ApiController : ControllerBase
    {
        
    }
}