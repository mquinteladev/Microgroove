using Microsoft.Extensions.DependencyInjection;
using BusinessLayer.Interfaces;
using BusinessLayer.Repositories;
using BusinessLayer.UnitOfWork;
using CustomerFunction.Infrastructure;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using DataAccess;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Model;
using BusinessLayer.Validators;
using FluentValidation;

[assembly: FunctionsStartup(typeof(CustomerFunction.Startup))]
namespace CustomerFunction;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<ICustomerUnitOfWork, CustomerUnitOfWork>();
        builder.Services.AddScoped<IValidator<CustomerDto>, CustomerValidator>();

        var keepAliveConnection = new SqliteConnection("DataSource=:memory:");
        keepAliveConnection.Open();

        builder.Services.AddDbContext<CustomerDbContext>(options =>
        {
            options.UseSqlite(keepAliveConnection);
        });

        _ = builder.AddInMemoryDatabaseSeed(keepAliveConnection);
    }
}