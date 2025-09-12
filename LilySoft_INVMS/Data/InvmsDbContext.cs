using Microsoft.EntityFrameworkCore;
using LilySoft_INVMS.Models.Invms; // assuming all your models are in this namespace

namespace LilySoft_INVMS.Data
{
    public class InvmsDbContext : DbContext
    {
        public InvmsDbContext(DbContextOptions<InvmsDbContext> options)
            : base(options) { }

        // DbSets for all your models
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierContact> SupplierContacts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerContact> CustomerContacts { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<PurchaseRequest> PurchaseRequests { get; set; }
        public DbSet<PRDetail> PRDetails { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PODetail> PODetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<CustomerReturn> CustomerReturns { get; set; }
        public DbSet<CustomerReturnDetail> CustomerReturnDetails { get; set; }
        public DbSet<SupplierReturn> SupplierReturns { get; set; }
        public DbSet<SupplierReturnDetail> SupplierReturnDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ======= Relationships =======

            // Category -> Product
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.catagory_id)
                .HasPrincipalKey(c => c.catagory_id)
                .OnDelete(DeleteBehavior.Restrict);

            // Supplier -> SupplierContact
            modelBuilder.Entity<SupplierContact>()
                .HasOne(sc => sc.Supplier)
                .WithMany(s => s.SupplierContacts)
                .HasForeignKey(sc => sc.supplier_id)
                .HasPrincipalKey(s => s.supplier_id)
                .OnDelete(DeleteBehavior.Cascade);

            // Customer -> CustomerContact
            modelBuilder.Entity<CustomerContact>()
                .HasOne(cc => cc.Customer)
                .WithMany(c => c.customerContacts)
                .HasForeignKey(cc => cc.customer_id)
                .HasPrincipalKey(c => c.customer_id);

            // Product -> Stock
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Product)
                .WithMany(p => p.Stocks)
                .HasForeignKey(s => s.product_id)
                .HasPrincipalKey(p => p.product_id);

            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Warehouse)
                .WithMany(w => w.Stocks)
                .HasForeignKey(s => s.warehouse_id)
                .HasPrincipalKey(w => w.warehouse_id);

            // PurchaseRequest -> PRDetail
            modelBuilder.Entity<PRDetail>()
                .HasOne(prd => prd.PurchaseRequest)
                .WithMany(pr => pr.PRDetails)
                .HasForeignKey(prd => prd.purchase_request_id)
                .HasPrincipalKey(pr => pr.purchase_request_id);

            modelBuilder.Entity<PRDetail>()
                .HasOne(prd => prd.Product)
                .WithMany(p => p.PRDetails)
                .HasForeignKey(prd => prd.product_id)
                .HasPrincipalKey(p => p.product_id);

            // PurchaseOrder -> PODetail
            modelBuilder.Entity<PODetail>()
                .HasOne(pod => pod.PurchaseOrder)
                .WithMany(po => po.PODetails)
                .HasForeignKey(pod => pod.purchase_order_id)
                .HasPrincipalKey(po => po.purchase_order_id);

            modelBuilder.Entity<PODetail>()
                .HasOne(pod => pod.Product)
                .WithMany(p => p.PODetails)
                .HasForeignKey(pod => pod.product_id)
                .HasPrincipalKey(p => p.product_id);

            // Customer -> Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.customer_id)
                .HasPrincipalKey(c => c.customer_id);

            // Order -> OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.order_id)
                .HasPrincipalKey(o => o.order_id);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.product_id)
                .HasPrincipalKey(p => p.product_id);

            // CustomerReturn -> CustomerReturnDetail
            modelBuilder.Entity<CustomerReturnDetail>()
                .HasOne(crd => crd.CustomerReturn)
                .WithMany(cr => cr.CustomerReturnDetails)
                .HasForeignKey(crd => crd.customer_return_id)
                .HasPrincipalKey(cr => cr.Customer_return_id);

            modelBuilder.Entity<CustomerReturnDetail>()
                .HasOne(crd => crd.Product)
                .WithMany(p => p.CustomerReturnDetails)
                .HasForeignKey(crd => crd.product_id)
                .HasPrincipalKey(p => p.product_id);

            // SupplierReturn -> SupplierReturnDetail
            modelBuilder.Entity<SupplierReturnDetail>()
                .HasOne(srd => srd.SupplierReturn)
                .WithMany(sr => sr.SupplierReturnDetails)
                .HasForeignKey(srd => srd.Supplier_return_id)
                .HasPrincipalKey(sr => sr.supplier_return_id);

            modelBuilder.Entity<SupplierReturnDetail>()
                .HasOne(srd => srd.Product)
                .WithMany(p => p.SupplierReturnDetails)
                .HasForeignKey(srd => srd.product_id)
                .HasPrincipalKey(p => p.product_id);

            // CustomerReturn -> Order
            modelBuilder.Entity<CustomerReturn>()
                .HasOne(cr => cr.Order)
                .WithMany(o => o.CustomerReturns)
                .HasForeignKey(cr => cr.order_id)
                .HasPrincipalKey(o => o.order_id);

            // SupplierReturn -> PurchaseOrder
            modelBuilder.Entity<SupplierReturn>()
                .HasOne(sr => sr.PurchaseOrder)
                .WithMany(po => po.SupplierReturns)
                .HasForeignKey(sr => sr.purchase_order_id)
                .HasPrincipalKey(po => po.purchase_order_id);

            // ======= Decimal Precision =======

            modelBuilder.Entity<Product>()
                .Property(p => p.product_price).HasPrecision(18, 2);

            modelBuilder.Entity<PRDetail>()
                .Property(p => p.unit_price).HasPrecision(18, 2);
            modelBuilder.Entity<PRDetail>()
                .Property(p => p.total_price).HasPrecision(18, 2);

            modelBuilder.Entity<PODetail>()
                .Property(p => p.unit_price).HasPrecision(18, 2);
            modelBuilder.Entity<PODetail>()
                .Property(p => p.total_price).HasPrecision(18, 2);

            modelBuilder.Entity<OrderDetail>()
                .Property(p => p.unit_price).HasPrecision(18, 2);
            modelBuilder.Entity<OrderDetail>()
                .Property(p => p.total_price).HasPrecision(18, 2);

            modelBuilder.Entity<CustomerReturnDetail>()
                .Property(p => p.unit_price).HasPrecision(18, 2);
            modelBuilder.Entity<CustomerReturnDetail>()
                .Property(p => p.total_price).HasPrecision(18, 2);

            modelBuilder.Entity<SupplierReturnDetail>()
                .Property(p => p.unit_price).HasPrecision(18, 2);
            modelBuilder.Entity<SupplierReturnDetail>()
                .Property(p => p.total_price).HasPrecision(18, 2);
        }
    }
}
