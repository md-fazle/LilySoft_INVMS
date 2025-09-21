using LilySoft_INVMS.Data;
using LilySoft_INVMS.Models.Invms;
using Microsoft.EntityFrameworkCore;

namespace LilySoft_INVMS.Services
{
    public class StockService : IStockService
    {
        private readonly InvmsDbContext _context;
        public StockService(InvmsDbContext context)
        {
            _context = context;
        }
        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks
                .Include(s => s.Product)
                .Include(s => s.Warehouse)
                .ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks
                .Include(s => s.Product)
                .Include(s => s.Warehouse)
                .FirstOrDefaultAsync(s => s.id == id);
        }

        public async Task AddAsync(Stock stock)
        {
            stock.last_updated = DateTime.Now;
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Stock stock)
        {
            stock.last_updated = DateTime.Now;
            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock != null)
            {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
            }
        }

    }
}
