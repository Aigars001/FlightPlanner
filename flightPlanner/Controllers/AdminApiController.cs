using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using flightPlanner.Storage;
using flightPlanner.Models;

namespace flightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlight(id);
            if(flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            FlightStorage.DeleteFlight(id);
            
            return Ok();
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flight flight)
        {

            if (FlightStorage.NullOrEmptyValidator(flight))
            {
                if(FlightStorage.SameAirportValidator(flight))
                {
                    return BadRequest();
                }

                if(!FlightStorage.DateValidator(flight))
                {
                    return BadRequest();
                }

                if (FlightStorage.SameFlightValidator(flight))
                {
                    return Conflict();
                }

                flight = FlightStorage.AddFlight(flight);
                return Created("", flight);
            }
            
            return BadRequest();
        }
    }
}
