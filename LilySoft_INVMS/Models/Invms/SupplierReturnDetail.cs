namespace LilySoft_INVMS.Models.Invms
{
    public class SupplierReturnDetail
    {
        public int id { get; set; }
        public string? Supplier_return_id { get; set; }
        public string? product_id { get; set; }
        public int quantity { get; set; }
        public decimal unit_price { get; set; }
        public decimal total_price { get; set; }

        public SupplierReturn? SupplierReturn { get; set; }
        public Product? Product { get; set; }
    }
}
