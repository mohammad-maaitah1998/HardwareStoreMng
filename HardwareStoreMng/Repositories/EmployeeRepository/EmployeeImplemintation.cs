using HardwareStoreMng.Context;
using HardwareStoreMng.DTO;
using HardwareStoreMng.Models;
using Microsoft.EntityFrameworkCore;

namespace HardwareStoreMng.Repositories.EmployeeRepository
{
    public class EmployeeImplemintation : EmployeeInterface
    {
        public readonly HardwareStoreMngContext _context;

        public EmployeeImplemintation(HardwareStoreMngContext context)
        {
            _context = context;
        }

        public void AddEmployee(EmployeeDTO employeeDto)
        {
            var newEmployee = new Employee
            {
                EmployeeId = employeeDto.EmployeeId,
                EmployeeName = employeeDto.EmployeeName,
                EmployeePosetion = employeeDto.EmployeePosetion,
                password = employeeDto.password


            };


            _context.Employee.Add(newEmployee);
            _context.SaveChanges();
        }

        public void DeleteEmployee(int employeeId)
        {
            var existingEmployee = _context.Employee.Find(employeeId);

            if (existingEmployee == null)
            {

                return;
            }

            _context.Employee.Remove(existingEmployee);
            _context.SaveChanges();
        }

        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            var employee = _context.Employee.ToList();
            var employeeDtos = employee.Select(p => new EmployeeDTO
            {
                EmployeeId = p.EmployeeId,
                EmployeeName = p.EmployeeName,
                EmployeePosetion = p.EmployeePosetion,
                password= p.password


            });
            return employeeDtos;
        }

        public EmployeeDTO GetEmployeeById(int employeeId)
        {
            var employee = _context.Employee.FirstOrDefault(p => p.EmployeeId== employeeId);

            if (employee == null)
            {
                return null;
            }


            var employeeDto = new EmployeeDTO
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                EmployeePosetion = employee.EmployeePosetion,
                password= employee.password

            };

            return employeeDto;
        }

        public void UpdateEmployee(EmployeeDTO employeeDto)
        {
            var existingEmployee= _context.Employee.Find(employeeDto.EmployeeId);

            if (existingEmployee == null)
            {
                return;
            }
            existingEmployee.EmployeePosetion = employeeDto.EmployeePosetion;
            _context.SaveChanges();

        }
    }
}
