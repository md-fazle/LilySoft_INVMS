namespace LilySoft_INVMS.Models.Invms
{
    public class Supplier
    {
        public int id { get; set; }
        public string? supplier_id { get; set; }
        public string? supplier_name { get; set; }
        public ICollection<SupplierContact>? SupplierContacts { get; set; }
        public ICollection<PurchaseOrder>? PurchaseOrders { get; set; }
        public ICollection<SupplierReturn>? SupplierReturns { get; set; }
    }
}
