using System;
using System.Collections;
using System.Collections.Generic;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using SamplePrintableForm.Data;
using SamplePrintableForm.Models;

namespace SamplePrintableForm
{
   public static class Seeder
   {
      private static AppDbContext _dbContext;
      private static List<Customer> _customers;
      private static List<Offer> _offers;

      public static ArrayList Errors { get; set; }

      public static void Initialize(IServiceProvider serviceProvider)
      {
         _dbContext = serviceProvider.GetRequiredService<AppDbContext>();
         _customers = serviceProvider.GetRequiredService<List<Customer>>();
         _offers = serviceProvider.GetRequiredService<List<Offer>>();

         _dbContext.Database.EnsureDeleted();
         _dbContext.Database.EnsureCreated();

         Errors = new ArrayList();

         AddCustomers();


         DumpErrors();
      }

      private static void AddCustomers()
      {
         var customers = new Faker<Customer>()
            .RuleFor(c => c.Name, f => f.Person.FirstName)
            .RuleFor(c => c.Surname, f => f.Person.LastName)
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Phone, f => f.Person.Phone)
            .Generate(20);

         try
         {
            _dbContext.Customer.AddRange(customers);
            _dbContext.SaveChanges();
         }
         catch (Exception e)
         {
            Errors.Add(e);
         }
      }

      private static void AddOffers()
      {
         var offers = new Faker<Offer>()
            .RuleFor(o => o.Date, f => f.Date.Past())
            .RuleFor(o => o.Price, f => decimal.Parse(f.Commerce.Price(1750, 2750)))
            .RuleFor(o => o.Currency, Currency.Euro)
            .Generate(15);

         for (var i = 0; i < offers.Count; i++)
         {
            offers[i].Customer = _customers[i];
            offers[i].CustomerId = _customers[i].Id;
         }

         try
         {
            _dbContext.Offer.AddRange(offers);
            _dbContext.SaveChanges();
         }
         catch (Exception e)
         {
            Errors.Add(e);
         }
      }

      private static void AddErrors<T>(T error) where T : class
      {
         Errors.Add(error);
      }

      private static void DumpErrors()
      {
         foreach (var error in Errors) Console.WriteLine(error);
      }
   }
}