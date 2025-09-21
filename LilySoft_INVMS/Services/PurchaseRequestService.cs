using LilySoft_INVMS.Data;
using LilySoft_INVMS.Models.Invms;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LilySoft_INVMS.Services
{
    public class PurchaseRequestService : IPurchaseRequestService
    {
        private readonly InvmsDbContext _context;

        public PurchaseRequestService(InvmsDbContext context)
        {
            _context = context;
        }

        public async Task<List<PurchaseRequest>> GetAllAsync()
        {
            return await _context.PurchaseRequests
                .Include(pr => pr.PRDetails)
                .ThenInclude(detail => detail.Product)
                .OrderByDescending(pr => pr.RequestDate)
                .ToListAsync();
        }

        public async Task<PurchaseRequest?> GetByIdAsync(int id)
        {
            return await _context.PurchaseRequests
                .Include(pr => pr.PRDetails)
                .ThenInclude(detail => detail.Product)
                .FirstOrDefaultAsync(pr => pr.id == id);
        }

        public async Task AddAsync(PurchaseRequestViewModel model)
        {
            // Generate purchase request ID if not provided
            var purchaseRequestId = model.PurchaseRequestId ?? GeneratePurchaseRequestId();

            var purchaseRequest = new PurchaseRequest
            {
                purchase_request_id = purchaseRequestId,
                RequestDate = model.RequestDate,
                PRDetails = model.Details.Select(detail => new PRDetail
                {
                    pr_detail_id = GeneratePRDetailId(),
                    product_id = detail.ProductId,
                    quantity = detail.Quantity,
                    unit_price = detail.UnitPrice,
                    total_price = detail.total_price
                }).ToList()
            };

            await _context.PurchaseRequests.AddAsync(purchaseRequest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PurchaseRequestViewModel model)
        {
            var existingRequest = await _context.PurchaseRequests
                .Include(pr => pr.PRDetails)
                .FirstOrDefaultAsync(pr => pr.id == model.Id);

            if (existingRequest == null)
                throw new ArgumentException("Purchase request not found");

            // Update main entity
            existingRequest.RequestDate = model.RequestDate;

            // Remove existing details
            _context.PRDetails.RemoveRange(existingRequest.PRDetails);

            // Add new details
            foreach (var detailVm in model.Details)
            {
                var newDetail = new PRDetail
                {
                    pr_detail_id = GeneratePRDetailId(),
                    purchase_request_id = existingRequest.purchase_request_id,
                    product_id = detailVm.ProductId,
                    quantity = detailVm.Quantity,
                    unit_price = detailVm.UnitPrice,
                    total_price = detailVm.total_price
                };
                existingRequest.PRDetails.Add(newDetail);
            }

            _context.PurchaseRequests.Update(existingRequest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var purchaseRequest = await _context.PurchaseRequests
                .Include(pr => pr.PRDetails)
                .FirstOrDefaultAsync(pr => pr.id == id);

            if (purchaseRequest == null)
                throw new ArgumentException("Purchase request not found");

            _context.PurchaseRequests.Remove(purchaseRequest);
            await _context.SaveChangesAsync();
        }

        private string GeneratePurchaseRequestId()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            return $"PR-{timestamp}";
        }

        private string GeneratePRDetailId()
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return $"PRD-{timestamp}";
        }
    }
}