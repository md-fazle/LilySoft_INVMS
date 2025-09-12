namespace LilySoft_INVMS.Models.Invms
{
    public class Warehouse
    {
        public int id { get; set; }
        public string? warehouse_id { get; set; }
        public string? warehouse_name { get; set; }
        public string? warehouse_location { get; set; }

        public ICollection<Stock>? Stocks { get; set; }

    }
}
