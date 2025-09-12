namespace LilySoft_INVMS.Models.Invms
{
    public class CustomerContact
    {
        public int id { get; set; }
        public string? customer_contact_id { get; set; }
        public string? customer_id { get; set; }
        public string? contact_name { get; set; }
        public string? contact_email { get; set; }
        public string? contact_phone { get; set; }
        public string? contact_address { get; set; }

        public Customer? Customer { get; set; }
    }
}
