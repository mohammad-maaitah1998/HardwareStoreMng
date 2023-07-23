using HardwareStoreMng.Models;
using Microsoft.EntityFrameworkCore;

namespace HardwareStoreMng.Context
{
    public class HardwareStoreMngContext: DbContext
    {

        public HardwareStoreMngContext(DbContextOptions<HardwareStoreMngContext> options) : base(options)
        {

        }
        public virtual DbSet<BarCodes> BarCodes { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItem { get; set; }
        public virtual DbSet<Product> Product { get; set; }

    }
}
