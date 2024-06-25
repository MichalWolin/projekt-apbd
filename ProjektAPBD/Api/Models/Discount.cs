namespace Api.Models;

public class Discount
{
    public int DiscountId { get; set; }
    public int SoftwareId { get; set; }
    public int Rate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; } = string.Empty;

    public Software Software { get; set; } = null!;
}