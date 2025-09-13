using LilySoft_INVMS.Data;
using LilySoft_INVMS.Models.Invms;
using Microsoft.EntityFrameworkCore;

namespace LilySoft_INVMS.Services
{
    public class ProductService:IProductService
    {
        private readonly InvmsDbContext _context;

        public ProductService(InvmsDbContext context)
        {
            _context = context;
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _context.Products.Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.id == id);

            if (product == null)
            {
                throw new InvalidOperationException($"Product with ID {id} not found.");
            }

            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync() ?? Enumerable.Empty<Product>();
        }
        public async Task CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
