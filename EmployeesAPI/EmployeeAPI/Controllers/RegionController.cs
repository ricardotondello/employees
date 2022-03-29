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
        private static readonly IdValidator IdValidator = new IdValidator("Region Id");
        public RegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Region))]
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

    }
}
