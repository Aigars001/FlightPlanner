using FlightPlannerCore.Models;

namespace FlightPlannerCore.Validations
{
    public interface IFlightValidator
    {
        bool IsValid(Flight flight);
    }
}
