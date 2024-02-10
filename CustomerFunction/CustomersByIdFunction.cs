using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using BusinessLayer.Interfaces;

namespace CustomerFunction;

public  class CustomersByIdFunction
{

    private readonly ICustomerUnitOfWork _customerUnitOfWork;

    public CustomersByIdFunction(ICustomerUnitOfWork customerUnitOfWork)
    {
        _customerUnitOfWork = customerUnitOfWork;
    }

    [FunctionName("customersById")]
    public  async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customers/{id:guid}")] HttpRequest req,
        Guid id,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var customers = _customerUnitOfWork.GetCustomerById(id);

        return new OkObjectResult(customers);
    }
}