using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using BusinessLayer.Interfaces;

namespace CustomerFunction;

public class CustomerFunction
{
    private readonly ICustomerUnitOfWork _customerUnitOfWork;

    public CustomerFunction(ICustomerUnitOfWork customerUnitOfWork)
    {
        _customerUnitOfWork = customerUnitOfWork;
    }


    [FunctionName("customers")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "customers")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var customers=  _customerUnitOfWork.GetCustomers();
 
        return new OkObjectResult(customers);
    }
}