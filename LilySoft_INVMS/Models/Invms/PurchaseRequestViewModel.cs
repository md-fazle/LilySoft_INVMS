namespace LilySoft_INVMS.Models.Invms
{
    public class PRDetailViewModel
    {
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Read-only property
        public decimal total_price => Quantity * UnitPrice;
    }

    public class PurchaseRequestViewModel
    {
        public int Id { get; set; }
        public string? PurchaseRequestId { get; set; }
        public DateTime RequestDate { get; set; }

        public List<PRDetailViewModel> Details { get; set; } = new();
    }
}
