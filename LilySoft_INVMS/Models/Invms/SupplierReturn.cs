namespace LilySoft_INVMS.Models.Invms
{
    public class SupplierReturn
    {
        public int id { get; set; }
        public string? supplier_return_id { get; set; }
        public string? purchase_order_id { get; set; }
        public DateTime Purchase_ReturnDate { get; set; }

        public PurchaseOrder? PurchaseOrder { get; set; }
        public ICollection<SupplierReturnDetail>? SupplierReturnDetails { get; set; }
    }
}
