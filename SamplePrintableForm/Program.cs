using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

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

      public static IWebHost BuildWebHost(string[] args)
      {
         return WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build();
      }
   }
}