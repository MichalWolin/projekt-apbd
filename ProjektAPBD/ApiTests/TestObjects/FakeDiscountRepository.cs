using Api.Interfaces;
using Api.Models;

namespace ApiTests.TestObjects;

public class FakeDiscountRepository : IDiscountRepository
{
    private List<Discount> _discounts;

    public FakeDiscountRepository()
    {
        _discounts = new List<Discount>
        {
            new Discount
            {
                DiscountId = 1,
                SoftwareId = 2,
                Rate = 10,
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(1)
            }
        };
    }

    public Task<int> GetDiscount(int softwareId, CancellationToken cancellationToken)
    {
        var discount = _discounts
            .Where(d => d.SoftwareId
                .Equals(softwareId) && d.StartDate < DateTime.Now && d.EndDate > DateTime.Now)
            .Select(d => d.Rate)
            .Max();

            return Task.FromResult(discount);
    }
}