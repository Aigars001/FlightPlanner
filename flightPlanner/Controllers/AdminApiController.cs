using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using flightPlanner.Models;
using flightPlanner.Validations;
using System.Data.Entity;

namespace flightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        private static readonly object _locker = new();
        public AdminApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .FirstOrDefault(f => f.Id == id);
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
            lock(_locker)
            {
                var flight = _context.Flights.FirstOrDefault(f => f.Id == id);

                if (flight != null)
                {
                    _context.Remove(flight);
                    _context.SaveChanges();
                }

                return Ok();
            }
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flight flight)
        {
            if(Validators.NullOrEmptyValidator(flight))
            {
                if(!Validators.AddSameAirportValidator(flight))
                {
                    return BadRequest();
                }

                if(!Validators.DateValidator(flight))
                {
                    return BadRequest();
                }

                if(Exists(flight))
                {
                    return Conflict();
                }

                lock(_locker)
                {
                    _context.Add(flight);
                    _context.SaveChanges();

                    return Created("", flight);   
                    }
            }

            return BadRequest();
        }

        private bool Exists(Flight flight)
        {
            return _context.Flights.Any(f =>
            f.DepartureTime == flight.DepartureTime &&
            f.ArrivalTime == flight.ArrivalTime &&
            f.From.AirportCode.Trim().ToLower() == flight.From.AirportCode.Trim().ToLower() &&
            f.To.AirportCode.Trim().ToLower() == flight.To.AirportCode.Trim().ToLower());
        }
    }
}
