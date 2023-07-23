using System.ComponentModel.DataAnnotations;

namespace HardwareStoreMng.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        public int InvoiceNumber { get; set; }

        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }

        public virtual ICollection<InvoiceItem> invoiceItems { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
