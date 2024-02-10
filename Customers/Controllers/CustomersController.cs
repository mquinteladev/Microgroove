using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Customers.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerUnitOfWork _customerUnitOfWork;
    private IValidator<CustomerDto> _validator;

    public CustomersController(ICustomerUnitOfWork customerUnitOfWork, IValidator<CustomerDto> validator)
    {
        _customerUnitOfWork = customerUnitOfWork;
        _validator = validator;
    }

    [HttpPost]
    public IEnumerable<CustomerBA> Get()
    {
        return _customerUnitOfWork.GetCustomers();
    }

    [HttpGet("{id}")]
    public CustomerBA GetByGuid(Guid id)
    {
        return _customerUnitOfWork.GetCustomerById(id);
    }

    [HttpGet("{age:int}")]
    public List<CustomerBA> GetByGuid(int age)
    {
        return _customerUnitOfWork.GetCustomersByAge(age);
    }

    [HttpGet("avatar/{id}")]
    public IActionResult GetAvatar(Guid id)
    {
        var customer = _customerUnitOfWork.GetCustomerById(id); ;
        return File(customer.Avatar, "image/png", $"{id}.png");
    }

    [HttpPost("insert")]
    public async  Task<IResult> Post(CustomerDto customer)
    {
        var validationResult = await _validator.ValidateAsync(customer);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var entity = await _customerUnitOfWork.AddCustomer(customer);
        return Results.Created($"/{entity.CustomerId}", entity);
    }
}