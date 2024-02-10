namespace BusinessLayer.Model;

public class CustomerBA
{
    public Guid CustomerId { get; set; }
    public string FullName { get; set; }
    public DateOnly DateOfBirth { get; set; }

    public byte[] Avatar { get; set; }
}