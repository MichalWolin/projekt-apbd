namespace Api.ResponseModels;

public class IncomeDto
{
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public int? SoftwareId { get; set; }
}