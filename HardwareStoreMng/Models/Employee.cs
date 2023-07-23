using System.ComponentModel.DataAnnotations;

namespace HardwareStoreMng.Models
{
    public class Employee
    {

        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
       public string password { get; set; }
        public string EmployeePosetion { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
