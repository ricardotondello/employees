using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Employee.Application.Interfaces.Services;
using Employee.Contracts.Output;
using EmployeeAPI.Mapper;
using EmployeeAPI.Validators;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class RegionController : BaseController
    {
        private readonly IRegionService _regionService;
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<RegionController> _logger;
        private static readonly IdValidator IdValidator = new("Region Id");
        private static readonly RegionValidator RegionValidator = new();

        public RegionController(IRegionService regionService, IEmployeeService employeeService,
            ILogger<RegionController> logger)
        {
            _regionService = regionService;
            _employeeService = employeeService;
            _logger = logger;
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
                _logger.LogWarning("IdValidator Validation Failed, Errors:{Errors}",
                    validation.Errors.Select(x => x.ErrorMessage));
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
        public async Task<IActionResult> PostAsync([FromBody, Required] Employee.Contracts.Input.Region region)
        {
            var validation = await RegionValidator.ValidateAsync(region);
            if (!validation.IsValid)
            {
                _logger.LogWarning("RegionValidator Validation Failed, Errors:{Errors}",
                    validation.Errors.Select(x => x.ErrorMessage));
                return CreateResponse(HttpStatusCode.BadRequest, validation.Errors.Select(x => x.ErrorMessage));
            }

            var maybeRegion = await _regionService.AddAsync(region.ToDomain());
            return maybeRegion.IsSome()
                ? CreateResponse(HttpStatusCode.OK, maybeRegion.Value().ToContract())
                : CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpGet("{id}/employees")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeAggregate))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PostAsync([FromRoute, Required] int id)
        {
            var validation = await IdValidator.ValidateAsync(id);
            if (!validation.IsValid)
            {
                _logger.LogWarning("IdValidator Validation Failed, Errors:{Errors}",
                    validation.Errors.Select(x => x.ErrorMessage));
                return CreateResponse(HttpStatusCode.BadRequest, validation.Errors.Select(x => x.ErrorMessage));
            }

            var maybeEmployee = (await _employeeService.GetEmployeesByRegionAsync(id)).ToList();
            return maybeEmployee.Any()
                ? CreateResponse(HttpStatusCode.OK, maybeEmployee.Select(s => s.ToAggregateContract()).ToList())
                : CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Region>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var regions = await _regionService.GetAllAsync();
            return CreateResponse(HttpStatusCode.OK, regions.ToContract());
        }
    }
}