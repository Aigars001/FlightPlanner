using FlightPlannerCore.Models;

namespace FlightPlannerCore.Validations.SearchFlightRequestValidations
{
    public class SameAirportValidator : ISearchFlightRequestValidator
    {
        public bool isValid(SearchFlightRequest search)
        {
            return search.From.Trim().ToLower() != search.To.Trim().ToLower();
        }
    }
}
