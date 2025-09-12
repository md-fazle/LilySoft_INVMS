namespace LilySoft_INVMS.Models.Invms
{
    public class Order
    {
        public int id { get; set; }
        public string? order_id { get; set; }
        public string? customer_id { get; set; }
        public DateTime orderDate { get; set; }

        public Customer? Customer { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<CustomerReturn>? CustomerReturns { get; set; }
    }
}
