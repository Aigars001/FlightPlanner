using flightPlanner.Models;

namespace flightPlanner.Validations
{
    public class Validators
    {
        public static bool NullOrEmptyValidator(Flight flight)
        {
            return flight != null &&
                !string.IsNullOrEmpty(flight.From.AirportCode) && !string.IsNullOrEmpty(flight.To.AirportCode) &&
                !string.IsNullOrEmpty(flight.From.City) && !string.IsNullOrEmpty(flight.To.City) &&
                !string.IsNullOrEmpty(flight.From.Country) && !string.IsNullOrEmpty(flight.To.Country) &&
                !string.IsNullOrEmpty(flight.DepartureTime) && !string.IsNullOrEmpty(flight.ArrivalTime) &&
                !string.IsNullOrEmpty(flight.Carrier);
        }

        public static bool AddSameAirportValidator(Flight flight)
        {
            return flight.From.AirportCode.Trim().ToLower() == flight.To.AirportCode.Trim().ToLower();
        }

        public static bool DateValidator(Flight flight)
        {
            var arival = DateTime.Parse(flight.ArrivalTime);
            var departure = DateTime.Parse(flight.DepartureTime);

            return arival > departure;
        }

        public static bool NullValueValidator(SearchFlightRequest search)
        {
            return search.From == null ||
                search.To == null ||
                search.DepartureDate == null;
        }

        public static bool SearchSameAirportValidator(SearchFlightRequest search)
        {
            return search.From == search.To;
        }
    }
}
