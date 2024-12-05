namespace WebApplication1.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int CustomerTypeId {  get; set; }
        public CustomerType? CustomerType { get; set; }
        public DateTime BusinessStartDate { get; set; }
        public string Phone { get; set; }
        public decimal CreditLimit { get; set; }
        //public int DeliveryInfoId { get; set; }

        //public virtual DeliveryInfo? DeliveryInfo { get; set; }
        
        public ICollection<DeliveryInfo> DeliveryInfoList { get; set; }


        

    }
}
