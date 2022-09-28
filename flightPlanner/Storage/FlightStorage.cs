using flightPlanner.Models;

namespace flightPlanner.Storage
{
    public class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _id = 0;
        private static readonly object _flightLock = new object();

        public static Flight AddFlight(Flight flight)
        {
            lock(_flightLock)
            {
                flight.Id = ++_id;
                _flights.Add(flight);
                return flight;
            }
        }

        public static Flight GetFlight(int id)
        {
            lock (_flightLock)
            {
                return _flights.FirstOrDefault(f => f.Id == id);
            }
        }

        public static void Clear()
        {
            _flights.Clear();
            _id = 0;
        }

        public static bool NullOrEmptyValidator(Flight flight)
        {
            return flight != null &&
                !string.IsNullOrEmpty(flight.From.AirportCode) && !string.IsNullOrEmpty(flight.To.AirportCode) &&
                !string.IsNullOrEmpty(flight.From.City) && !string.IsNullOrEmpty(flight.To.City) &&
                !string.IsNullOrEmpty(flight.From.Country) && !string.IsNullOrEmpty(flight.To.Country) &&
                !string.IsNullOrEmpty(flight.DepartureTime) && !string.IsNullOrEmpty(flight.ArrivalTime) &&
                !string.IsNullOrEmpty(flight.Carrier);        
        }

        public static bool SameFlightValidator(Flight flight)
        {
            lock(_flightLock)
            {
                if(_flights.Any(f =>
                f.To.AirportCode.ToLower().Trim() == flight.To.AirportCode.ToLower().Trim() &&
                f.To.Country.ToLower().Trim() == flight.To.Country.ToLower().Trim() &&
                f.To.City.ToLower().Trim() == flight.To.City.ToLower().Trim() &&
                f.From.City.ToLower().Trim() == flight.From.City.ToLower().Trim() &&
                f.From.Country.ToLower().Trim() == flight.From.Country.ToLower().Trim() &&
                f.From.AirportCode.ToLower().Trim() == flight.From.AirportCode.ToLower().Trim() &&
                f.Carrier.ToLower().Trim() == flight.Carrier.ToLower().Trim() &&
                f.ArrivalTime == flight.ArrivalTime &&
                f.DepartureTime == flight.DepartureTime))
                {
                    return true;
                }

                return false;
            }
        }

        public static void DeleteFlight(int id)
        {
            lock(_flightLock)
            {
                var flight = GetFlight(id);

                if(flight != null)
                {
                    _flights.Remove(flight);
                }
            }
        }

        public static Airport[] SearchAirport(string searchValue)
        {
            var searchList = new List<Airport>();
            searchValue = searchValue.ToLower().Trim();
            
            foreach(Flight f in _flights)
            {
                if (f.From.AirportCode.ToLower().Contains(searchValue) ||
                    f.From.Country.ToLower().Contains(searchValue) ||
                    f.From.City.ToLower().Contains(searchValue))
                {
                    searchList.Add(f.From);
                }

                if (f.To.AirportCode.ToLower().Contains(searchValue) ||
                    f.To.Country.ToLower().Contains(searchValue) ||
                    f.To.City.ToLower().Contains(searchValue))
                {
                    searchList.Add(f.To);
                }
            }

            return searchList.ToArray();
        }

        public static PageResult SearchFlight(SearchFlightRequest req)
        {
            var flightList = new List<Flight>();
            lock(_flightLock)
            {
                foreach(Flight f in _flights)
                {
                    if(req.From == f.From.AirportCode &&
                       req.To == f.To.AirportCode &&
                       DateTime.Parse(req.DepartureDate) == DateTime.Parse(f.DepartureTime).Date)
                    {
                        flightList.Add(f);
                    }
                }

                return new PageResult(flightList.ToArray());
            }
        }

        public static bool SameAirportValidator(Flight flight)
        {
            return flight.From.AirportCode.ToLower().Trim() == flight.To.AirportCode.ToLower().Trim();
        }

        public static bool DateValidator(Flight flight)
        {
            var arival = DateTime.Parse(flight.ArrivalTime);
            var departure = DateTime.Parse(flight.DepartureTime);

            return arival > departure;
        }
    }
}
