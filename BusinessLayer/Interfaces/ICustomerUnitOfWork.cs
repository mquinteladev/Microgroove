using BusinessLayer.Model;

namespace BusinessLayer.Interfaces;

public interface ICustomerUnitOfWork
{
    Task<CustomerBA> AddCustomer(CustomerDto customer);
    IEnumerable<CustomerBA> GetCustomers();
    CustomerBA GetCustomerById(Guid id);
    List<CustomerBA> GetCustomersByAge(int age);
}