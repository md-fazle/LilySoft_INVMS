using LilySoft_INVMS.Data;
using LilySoft_INVMS.Models.Invms;
using Microsoft.EntityFrameworkCore;

namespace LilySoft_INVMS.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly InvmsDbContext _context;
        public WarehouseService(InvmsDbContext context)
        {
            _context = context;
        }
        public async Task<List<Warehouse>> GetAllAsync()
        {
            return await _context.Warehouses
                                 .Include(w => w.Stocks)
                                 .ToListAsync();
        }

        public async Task<Warehouse?> GetByIdAsync(int id)
        {
            return await _context.Warehouses
                                 .Include(w => w.Stocks)
                                 .FirstOrDefaultAsync(w => w.id == id);
        }

        public async Task CreateAsync(Warehouse warehouse)
        {
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Warehouse warehouse)
        {
            _context.Warehouses.Update(warehouse);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse != null)
            {
                _context.Warehouses.Remove(warehouse);
                await _context.SaveChangesAsync();
            }
        }


    }
}
