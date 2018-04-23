using System.ComponentModel.DataAnnotations;

namespace SamplePrintableForm.Models
{
   public class Currency
   {
      [Key] public int Id { get; set; }
      [Required] public string Name { get; set; }

      [Required]
      [MinLength(3)]
      [MaxLength(3)]
      public string Code { get; set; }

      [Required]
      [MinLength(1)]
      [MaxLength(1)]
      public string Symbol { get; set; }
   }
}