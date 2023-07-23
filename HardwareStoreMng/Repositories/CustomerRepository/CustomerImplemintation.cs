using HardwareStoreMng.Context;
using HardwareStoreMng.DTO;
using HardwareStoreMng.Models;
using Microsoft.EntityFrameworkCore;

namespace HardwareStoreMng.Repositories.CustomerRepository
{
    public class CustomerImplemintation : CustomerInterface
    {
        public readonly HardwareStoreMngContext _context;

        public CustomerImplemintation(HardwareStoreMngContext context)
        {
            _context = context;
        }

        public void AddCustomer(CustomerDTO customerDto)
        {

            var newCustomer = new Customer
            {
                CustomerId = customerDto.CustomerId,
                CustomerName = customerDto.CustomerName,
                CustomerEmail = customerDto.CustomerEmail,
                CustomerPhone = customerDto.CustomerPhone,

            };


            _context.Customer.Add(newCustomer);
            _context.SaveChanges();

        }

        public void DeleteCustomer(int customerid)
        {
            var existingCustomer = _context.Customer.Find(customerid);

            if (existingCustomer == null)
            {

                return;
            }

            _context.Customer.Remove(existingCustomer);
            _context.SaveChanges();
        }

        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
            var customers = _context.Customer.ToList();
            var customersDtos = customers.Select(p => new CustomerDTO
            {
                CustomerId = p.CustomerId,
                CustomerName = p.CustomerName,
                CustomerEmail = p.CustomerEmail,
                CustomerPhone = p.CustomerPhone

            });
            return customersDtos;

        }

        public CustomerDTO GetCustomerById(int CustomerId)
        {
            var customer = _context.Customer.FirstOrDefault(p => p.CustomerId == CustomerId);

            if (customer == null)
            {
                return null;
            }
            var customerDto = new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                CustomerEmail = customer.CustomerEmail,
                CustomerPhone = customer.CustomerPhone
            };

            return customerDto;
        }

        public void UpdateCustomer(CustomerDTO customerDto)
        {

            var existingCustomer = _context.Customer.Find(customerDto.CustomerId);

            if (existingCustomer == null)
            {
                return;
            }

            existingCustomer.CustomerEmail = customerDto.CustomerEmail;
            existingCustomer.CustomerPhone = customerDto.CustomerPhone;
            _context.SaveChanges();
        }
    }
}
