namespace LilySoft_INVMS.Models.Invms
{
    public class PurchaseRequest
    {
        public int id { get; set; }
        public string? purchase_request_id { get; set; }
        public DateTime RequestDate { get; set; }

        public ICollection<PRDetail>? PRDetails { get; set; }
    }
}
