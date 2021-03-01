using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleApi.Data.Entities;
using SampleApi.Data.Repository;
using SampleApi.RestAPI.Models;
using System.Threading.Tasks;

namespace SampleApi.RestAPI.Controllers
{
    [Route("locations")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsRepository _locationsRepo;
        private readonly ILogger<LocationsController> _logger;
        private readonly IMapper _mapper;

        public LocationsController(ILocationsRepository locationsRepo, ILogger<LocationsController> logger, IMapper mapper)
        {
            _locationsRepo = locationsRepo;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{locationId}")]
        public async Task<ActionResult<LocationDto>> Get(int locationId)
        {
            var locationFromRepo = await _locationsRepo.GetAsync(locationId);

            if (locationFromRepo is null)
                return NotFound();

            var locationDto = _mapper.Map<LocationDto>(locationFromRepo);

            return Ok(locationDto);
        }

        [Authorize("add:locations")]
        [HttpPost]
        public async Task<ActionResult<LocationDto>> PostAsync(CreateLocationDto createLocationDto)
        {
            // Note: check if the location with same data exists.
            if (await _locationsRepo.GetByAsync(createLocationDto.StreetName, createLocationDto.Building, createLocationDto.Flat) != null)
                return StatusCode(StatusCodes.Status409Conflict, "Location with the same data already exist.");

            var location = _mapper.Map<Location>(createLocationDto);
            await _locationsRepo.AddAsync(location);

            var locationDto = _mapper.Map<LocationDto>(location);
            return CreatedAtAction("Get", new { locationId = location.Id }, locationDto);
        }
    }
}
