namespace LilySoft_INVMS.Models.Invms
{
    public class SupplierViewModel
    {
        public int Id { get; set; }
        public string? SupplierId { get; set; }
        public string? SupplierName { get; set; }

        // List of contacts
        public List<SupplierContact> Contacts { get; set; } = new();
    }
}
