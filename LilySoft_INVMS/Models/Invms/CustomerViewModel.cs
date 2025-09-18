namespace LilySoft_INVMS.Models.Invms
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }

        public List<CustomerContact> Contacts { get; set; } = new List<CustomerContact>();
    }
}
