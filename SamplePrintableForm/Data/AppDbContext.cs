using Microsoft.EntityFrameworkCore;
using SamplePrintableForm.Models;

namespace SamplePrintableForm.Data
{
   public class AppDbContext : DbContext
   {
      public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options)
      {
      }

      public DbSet<Customer> Customers { get; set; }
      public DbSet<Offer> Offers { get; set; }
      public DbSet<Currency> Currencies { get; set; }
   }
}