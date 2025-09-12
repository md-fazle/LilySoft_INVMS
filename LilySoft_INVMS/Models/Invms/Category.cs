namespace LilySoft_INVMS.Models.Invms
{
    public class Category
    {
        public int id { get; set; }
        public string? catagory_id { get; set; }
        public string? catagory_name { get; set; }
        public string? catagory_description { get; set; }

        public ICollection<Product>? Products { get; set; }

    }
}
