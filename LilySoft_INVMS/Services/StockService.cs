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


        public async Task<List<ProductsViewModel>> GetStockProductsAsync()
        {
            var result = await (from stock in _context.Stocks
                                join product in _context.Products on stock.product_id equals product.product_id
                                join warehouse in _context.Warehouses on stock.warehouse_id equals warehouse.warehouse_id
                                select new ProductsViewModel
                                {
                                    stock_id = stock.stock_id,
                                    product_id = product.product_id,
                                    product_image = product.product_image,
                                    product_code = product.product_code,
                                    product_name = product.product_name,
                                    product_description = product.product_description,
                                    product_price = product.product_price,
                                    quantity = stock.quantity,
                                    warehouse_name = warehouse.warehouse_name,
                                    warehouse_location = warehouse.warehouse_location,
                                    totalPrice = (product.product_price ?? 0) * stock.quantity
                                }).ToListAsync();

            return result;
        }

    }
}
