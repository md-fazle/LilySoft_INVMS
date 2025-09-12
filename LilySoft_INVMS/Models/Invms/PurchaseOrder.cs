namespace LilySoft_INVMS.Models.Invms
{
    public class PurchaseOrder
    {
        public int id { get; set; }
        public string? purchase_order_id { get; set; }
        public DateTime purchaseDate { get; set; }

        public ICollection<PODetail>? PODetails { get; set; }
        public ICollection<SupplierReturn>? SupplierReturns { get; set; }
    }
}
