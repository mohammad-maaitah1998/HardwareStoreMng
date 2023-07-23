using System.ComponentModel.DataAnnotations;

namespace HardwareStoreMng.DTO
{
    public class ProductDTO
    {
        public int productId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Product Name ")]
        [MaxLength(50, ErrorMessage = "Max length of Product name is 50 char")]
        public string productName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product Description is nessesry")]

        public string productDescription { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product Price is nessesry")]

        public decimal price { get; set; }
    }
}
