using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SamplePrintableForm.Models;

namespace SamplePrintableForm.Models
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
