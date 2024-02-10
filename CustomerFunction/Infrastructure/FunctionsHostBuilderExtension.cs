using System;
using System.Threading.Tasks;
using BusinessLayer.Infrastructure;
using DataAccess;
using DataAccess.Models;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CustomerFunction.Infrastructure;

public static class FunctionsHostBuilderExtension
{
    public static async Task AddInMemoryDatabaseSeed(this IFunctionsHostBuilder builder, SqliteConnection keepAliveConnection)
    {
          
        var optionsBuilder = new DbContextOptionsBuilder<CustomerDbContext>();
        optionsBuilder.UseSqlite(keepAliveConnection);

        var db = new CustomerDbContext(optionsBuilder.Options);

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        var customer1 = new Customer
        {
            CustomerId = new Guid(),
            DateOfBirth = new DateOnly(1985, 04, 27),
            FullName = "Michael Quintela",
            Avatar = await AvatarHelper.GenerateAvatarAsync("Michael Quintela")
        };

        var customer2 = new Customer
        {
            CustomerId = new Guid(),
            DateOfBirth = new DateOnly(1946, 06, 14),
            FullName = "Donald Trump",
            Avatar = await AvatarHelper.GenerateAvatarAsync("Donald Trump")
        };

        var customer3 = new Customer
        {
            CustomerId = new Guid(),
            DateOfBirth = new DateOnly(1942, 11, 20),
            FullName = "Joe Biden",
            Avatar = await AvatarHelper.GenerateAvatarAsync("Joe Biden")
        };

        db.Customers.Add(customer1);
        db.Customers.Add(customer2);
        db.Customers.Add(customer3);

        db.SaveChanges();
    }
}