namespace FlightPlannerCore.Models
{
    public class SearchFlightRequest
    {
        public string From { set; get; }
        public string To { set; get; }
        public string DepartureDate { get; set; }
    }
}
