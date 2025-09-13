using LilySoft_INVMS.Models.Invms;

namespace LilySoft_INVMS.Services
{
    public interface ICategoryService
    {

        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);


    }
}
