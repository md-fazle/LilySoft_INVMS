using LilySoft_INVMS.Data;
using LilySoft_INVMS.Models.Invms;

namespace LilySoft_INVMS.Services
{
    public class CRUDInvmsServices:ICRUDInvmsServices
    {
        private readonly InvmsDbContext _context;
        public CRUDInvmsServices(InvmsDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await Task.FromResult(_context.Products.ToList());
        }

    }
}
