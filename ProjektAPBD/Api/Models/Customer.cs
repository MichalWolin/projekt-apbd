namespace Api.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public int? PersonId { get; set; }
    public int? CompanyId { get; set; }

    public Person? Person { get; set; }
    public Company? Company { get; set; }

    public ICollection<Contract> Contracts { get; set; } = new HashSet<Contract>();
}