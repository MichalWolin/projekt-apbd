namespace Api.RequestModels;

public class PaymentRequestDto
{
    public int CustomerId { get; set; }
    public int ContractId { get; set; }
    public decimal Amount { get; set; }
}