using System;
using System.Collections;
using System.Collections.Generic;
using Bogus;
using Bogus.DataSets;
using Microsoft.Extensions.DependencyInjection;
using SamplePrintableForm.Data;
using SamplePrintableForm.Models;
using Currency = SamplePrintableForm.Models.Currency;

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
         _dbContext.Database.EnsureDeleted();
         _dbContext.Database.EnsureCreated();

         Errors = new ArrayList();

         AddCustomers();
         AddOffers();

         DumpErrors();
      }

      private static void AddCustomers()
      {
         _customers = new Faker<Customer>()
            .StrictMode(false)
            .CustomInstantiator(c => new Customer())
            .Ignore(c => c.Id)
            .Rules((f, c) =>
            {
               c.Name = f.Name.FirstName(Name.Gender.Male);
               c.Surname = f.Name.LastName();
               c.Email = $"{c.Name.ToLowerInvariant()}@gmail.com";
               c.Phone = f.Phone.PhoneNumber();
            })
            .Generate(20);

         try
         {
            _dbContext.Customer.AddRange(_customers);
            _dbContext.SaveChanges();
         }
         catch (Exception e)
         {
            Errors.Add(e);
         }
      }

      private static void AddOffers()
      {
         _offers = new Faker<Offer>()
            .RuleFor(o => o.Date, f => f.Date.Past())
            .RuleFor(o => o.Price, f => decimal.Parse(f.Commerce.Price(1750, 2750)))
            .RuleFor(o => o.Currency, Currency.Euro)
            .Generate(15);

         for (var i = 0; i < _offers.Count; i++)
         {
            _offers[i].Customer = _customers[i];
            _offers[i].CustomerId = _customers[i].Id;
         }

         try
         {
            _dbContext.Offer.AddRange(_offers);
            _dbContext.SaveChanges();
         }
         catch (Exception e)
         {
            Errors.Add(e);
         }
      }

      private static void DumpErrors()
      {
         foreach (var error in Errors) Console.WriteLine(error);
      }
   }
}