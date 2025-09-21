using LilySoft_INVMS.Data;
using LilySoft_INVMS.Models.Invms;
using LilySoft_INVMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace LilySoft_INVMS.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockService _stockService;
        private readonly InvmsDbContext _context;

        public StockController(IStockService stockService, InvmsDbContext context)
        {
            _stockService = stockService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var stocks = await _stockService.GetAllAsync();
            return View(stocks);
        }

        public IActionResult Create()
        {
            ViewBag.Products = _context.Products.ToList();
            ViewBag.Warehouses = _context.Warehouses.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Stock stock)
        {
            if (ModelState.IsValid)
            {
                await _stockService.AddAsync(stock);
                return RedirectToAction(nameof(Index));
            }
            return View(stock);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var stock = await _stockService.GetByIdAsync(id);
            if (stock == null) return NotFound();

            ViewBag.Products = _context.Products.ToList();
            ViewBag.Warehouses = _context.Warehouses.ToList();
            return View(stock);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Stock stock)
        {
            if (ModelState.IsValid)
            {
                await _stockService.UpdateAsync(stock);
                return RedirectToAction(nameof(Index));
            }
            return View(stock);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var stock = await _stockService.GetByIdAsync(id);
            if (stock == null) return NotFound();
            return View(stock);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _stockService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Stock/
        public async Task<IActionResult> ProductList()
        {
            var stockProducts = await _stockService.GetStockProductsAsync();
            return View(stockProducts);
        }
    }
}
