using FlightPlannerCore.Models;

namespace FlightPlannerCore.Validations
{
    public class CarrierValidator : IFlightValidator
    {
        public bool IsValid(Flight flight)
        {
            return !string.IsNullOrEmpty(flight?.Carrier);
        }
    }
}
