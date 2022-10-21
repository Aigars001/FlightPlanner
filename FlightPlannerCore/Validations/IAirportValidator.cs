using FlightPlannerCore.Models;

namespace FlightPlannerCore.Validations
{
    public interface IAirportValidator
    {
        bool IsValid(Airport airport);
    }
}
