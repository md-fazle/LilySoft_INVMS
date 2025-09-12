namespace LilySoft_INVMS.Models.Invms
{
    public class SupplierContact
    {
        public int id { get; set; }
        public string? supplier_contact_id { get; set; }
        public string? supplier_id { get; set; }
        public string? contact_name { get; set; }
        public string? contact_email { get; set; }
        public string? contact_phone { get; set; }
        public string? contact_address { get; set; }

        public Supplier? Supplier { get; set; }
    }
}
