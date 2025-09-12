namespace LilySoft_INVMS.Models.Invms
{
    public class Product
    {
        public int id { get; set; }
        public string? product_id { get; set; }
        public string? product_code { get; set; }
        public string? product_name { get; set; }
        public string? product_description { get; set; }
        public string? product_image { get; set; }
        public decimal? product_price { get; set; }

        public string? catagory_id { get; set; }
        public Category? Category { get; set; }

        public ICollection<Stock>? Stocks { get; set; }
        public ICollection<PRDetail>? PRDetails { get; set; }
        public ICollection<PODetail>? PODetails { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<CustomerReturnDetail>? CustomerReturnDetails { get; set; }
        public ICollection<SupplierReturnDetail>? SupplierReturnDetails { get; set; }
    }
}
