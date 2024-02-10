using BusinessLayer.Model;
using DataAccess.Models;

namespace BusinessLayer.Infrastructure;

public static class CustomerMapper
{
    public static Customer AsCustomer(this CustomerBA customer)
    {
        return new Customer
        {
            DateOfBirth = customer.DateOfBirth,
            FullName = customer.FullName,
            CustomerId = customer.CustomerId,
            Avatar = customer.Avatar
        };
    }

    public static Customer AsCustomer(this CustomerDto customer)
    {
        return new Customer
        {
            DateOfBirth = customer.DateOfBirth,
            FullName = customer.FullName
        };
    }

    public static CustomerBA AsBACustomer(this Customer customer)
    {
        return new CustomerBA
        {
            DateOfBirth = customer.DateOfBirth,
            FullName = customer.FullName,
            CustomerId = customer.CustomerId,
            Avatar = customer.Avatar
        };
    }

    public static List<CustomerBA> AsBACustomerList(this List<Customer> customers)
    {
        List<CustomerBA> outputList = new List<CustomerBA>();
            
        foreach (Customer customer in customers)
        {
            outputList.Add(customer.AsBACustomer());
        }

        return outputList;
    }
}