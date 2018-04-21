using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SamplePrintableForm
{
   public static class Program
   {
      public static void Main(string[] args)
      {
         var webHost = BuildWebHost(args);

         using (var scope = webHost.Services.CreateScope())
         {
            var services = scope.ServiceProvider;
            try
            {
               Seeder.Initialize(services);
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
            }
         }

         webHost.Run();
      }

      public static IWebHost BuildWebHost(string[] args) =>
          WebHost.CreateDefaultBuilder(args)
              .UseStartup<Startup>()
              .Build();
   }
}
