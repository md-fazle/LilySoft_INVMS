namespace LilySoft_INVMS.Models.Invms
{
    public class ProductsViewModel
    {
        public string? product_image { get; set; }
        public string? stock_id { get; set; }
        public string? product_id { get; set; }
        public string? warehouse_name { get; set; }
        public string? warehouse_location { get; set; }
        public string? product_code { get; set; }
        public string? product_name { get; set; }
        public string? product_description { get; set; }
        public decimal? product_price { get; set; }
        public int quantity { get; set; }

        public decimal totalPrice { get; set; }


    }
}
