using System.ComponentModel.DataAnnotations;

namespace HardwareStoreMng.Models
{
    public class Product
    {

        [Key]
        public int productId { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public decimal price { get; set; }
        public virtual ICollection<BarCodes> BarCodes { get; set; }
        public virtual ICollection< InvoiceItem> Invoices { get; set; }
    }
}
