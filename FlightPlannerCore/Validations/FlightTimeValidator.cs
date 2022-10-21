using FlightPlannerCore.Models;

namespace FlightPlannerCore.Validations
{
    public class FlightTimeValidator : IFlightValidator
    {
        public bool IsValid(Flight flight)
        {
            if(!string.IsNullOrEmpty(flight?.ArrivalTime) &&
                !string.IsNullOrEmpty(flight?.DepartureTime))
            {
                var departure = DateTime.Parse(flight.DepartureTime);
                var arrival = DateTime.Parse(flight.ArrivalTime);

                return departure < arrival;
            }

            return false;
        }
    }
}
