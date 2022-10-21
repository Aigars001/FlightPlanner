namespace FlightPlannerCore.Models
{
    public class PageResult
    {
        public int Page { get; set; }
        public List<Flight> Items { get; set; }
        public int TotalItems => Items.Count;
    }
}
