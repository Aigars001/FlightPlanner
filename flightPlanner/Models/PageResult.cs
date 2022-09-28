namespace flightPlanner.Models
{
    public class PageResult
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public Flight[] Items { get; set; }

        public PageResult(Flight[] items)
        {
            Page = 0;
            Items = items;
            TotalItems = items.Length;
        }
    }
}
