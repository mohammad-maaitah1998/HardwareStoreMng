using System.ComponentModel.DataAnnotations;

namespace HardwareStoreMng.DTO
{
    public class EmployeeDTO
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "we need Id ")]
        [MaxLength(50,ErrorMessage ="Max length of Employee name is 50 char")]
        public string EmployeeName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter The Employee Posetion  ")]
        public string password { get; set; }
        public string EmployeePosetion { get; set; }
    }
}
