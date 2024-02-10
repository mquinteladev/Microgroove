namespace DataAccess.Models;

public class Customer
{
    public Guid CustomerId { get; set; }
    public string FullName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public byte[] Avatar { get; set; }

}