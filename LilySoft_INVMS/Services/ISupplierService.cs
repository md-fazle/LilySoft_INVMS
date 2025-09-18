using LilySoft_INVMS.Models.Invms;
using System.Threading.Tasks;

namespace LilySoft_INVMS.Services
{
    public interface ISupplierService
    {
        Task<List<Supplier>> GetAllSuppliersAsync();
        Task<SupplierViewModel> GetSupplierByIdAsync(int id);
        Task AddSupplierAsync(SupplierViewModel model);
        Task UpdateSupplierAsync(SupplierViewModel model);
        Task DeleteSupplierAsync(int id);
    }
}
