namespace Api.Models;

public class Company
{
    public int CompanyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Krs { get; set; } = string.Empty;

    public Customer Customer { get; set; } = null!;
}