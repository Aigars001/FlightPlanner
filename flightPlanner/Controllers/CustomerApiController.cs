using Microsoft.AspNetCore.Mvc;
using flightPlanner.Storage;
using flightPlanner.Models;

namespace flightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirport(string search)
        {
            var airport = FlightStorage.SearchAirport(search);
               
            return Ok(airport);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult FindFlight(SearchFlightRequest search)
        {
            if(search.From == null || search.To == null || search.DepartureDate == null)
            {
                return BadRequest();
            }

            if(search.From == search.To)
            {
                return BadRequest();
            }

            var flight = FlightStorage.SearchFlight(search);

            return Ok(flight);
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult FindFlightById(int id)
        {
            var flight = FlightStorage.GetFlight(id);

            if(flight != null)
            {
                return Ok(flight);
            }

            return NotFound();
        }
    }
}
