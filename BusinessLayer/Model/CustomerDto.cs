using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Model;

public class CustomerDto
{
    [Required(ErrorMessage = "FullName is required")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "DateOfBirth is required")]
    public DateOnly DateOfBirth { get; set; }
}