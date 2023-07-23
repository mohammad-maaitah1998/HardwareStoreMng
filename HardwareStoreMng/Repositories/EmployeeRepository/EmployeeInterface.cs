using HardwareStoreMng.DTO;

namespace HardwareStoreMng.Repositories.EmployeeRepository
{
    public interface EmployeeInterface
    {
        IEnumerable<EmployeeDTO> GetAllEmployees();
        EmployeeDTO GetEmployeeById(int employeeId);
        void AddEmployee(EmployeeDTO employeeDto);
        void UpdateEmployee(EmployeeDTO employeeDto);
        void DeleteEmployee(int employeeId);
    }
}
