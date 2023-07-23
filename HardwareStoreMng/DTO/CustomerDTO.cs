using System.ComponentModel.DataAnnotations;

namespace HardwareStoreMng.DTO
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Customer Name ")]
        [MaxLength(50, ErrorMessage = "Max length of Customer name is 50 char")]
        public string CustomerName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Customer Email ")]
        [MinLength(10, ErrorMessage = "Min length of Customer Email is 10 char")]
        public string CustomerEmail { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Customer Phone ")]
        [MinLength(8, ErrorMessage = "Min length of Customer Phone is 8 number")]

        [RegularExpression("^[07]{2}[7-9]{1}[0-9]{7}",ErrorMessage ="Enter phone number in the jordanian format")]
        public string CustomerPhone { get; set; }
    }
}
