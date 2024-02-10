using BusinessLayer.Infrastructure;
using DataAccess;
using DataAccess.Models;

namespace Customers.Infrastructure;

public static class WebApplicationExtension
{
    public static async Task AddInMemoryDatabaseSeed(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetService<CustomerDbContext>();


        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        var customer1 = new Customer
        {
            CustomerId = new Guid(),
            DateOfBirth = new DateOnly(1985,04,27),
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