using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult CreateResponse<T>(HttpStatusCode statusCode, T value)
        {
            return new ObjectResult(value)
            {
                StatusCode = (int) statusCode
            };
        }

        protected IActionResult CreateResponse(HttpStatusCode statusCode)
        {
            return CreateResponse(statusCode, default(object));
        }
    }
}