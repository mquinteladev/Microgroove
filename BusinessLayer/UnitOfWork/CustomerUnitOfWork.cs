using BusinessLayer.Infrastructure;
using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using DataAccess.Infrastructure;
using DataAccess.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.UnitOfWork;

public class CustomerUnitOfWork : ICustomerUnitOfWork
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IValidator<CustomerDto> _validator;
    private readonly ILogger _logger;

    public CustomerUnitOfWork(ICustomerRepository customerRepository,IValidator<CustomerDto> validator, ILogger<CustomerUnitOfWork> logger)
    {
        _customerRepository = customerRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<CustomerBA> AddCustomer(CustomerDto customer)
    {

        try
        {
            var validationResult = await _validator.ValidateAsync(customer);
            if (!validationResult.IsValid)
            {
                throw new Exception(String.Join("", validationResult.Errors.Select(p => p.ErrorMessage)));
            }

            Customer customerEntity = customer.AsCustomer();
            customerEntity.CustomerId = new Guid();
            customerEntity.Avatar = await AvatarHelper.GenerateAvatarAsync(customerEntity.FullName);

            var customerEntityInserted = _customerRepository.InsertCustomer(customerEntity);
            _customerRepository.Save();
            return customerEntityInserted.AsBACustomer();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw ex;
        }
    }

    public IEnumerable<CustomerBA> GetCustomers()
    {
        return _customerRepository.GetCustomers().ToList().AsBACustomerList();
    }

    public CustomerBA GetCustomerById(Guid id)
    {
        return _customerRepository.GetCustomerById(id).AsBACustomer();
    }

    public List<CustomerBA> GetCustomersByAge(int age)
    {
        return _customerRepository.GetCustomers().Where(p => p.DateOfBirth.Age() <= age).ToList().AsBACustomerList();
    }

}