namespace LilySoft_INVMS.Models.Invms
{
    public class CustomerReturn
    {
        public int id { get; set; }
        public string? Customer_return_id { get; set; }
        public string? order_id { get; set; }
        public DateTime ReturnDate { get; set; }

        public Order? Order { get; set; }
        public ICollection<CustomerReturnDetail>? CustomerReturnDetails { get; set; }



    }
}
