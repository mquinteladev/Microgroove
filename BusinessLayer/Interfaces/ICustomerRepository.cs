using DataAccess.Models;
using System.Linq.Expressions;

namespace BusinessLayer.Interfaces;

public interface ICustomerRepository
{
    IEnumerable<Customer> GetCustomers();
    Customer GetCustomerById(Guid customerId);
    Customer InsertCustomer(Customer? customer);
    void DeleteCustomer(int customerId);
    void UpdateCustomer(Customer customer);
    IQueryable<Customer> FilterBy(Expression<Func<Customer, Boolean>> predicate);
    void Save();
}