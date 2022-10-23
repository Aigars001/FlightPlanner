using FlightPlannerCore.Models;

namespace FlightPlannerCore.Validations.SearchFlightRequestValidations
{
    public class AirportValueValidator : ISearchFlightRequestValidator
    {
        public bool isValid(SearchFlightRequest search)
        {
            return !string.IsNullOrEmpty(search.From) ||
            !string.IsNullOrEmpty(search.To);
        }
    }
}
