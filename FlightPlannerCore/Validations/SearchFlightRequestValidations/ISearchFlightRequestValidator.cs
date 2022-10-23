using FlightPlannerCore.Models;

namespace FlightPlannerCore.Validations.SearchFlightRequestValidations
{
    public interface ISearchFlightRequestValidator
    {
        bool isValid(SearchFlightRequest search);
    }
}
