namespace Api.ResponseModels;

public class CreatedCompanyCustomerDto
{
    public int CustomerId { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public string Krs { get; set; }
}