using Microsoft.AspNetCore.Mvc;
using XeroTechnicalTest.Filters;

namespace XeroTechnicalTest.Controllers
{
    [TypeFilter(typeof(UnhandledExceptionFilter))]
    public class ApiController : ControllerBase
    {
        
    }
}