using System.ComponentModel.DataAnnotations;

namespace SamplePrintableForm.Models
{
   public class Customer
   {
      [Key]
      public int Id { get; set; }
      [Required, MinLength(2), MaxLength(20)]
      public string Name { get; set; }
      [MinLength(2), MaxLength(20)]
      public string MiddleName { get; set; }
      [Required, MinLength(2), MaxLength(20)]
      public string Surname { get; set; }
      [Required, Phone]
      public string Phone { get; set; }
      [Required, EmailAddress]
      public string Email { get; set; }
   }
}
