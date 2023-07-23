using HardwareStoreMng.DTO;

namespace HardwareStoreMng.Repositories.CustomerRepository
{
    public interface CustomerInterface
    {
        IEnumerable<CustomerDTO> GetAllCustomers();
        CustomerDTO GetCustomerById(int CustomerId);
        void AddCustomer(CustomerDTO customerDto);
        void UpdateCustomer(CustomerDTO customerDto);
        void DeleteCustomer(int customerid);
    }
}
