namespace LilySoft_INVMS.Models.Invms
{
    public class Customer
    {
        public int id { get; set; }
        public string? customer_id { get; set; }
        public string? customer_name { get; set; }

        public ICollection<CustomerContact>? customerContacts { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<CustomerReturn>? CustomerReturns { get; set; }
    }
}
