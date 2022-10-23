using FlightPlannerCore.Models;

namespace FlightPlannerCore.Validations.SearchFlightRequestValidations
{
    public class DepartureDateValidator : ISearchFlightRequestValidator
    {
        public bool isValid(SearchFlightRequest search)
        {
            return !string.IsNullOrEmpty(search.DepartureDate);
        }
    }
}
