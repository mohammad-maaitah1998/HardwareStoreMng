using System.ComponentModel.DataAnnotations;

namespace HardwareStoreMng.DTO
{
    public class InvoiceDTO
    {
        [Key]
        public int InvoiceId { get; set; }
        [Required( ErrorMessage = "Enter The Invoice number   ")]
        [RegularExpression("^\\d{4,}$",ErrorMessage ="Minimum length of invoice number is 4 numbers")]
        public int InvoiceNumber { get; set; }
        [Required(ErrorMessage = "Enter The Invoice Total Price   ")]
        public decimal TotalPrice { get; set; }
        [Required(ErrorMessage = "Enter The Invoice Date   ")]
        public DateTime Date { get; set; }
      [Required( ErrorMessage = "Enter The Customer id od the customer who by from the store   ")]
        public int CustomerId { get; set; }
        [Required( ErrorMessage = "Enter The Employee id of the employee who service this customer  ")]
        public int EmployeeId { get; set; }
    }
}
