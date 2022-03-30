using EmployeeAPI.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using EmployeeAPI.Contracts.Output;
using EmployeeAPI.Mapper;
using EmployeeAPI.Validators;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionController : BaseController
    {
        private readonly IRegionService _regionService;
        private readonly IEmployeeService _employeeService;
        private static readonly IdValidator IdValidator = new IdValidator("Region Id");
        private static readonly RegionValidator RegionValidator = new RegionValidator();
        public RegionController(IRegionService regionService, IEmployeeService employeeService)
        {
            _regionService = regionService;
            _employeeService = employeeService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Region))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync([FromRoute, Required] int id)
        {
            var validation = await IdValidator.ValidateAsync(id);
            if (!validation.IsValid)
            {
                return CreateResponse(HttpStatusCode.BadRequest, validation.Errors.Select(x => x.ErrorMessage));
            }

            var maybeRegion = await _regionService.GetByIdAsync(id);
            return maybeRegion.IsSome() 
                ? CreateResponse(HttpStatusCode.OK, maybeRegion.Value().ToContract()) 
                : CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Region))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PostAsync([FromBody, Required] Contracts.Input.Region region)
        {
            var validation = await RegionValidator.ValidateAsync(region);
            if (!validation.IsValid)
            {
                return CreateResponse(HttpStatusCode.BadRequest, validation.Errors.Select(x => x.ErrorMessage));
            }

            var maybeRegion = await _regionService.AddAsync(region.ToDomain());
            return maybeRegion.IsSome()
                ? CreateResponse(HttpStatusCode.OK, maybeRegion.Value().ToContract())
                : CreateResponse(HttpStatusCode.NoContent);
        }
        [HttpGet("{id}/employees")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contracts.Output.EmployeeAggregate))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PostAsync([FromRoute, Required] int id)
        {
            var validation = await IdValidator.ValidateAsync(id);
            if (!validation.IsValid)
            {
                return CreateResponse(HttpStatusCode.BadRequest, validation.Errors.Select(x => x.ErrorMessage));
            }

            var maybeEmployee = await _employeeService.GetEmployeesByRegionAsync(id);
            return maybeEmployee.Any()
                ? CreateResponse(HttpStatusCode.OK, maybeEmployee.Select(s => s.ToAggregateContract()).ToList())
                : CreateResponse(HttpStatusCode.NoContent);
        }

    }
}
