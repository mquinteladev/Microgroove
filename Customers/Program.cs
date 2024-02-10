using BusinessLayer.Interfaces;
using BusinessLayer.Model;
using Customers.Infrastructure;
using DataAccess;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Repositories;
using BusinessLayer.UnitOfWork;
using BusinessLayer.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerUnitOfWork, CustomerUnitOfWork>();
builder.Services.AddScoped<IValidator<CustomerDto>, CustomerValidator>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var keepAliveConnection = new SqliteConnection("DataSource=:memory:");
keepAliveConnection.Open();

builder.Services.AddDbContext<CustomerDbContext>(options =>
{
    options.UseSqlite(keepAliveConnection);
});


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.AddInMemoryDatabaseSeed();

app.Run();
