using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using BusinessLayer.Interfaces;

namespace CustomerFunction;

public  class CustomersByAgeFunction
{
    private readonly ICustomerUnitOfWork _customerUnitOfWork;

    public CustomersByAgeFunction(ICustomerUnitOfWork customerUnitOfWork)
    {
        _customerUnitOfWork = customerUnitOfWork;
    }

    [FunctionName("customersByAgeFunction")]
    public  async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "customers/{age:int}")] HttpRequest req,
        int age,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");
             

        var customers = _customerUnitOfWork.GetCustomersByAge(age);

        return new OkObjectResult(customers);
    }
}