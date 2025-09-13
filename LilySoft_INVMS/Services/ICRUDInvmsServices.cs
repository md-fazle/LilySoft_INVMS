using LilySoft_INVMS.Models.Invms;

namespace LilySoft_INVMS.Services
{
    public interface ICRUDInvmsServices
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Supplier> InsertSupplierWithContactsAsync(string supplier_id, string supplier_name, List<SupplierContact> contacts);
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
        Task<Supplier?> GetSupplierByCodeAsync(string supplier_id);
    }
}
