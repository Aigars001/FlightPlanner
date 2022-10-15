using Microsoft.AspNetCore.Mvc;
using flightPlanner.Models;
using System.Data.Entity;
using flightPlanner.Validations;

namespace flightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        private static readonly object _locker = new();

        public CustomerApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirport(string search)
        {
            search = search.Trim().ToLower();

            var searchAirports = _context.Airports.Where(a =>
            a.AirportCode.Trim().ToLower().Contains(search) ||
            a.City.Trim().ToLower().Contains(search) ||
            a.Country.Trim().ToLower().Contains(search));

            if(searchAirports.Any())
            {
                return Ok(searchAirports);
            }

            return NotFound();
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult FindFlight(SearchFlightRequest search)
        {
            if(Validators.NullValueValidator(search))
            {
                return BadRequest();
            }

            if(Validators.SearchSameAirportValidator(search))
            {
                return BadRequest();
            }

            lock(_locker)
            {
                var flight = new PageResult
                {
                    Items = _context.Flights
                    .Include(f => f.From)
                    .Include(f => f.To)
                    .ToList()
                    .Where(f => f.From.AirportCode.Trim().ToLower() == search.From.Trim().ToLower() &&
                    f.To.AirportCode.Trim().ToLower() == search.To.Trim().ToLower() &&
                    DateTime.Parse(f.DepartureTime).Date == DateTime.Parse(search.DepartureDate).Date
                    ).ToList()
                };

                return Ok(flight);
            }
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult FindFlightById(int id)
        {
            var flight = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .FirstOrDefault(f => f.Id == id);

            if(flight != null)
            {
                return Ok(flight);
            }

            return NotFound();
        }
    }
}
