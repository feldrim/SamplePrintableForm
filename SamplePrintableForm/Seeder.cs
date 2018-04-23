using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
      private static List<Currency> _currencies;

      public static ArrayList Errors { get; set; }

      public static void Initialize(IServiceProvider serviceProvider)
      {
         _dbContext = serviceProvider.GetRequiredService<AppDbContext>();
         _dbContext.Database.EnsureDeleted();
         _dbContext.Database.EnsureCreated();

         Errors = new ArrayList();

         AddCurrencies();
         AddCustomers();
         AddOffers();

         DumpErrors();
      }

      private static void AddCurrencies()
      {
         _currencies = new List<Currency>
         {
            new Currency {Name = "Euro", Code = "EUR", Symbol = "€"},
            new Currency {Name = "US Dollar", Code = "USD", Symbol = "$"},
            new Currency {Name = "Turkish Lira", Code = "TRY", Symbol = "₺"},
         };

         try
         {
            _dbContext.Currencies.AddRange(_currencies);
            _dbContext.SaveChanges();
         }
         catch (Exception e)
         {
            Errors.Add(e);
         }
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
            _dbContext.Customers.AddRange(_customers);
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
            .RuleFor(o => o.Version, f => 1)
            .RuleFor(o => o.Date, f => f.Date.Past())
            .RuleFor(o => o.Price, f => decimal.Parse(f.Commerce.Price(1750, 2750)))
            .Generate(20);

         for (var i = 0; i < _offers.Count; i++)
         {
            _offers[i].Customer = _customers[i];
            _offers[i].CustomerId = _customers[i].Id;
            _offers[i].Currency = _currencies.First(c => c.Code.Equals("EUR"));
            _offers[i].CurrencyId = _currencies.First(c => c.Code.Equals("EUR")).Id;
         }

         try
         {
            _dbContext.Offers.AddRange(_offers);
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