using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleApi.Data.Entities;
using SampleApi.Data.Repository;
using SampleApi.RestAPI.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Threading.Tasks;

namespace SampleApi.RestAPI.Controllers
{
    [ApiController]
    [Route("apartments")]
    public class ApartmentsController : ControllerBase
    {
        private readonly IApartmentsRepository _apartmentsRepo;
        private readonly ILocationsRepository _locationsRepo;
        private readonly ILogger<ApartmentsController> _logger;
        private readonly IMapper _mapper;

        public ApartmentsController(ILogger<ApartmentsController> logger, IApartmentsRepository apartmentRepo, ILocationsRepository locationsRepo, IMapper mapper)
        {
            _logger = logger;
            _apartmentsRepo = apartmentRepo;
            _locationsRepo = locationsRepo;
            _mapper = mapper;
        }

        [HttpGet("{apartmentId}")]
        public async Task<ActionResult<ApartmentDto>> Get(int apartmentId)
        {
            var apartmentFromRepo = await _apartmentsRepo.GetAsync(apartmentId);
            if (apartmentFromRepo is null)
            {
                _logger.LogWarning("The apartment with {apartmentId} has not been found, returning 404.", apartmentId);
                return NotFound();                
            }
            _logger.LogInformation("The apartment with {apartmentId} has not been found, returning 404.", apartmentId);
            var apartmentDto = _mapper.Map<ApartmentDto>(apartmentFromRepo);
            _logger.LogInformation("The apartment with {apartmentId} has not been found, returning 404.", apartmentId);
            return Ok(apartmentDto);
        }

        /// <summary>
        /// Creates a apartment at specified location.
        /// </summary>
        /// <param name="createApartmentDto">The model of the new Apartment</param>
        /// <response code="500">Server side error has happened.</response>
        /// <response code="406">The requested media type is not supported.</response>
        /// <response code="400">The data provided in the request can not be parsed. Check the syntax of the json payload.</response>
        /// <response code="403">You have no permission for creating new apartments.</response>
        /// <response code="404">The location with the specified Id does not exist.</response>
        /// <response code="422">The apartment in this location already been added. </response>
        /// <returns></returns>
        [Authorize("add:apartments")]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(ApiErrorResponse), 403)]
        [ProducesResponseType(typeof(ApiErrorResponse), 404)]
        [ProducesResponseType(typeof(ApiErrorResponse), 406)]
        [ProducesResponseType(typeof(ApiErrorResponse), 422)]
        [SwaggerResponseHeader(201,"Location","URL", "Location of newly created apartment resource.")]
        [HttpPost]
        public async Task<ActionResult<ApartmentDto>> Post(CreateApartmentDto createApartmentDto)
        {
            var location = await _locationsRepo.GetAsync(createApartmentDto.LocationId);
            if (location is null)
                
                return this.StatusCode(StatusCodes.Status422UnprocessableEntity, "The location with the specified Id does not exist.");
            
            var existingApartmentForLocation = await _apartmentsRepo.GetByLocationIdAsync(createApartmentDto.LocationId);
            if (existingApartmentForLocation != null)
                return this.StatusCode(StatusCodes.Status422UnprocessableEntity, "The apartment in this location already been added.");

            var apartment = _mapper.Map<Apartment>(createApartmentDto);
            await _apartmentsRepo.AddAsync(apartment);
            apartment.Location = location;

            var apartmentDto = _mapper.Map<ApartmentDto>(apartment);
            return CreatedAtAction("Get", new { apartmentId = apartment.Id }, apartmentDto);
        }


        [HttpPut("{apartmentId}")]
        public async Task<ActionResult> Put(int apartmentId, UpdateApartmentDto updateApartmentDto)
        {
            var apartment = await _apartmentsRepo.GetAsync(apartmentId);
            if (apartment is null)
                return NotFound();

            var location = await _locationsRepo.GetAsync(updateApartmentDto.LocationId);
            if (location is null)
                return this.StatusCode(StatusCodes.Status422UnprocessableEntity, "The location with the specified Id does not exist.");

            await _apartmentsRepo.UpdateLocationAsync(apartmentId, updateApartmentDto.LocationId);

            return NoContent();
        }       
    }
}
