using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using BusinessLayer.Interfaces;

namespace CustomerFunction;

public  class CustomerAvatarFunction
{
    private readonly ICustomerUnitOfWork _customerUnitOfWork;

    public CustomerAvatarFunction(ICustomerUnitOfWork customerUnitOfWork)
    {
        _customerUnitOfWork = customerUnitOfWork;
    }

    [FunctionName("customerAvatar")]
    public  async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "avatar/{id}")] HttpRequest req,
        Guid id,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        var customer = _customerUnitOfWork.GetCustomerById(id);
        return new FileContentResult(customer.Avatar, "image/png");
    }
}