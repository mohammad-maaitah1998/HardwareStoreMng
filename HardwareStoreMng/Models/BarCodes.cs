using System.ComponentModel.DataAnnotations;

namespace HardwareStoreMng.Models
{
    public class BarCodes
    {

        [Key]
        public int BarCodeId { get; set; }

        public int ProductId { get; set; }
        public string BarCodeName { get; set; }
        public virtual Product Product { get; set; }
    }
}
