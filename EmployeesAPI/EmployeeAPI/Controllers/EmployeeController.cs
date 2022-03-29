using System.ComponentModel.DataAnnotations;
using System.Net;
using EmployeeAPI.Application.Interfaces.Services;
using EmployeeAPI.Mapper;
using EmployeeAPI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        private static readonly GuidValidator GuidValidator = new GuidValidator("Employee Id");
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contracts.Output.Employee))]
        public async Task<IActionResult> GetAsync([FromRoute, Required] Guid id)
        {
            var validation = await GuidValidator.ValidateAsync(id);
            if (!validation.IsValid)
            {
                return CreateResponse(HttpStatusCode.BadRequest, validation.Errors.Select(x => x.ErrorMessage));
            }
        
            var maybeEmployee = await _employeeService.GetByIdAsync(id);
            return maybeEmployee.IsSome()
                ? CreateResponse(HttpStatusCode.OK, maybeEmployee.Value().ToContract())
                : CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
