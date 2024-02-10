using BusinessLayer.Interfaces;
using DataAccess;
using DataAccess.Models;
using System.Linq.Expressions;

namespace BusinessLayer.Repositories;

public class CustomerRepository : ICustomerRepository, IDisposable
{
    private readonly CustomerDbContext _context;

    public CustomerRepository(CustomerDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Customer> GetCustomers()
    {
        try
        {
            return _context.Customers.ToList();
        }
        catch (Exception ex)
        {

            throw;
        }
           
    }

    public Customer GetCustomerById(Guid customerId)
    {
        return  _context.Customers.Find(customerId);
    }

    public Customer InsertCustomer(Customer? customer)
    {
        if (customer != null)
        {
            var entity = _context.Customers.Add(customer);
            return entity.Entity;
        }

        return null;
    }

    public void DeleteCustomer(int customerId)
    {
        throw new NotImplementedException();
    }

    public void UpdateCustomer(Customer customer)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Customer> FilterBy(Expression<Func<Customer, Boolean>> predicate)
    {
        return _context.Customers.Where(predicate);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}