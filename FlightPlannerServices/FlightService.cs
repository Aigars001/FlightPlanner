using FlightPlannerCore.Models;
using FlightPlannerCore.Services;
using FlightPlannerData;
using Microsoft.EntityFrameworkCore;


namespace FlightPlannerServices
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {

        }

        public Flight GetCompleteFlightById(int id)
        {
            return _context.Flights.Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
        }

        public bool Exists(Flight flight)
        {
            return _context.Flights.Any(f => f.DepartureTime == flight.DepartureTime &&
            f.ArrivalTime == flight.ArrivalTime &&
            f.Carrier == flight.Carrier &&
            f.From.AirportCode == flight.From.AirportCode &&
            f.To.AirportCode == flight.To.AirportCode);
        }

        public List<Airport> FindAirport(string search)
        {
            search = search.Trim().ToLower();

            var result = _context.Airports.Where(a =>
            a.AirportCode.Trim().ToLower().Contains(search) ||
            a.City.Trim().ToLower().Contains(search) ||
            a.Country.Trim().ToLower().Contains(search)).ToList();

            return result.DistinctBy(a => a.AirportCode).ToList();
        }

        public PageResult SearchFlightRequest(SearchFlightRequest search)
        {
            var result = new PageResult();
            var items = new List<Flight>();

            var flight = _context.Flights
            .Include(f => f.From)
            .Include(f => f.To)
            .FirstOrDefault(f =>
            f.From.AirportCode == search.From &&
            f.To.AirportCode == search.To &&
            f.DepartureTime.Contains(search.DepartureDate));

            if (flight != null)
            {
                items.Add(flight);
            }

            result.Items = items;

            return result;
        }

        public void Clear()
        {
            _context.Flights.RemoveRange(_context.Flights);
            _context.Airports.RemoveRange(_context.Airports);
            _context.SaveChanges();
        }
    }
}
