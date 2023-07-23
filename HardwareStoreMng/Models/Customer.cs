using System.ComponentModel.DataAnnotations;

namespace HardwareStoreMng.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
