namespace Api.ResponseModels;

public class PaymentResponseDto
{
    public int ContractId { get; set; }
    public decimal Price { get; set; }
    public decimal Paid { get; set; }
}