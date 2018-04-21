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

      public DbSet<Customer> Customer { get; set; }

      public DbSet<Offer> Offer { get; set; }
   }
}