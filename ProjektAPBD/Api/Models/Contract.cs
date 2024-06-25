namespace Api.Models;

public class Contract
{
    public int ContractId { get; set; }
    public int SoftwareId { get; set; }
    public int CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public decimal Paid { get; set; }
    public DateTime SupportEndDate { get; set; }
    public bool Signed { get; set; }

    public Software Software { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
}