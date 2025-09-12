using LilySoft_INVMS.Models.Invms;

namespace LilySoft_INVMS.Services
{
    public interface ICRUDInvmsServices
    {
        Task<IEnumerable<Product>> GetAllProducts()
    }
}
