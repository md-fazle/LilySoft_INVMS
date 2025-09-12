namespace LilySoft_INVMS.Models.Invms
{
    public class OrderDetail
    {
        public int id { get; set; }
        public string? order_detail_id { get; set; }
        public string? order_id { get; set; }
        public string? product_id { get; set; }
        public int quantity { get; set; }
        public decimal unit_price { get; set; }
        public decimal total_price { get; set; }

        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
