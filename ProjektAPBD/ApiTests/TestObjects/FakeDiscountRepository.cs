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