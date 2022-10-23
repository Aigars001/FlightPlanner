using FlightPlannerCore.Models;

namespace FlightPlannerCore.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight GetCompleteFlightById(int id);
        bool Exists(Flight flight);
        List<Airport> FindAirport(string search);
        PageResult SearchFlightRequest(SearchFlightRequest search);
        void Clear();
    }
}
