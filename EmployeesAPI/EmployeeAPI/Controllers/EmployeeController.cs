﻿using System.ComponentModel.DataAnnotations;
using System.Net;
using Employee.Application.Interfaces.Services;
using EmployeeAPI.Mapper;
using EmployeeAPI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private static readonly GuidValidator GuidValidator = new("Employee Id");
        private static readonly EmployeeValidator EmployeeValidator = new();

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee.Contracts.Output.Employee))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync([FromRoute, Required] Guid id)
        {
            var validation = await GuidValidator.ValidateAsync(id);
            if (!validation.IsValid)
            {
                _logger.LogWarning("GuidValidator Validation Failed, Errors:{Errors}",
                    validation.Errors.Select(x => x.ErrorMessage));
                return CreateResponse(HttpStatusCode.BadRequest, validation.Errors.Select(x => x.ErrorMessage));
            }

            var maybeEmployee = await _employeeService.GetByIdAsync(id);
            return maybeEmployee.IsSome()
                ? CreateResponse(HttpStatusCode.OK, maybeEmployee.Value().ToContract())
                : CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee.Contracts.Output.Employee))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync([FromBody, Required] Employee.Contracts.Input.Employee employee)
        {
            var validation = await EmployeeValidator.ValidateAsync(employee);
            if (!validation.IsValid)
            {
                _logger.LogWarning("EmployeeValidator Validation Failed, Errors:{Errors}",
                    validation.Errors.Select(x => x.ErrorMessage));
                return CreateResponse(HttpStatusCode.BadRequest, validation.Errors.Select(x => x.ErrorMessage));
            }

            var maybeEmployee = await _employeeService.AddAsync(employee.ToDomain());
            return maybeEmployee.IsSome()
                ? CreateResponse(HttpStatusCode.OK, maybeEmployee.Value().ToContract())
                : CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Employee.Contracts.Output.Employee>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var maybeEmployees = await _employeeService.GetAllAsync();
            return CreateResponse(HttpStatusCode.OK, maybeEmployees.ToContract());
        }
    }
}