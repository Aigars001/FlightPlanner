using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using flightPlanner.Models;
using FlightPlannerCore.Services;
using FlightPlannerCore.Models;
using FlightPlannerCore.Validations;
using AutoMapper;

namespace flightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IEnumerable<IFlightValidator> _flightSValidator;
        private readonly IEnumerable<IAirportValidator> _airportValidator;
        private readonly IMapper _mapper;

        public AdminApiController(IFlightService flightService,
            IEnumerable<IFlightValidator> flightSValidator,
            IEnumerable<IAirportValidator> airportValidator,
            IMapper mapper)
        {
            _flightService = flightService;
            _flightSValidator = flightSValidator;
            _airportValidator = airportValidator;
            _mapper = mapper;
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = _flightService.GetCompleteFlightById(id);

            if(flight == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<FlightRequest>(flight);

            return Ok(response);
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            var flight = _flightService.GetCompleteFlightById(id);
                
            if (flight != null)
            {
                var result = _flightService.Delete(flight);
                if(result.Success)
                {
                    return Ok();
                }

                return Problem(result.FormattedErrors);
            }

            return Ok();
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(FlightRequest request)
        {
            var flight = _mapper.Map<Flight>(request);
            if (!_flightSValidator.All(f => f.IsValid(flight)) ||
                !_airportValidator.All(a => a.IsValid(flight?.From)) ||
                !_airportValidator.All(a => a.IsValid(flight?.To)))
            {
                return BadRequest();
            }

            if(_flightService.Exists(flight))
            {
                return Conflict();
            }

            var result = _flightService.Create(flight); 
            if(result.Success)
            {
                request = _mapper.Map<FlightRequest>(flight);

                return Created("", request);   
            }

            return Problem(result.FormattedErrors);          
        }
    }
}
