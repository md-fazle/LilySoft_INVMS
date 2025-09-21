namespace LilySoft_INVMS.Models.Invms
{
    public class PurchaseRequest
    {
        public int id { get; set; }
        public string? purchase_request_id { get; set; }
        public DateTime RequestDate { get; set; }

        // ✅ Must not be nullable
        public ICollection<PRDetail> PRDetails { get; set; } = new List<PRDetail>();
    }
}
