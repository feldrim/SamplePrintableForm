using System;
using System.ComponentModel.DataAnnotations;

namespace SamplePrintableForm.Models
{
   public class Offer
   {
      [Key]
      public int Id { get; set; }
      [Required]
      public int CustomerId { get; set; }
      [Required]
      public Customer Customer { get; set; }
      [Required]
      public DateTime Date { get; set; }
      [Required]
      public decimal Price { get; set; }
      [Required]
      public Currency Currency { get; set; }
   }

   public enum Currency
   {
      Euro,
      Dollar,
      Lira
   }
}
