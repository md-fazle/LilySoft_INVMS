namespace LilySoft_INVMS.Models.Invms
{
    public class Stock
    {
        public int id { get; set; }
        public string? stock_id { get; set; }
        public string? product_id { get; set; }
        public string? warehouse_id { get; set; }
        public int quantity { get; set; }
        public DateTime last_updated { get; set; }

        public Product? Product { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
