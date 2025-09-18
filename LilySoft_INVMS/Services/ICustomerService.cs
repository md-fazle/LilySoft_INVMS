using LilySoft_INVMS.Models.Invms;

namespace LilySoft_INVMS.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<CustomerViewModel?> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(CustomerViewModel model);
        Task UpdateCustomerAsync(CustomerViewModel model);
        Task DeleteCustomerAsync(int id);
    }
}
