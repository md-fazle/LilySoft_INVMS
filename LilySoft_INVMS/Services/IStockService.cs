using LilySoft_INVMS.Models.Invms;

namespace LilySoft_INVMS.Services
{
    public interface IStockService
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task AddAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        Task DeleteAsync(int id);
    }
}
