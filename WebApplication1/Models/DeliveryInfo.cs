namespace WebApplication1.Models
{
    public class DeliveryInfo
    {
        public int DeliveryInfoId { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        //public ICollection<Customer> Customers { get; set; } 

        public int CustomerId { get; set; } 
        public virtual Customer Customer { get; set; }
    }
}