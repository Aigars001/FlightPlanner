using Microsoft.AspNetCore.Mvc;
using flightPlanner.Models;
using System.Data.Entity;
using flightPlannerData;
using FlightPlannerCore.Services;
using AutoMapper;
using FlightPlannerCore.Models;
using FlightPlannerCore.Validations.SearchFlightRequestValidations;

namespace flightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<ISearchFlightRequestValidator> _searchFlightRequestValidator;

        public CustomerApiController(IFlightService flightService,
            IEnumerable<ISearchFlightRequestValidator> searchFlightRequestValidator,
            IMapper mapper)
        {
            _flightService = flightService;
            _searchFlightRequestValidator = searchFlightRequestValidator;
            _mapper = mapper;
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirport(string search)
        {
            var airport = _flightService.FindAirport(search);

            if(airport.Any())
            {
                var response = airport.Select(a => _mapper.Map<AirportRequest>(a)).ToList();

                return Ok(response);
            }

            return NotFound();
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult FindFlight(SearchFlightRequest search)
        {
            if (!_searchFlightRequestValidator.All(f => f.isValid(search)))
            {
                return BadRequest();
            }
            var result = _flightService.SearchFlightRequest(search);

            return Ok(result);
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult FindFlightById(int id)
        {
            var flight = _flightService.GetCompleteFlightById(id);

            if(flight != null)
            {
                var response = _mapper.Map<FlightRequest>(flight);
                return Ok(response);
            }

            return NotFound();
        }
    }
}
