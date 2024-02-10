using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BusinessLayer.Model;
using BusinessLayer.Interfaces;

namespace CustomerFunction;

public  class CustomerAddFunction
{
    private readonly ICustomerUnitOfWork _customerUnitOfWork;

    public CustomerAddFunction(ICustomerUnitOfWork customerUnitOfWork)
    {
        _customerUnitOfWork = customerUnitOfWork;
    }

    [FunctionName("customerAdd")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "insert")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");
            
        try
        {
            var content = await new StreamReader(req.Body).ReadToEndAsync();
            CustomerDto customer = JsonConvert.DeserializeObject<CustomerDto>(content);
            var entity = await _customerUnitOfWork.AddCustomer(customer);
            return new OkObjectResult(entity);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }
}