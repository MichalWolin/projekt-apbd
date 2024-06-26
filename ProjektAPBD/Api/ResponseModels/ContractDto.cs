namespace Api.ResponseModels;

public class ContractDto
{
    public int ContractId { get; set; }
    public int SoftwareId { get; set; }
    public int CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public DateTime SupportEndDate { get; set; }
}