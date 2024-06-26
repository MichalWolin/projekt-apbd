using System.ComponentModel.DataAnnotations;

namespace Api.RequestModels;

public class NewCompanyCustomerDto
{
    [Required]
    public string Address { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Krs { get; set; }
}