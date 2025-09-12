namespace LilySoft_INVMS.Models.Invms
{
    public class PODetail
    {
        public int id { get; set; }
        public string? po_detail_id { get; set; }
        public string? purchase_order_id { get; set; }
        public string? product_id { get; set; }
        public int quantity { get; set; }
        public decimal unit_price { get; set; }
        public decimal total_price { get; set; }

        public PurchaseOrder? PurchaseOrder { get; set; }
        public Product? Product { get; set; }
    }
}
