namespace Api.RequestModels;

public class NewContractDto
{
    public int SoftwareId { get; set; }
    public int CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? AdditionalSupport { get; set; }
}