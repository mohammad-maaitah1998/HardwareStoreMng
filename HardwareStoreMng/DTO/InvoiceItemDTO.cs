using System.ComponentModel.DataAnnotations;

namespace HardwareStoreMng.DTO
{
    public class InvoiceItemDTO
    {
        [Key]
        public int InvoiceItemId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter The Invoice Id please   ")]
        public int InvoiceId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter The Product Id   ")]
        public int ProductId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter The Quantity of the product   ")]
        public int Quantity { get; set; }
    }
}
