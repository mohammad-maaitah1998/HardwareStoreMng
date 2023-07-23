using System.ComponentModel.DataAnnotations;

namespace HardwareStoreMng.DTO
{
    public class BarCodesDTO
    {
        [Key]
        public int BarCodeId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Product ID please ")]
        public int ProductId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Product ID please ")]

        public string BarCodeName { get; set; }
    }
}
