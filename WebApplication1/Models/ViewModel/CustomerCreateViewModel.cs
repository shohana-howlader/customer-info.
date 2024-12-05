namespace WebApplication1.Models.ViewModel
{
    public class CustomerCreateViewModel
    {
        public string CustomerId { get; set; } 
        public string Name { get; set; }
        public string Address { get; set; }
        public int CustomerTypeId { get; set; }
        public List<CustomerType> CustomerTypes { get; set; } = new();
        public DateTime BusinessStartDate { get; set; }
        public string Phone { get; set; }
        public decimal CreditLimit { get; set; }
        public List<DeliveryInfoViewModel> DeliveryInfos { get; set; } = new();
    }


    public class DeliveryInfoViewModel
    {
        public string DeliveryAddress { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
    }

}
