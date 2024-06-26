using System.ComponentModel.DataAnnotations;

namespace Api.RequestModels;

public class NewCompanyCustomerDto
{
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public string Krs { get; set; }
}