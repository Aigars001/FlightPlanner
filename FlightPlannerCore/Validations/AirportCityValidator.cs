using FlightPlannerCore.Models;

namespace FlightPlannerCore.Validations
{
    public class AirportCityValidator : IAirportValidator
    {
        public bool IsValid(Airport airport)
        {
            return !string.IsNullOrEmpty(airport?.City);
        }
    }
}
