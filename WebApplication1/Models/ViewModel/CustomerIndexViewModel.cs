namespace WebApplication1.Models.ViewModel
{
    public class CustomerIndexViewModel
    {
        public List<Customer> Customers { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartItem { get; set; }
        public int EndItem { get; set; }
    }
}
