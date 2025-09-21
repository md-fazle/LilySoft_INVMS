using LilySoft_INVMS.Models.Invms;

namespace LilySoft_INVMS.Services
{
    public interface IPurchaseRequestService
    {
        Task<List<PurchaseRequest>> GetAllAsync();
        Task<PurchaseRequest?> GetByIdAsync(int id);
        Task AddAsync(PurchaseRequestViewModel model);
        Task UpdateAsync(PurchaseRequestViewModel model);
        Task DeleteAsync(int id);
    }
}
