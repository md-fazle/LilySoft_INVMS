namespace LilySoft_INVMS.Models.Invms
{
    public class CustomerReturnDetail
    {
        public int id { get; set; }
        public string? customer_return_detail_id { get; set; }
        public string? customer_return_id { get; set; }
        public string? product_id { get; set; }
        public int quantity { get; set; }
        public decimal unit_price { get; set; }
        public decimal total_price { get; set; }

        public CustomerReturn? CustomerReturn { get; set; }
        public Product? Product { get; set; }
    }
}
