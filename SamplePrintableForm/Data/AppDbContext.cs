using Microsoft.EntityFrameworkCore;

namespace SamplePrintableForm.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<SamplePrintableForm.Models.Customer> Customer { get; set; }

        public DbSet<SamplePrintableForm.Models.Offer> Offer { get; set; }
    }
}
