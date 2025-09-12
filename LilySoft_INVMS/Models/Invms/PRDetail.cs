namespace LilySoft_INVMS.Models.Invms
{
    public class PRDetail
    {
        public int id { get; set; }
        public string? pr_detail_id { get; set; }
        public string? purchase_request_id { get; set; }
        public string? product_id { get; set; }
        public int quantity { get; set; }
        public decimal unit_price { get; set; }
        public decimal total_price { get; set; }

        public PurchaseRequest? PurchaseRequest { get; set; }
        public Product? Product { get; set; }
    }
}
