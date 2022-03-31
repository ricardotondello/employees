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
        private readonly ILogger<EmployeeController> _logger;
        private static readonly GuidValidator GuidValidator = new GuidValidator("Employee Id");
        private static readonly EmployeeValidator EmployeeValidator = new EmployeeValidator();

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contracts.Output.Employee))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync([FromRoute, Required] Guid id)
        {
            var validation = await GuidValidator.ValidateAsync(id);
            if (!validation.IsValid)
            {
                _logger.LogWarning("GuidValidator Validation Failed, Errors:{errors}", 
                    validation.Errors.Select(x => x.ErrorMessage));
                return CreateResponse(HttpStatusCode.BadRequest, validation.Errors.Select(x => x.ErrorMessage));
            }

            var maybeEmployee = await _employeeService.GetByIdAsync(id);
            return maybeEmployee.IsSome()
                ? CreateResponse(HttpStatusCode.OK, maybeEmployee.Value().ToContract())
                : CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contracts.Output.Employee))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync([FromBody, Required] Contracts.Input.Employee employee)
        {
            var validation = await EmployeeValidator.ValidateAsync(employee);
            if (!validation.IsValid)
            {
                _logger.LogWarning("EmployeeValidator Validation Failed, Errors:{errors}",
                    validation.Errors.Select(x => x.ErrorMessage));
                return CreateResponse(HttpStatusCode.BadRequest, validation.Errors.Select(x => x.ErrorMessage));
            }

            var maybeEmployee = await _employeeService.AddAsync(employee.ToDomain());
            return maybeEmployee.IsSome()
                ? CreateResponse(HttpStatusCode.OK, maybeEmployee.Value().ToContract())
                : CreateResponse(HttpStatusCode.NoContent);
        }
    }
}