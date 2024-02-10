using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class CustomerDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public CustomerDbContext()
    {
    }

    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
    {
    }
}