using LilySoft_INVMS.Data;
using LilySoft_INVMS.Models.Invms;
using Microsoft.EntityFrameworkCore;

namespace LilySoft_INVMS.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly InvmsDbContext _context;
        public CustomerService(InvmsDbContext context)
        {
            _context = context;
        }
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers
                .Include(c => c.customerContacts)
                .ToListAsync();
        }

        public async Task<CustomerViewModel?> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.customerContacts)
                .FirstOrDefaultAsync(c => c.id == id);

            if (customer == null) return null;

            return new CustomerViewModel
            {
                Id = customer.id,
                CustomerId = customer.customer_id,
                CustomerName = customer.customer_name,
                Contacts = customer.customerContacts?.ToList() ?? new List<CustomerContact>()
            };
        }

        public async Task AddCustomerAsync(CustomerViewModel model)
        {
            var customer = new Customer
            {
                customer_id = model.CustomerId,
                customer_name = model.CustomerName,
                customerContacts = model.Contacts
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(CustomerViewModel model)
        {
            var customer = await _context.Customers
                .Include(c => c.customerContacts)
                .FirstOrDefaultAsync(c => c.id == model.Id);

            if (customer == null) return;

            customer.customer_id = model.CustomerId;
            customer.customer_name = model.CustomerName;

            // Remove old contacts
            if (customer.customerContacts != null)
                _context.CustomerContacts.RemoveRange(customer.customerContacts);

            // Add new contacts
            customer.customerContacts = model.Contacts;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.customerContacts)
                .FirstOrDefaultAsync(c => c.id == id);

            if (customer == null) return;

            if (customer.customerContacts != null)
                _context.CustomerContacts.RemoveRange(customer.customerContacts);

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}
