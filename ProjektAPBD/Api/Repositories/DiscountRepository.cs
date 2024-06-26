using Api.Data;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly DatabaseContext _context;

    public DiscountRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<int> GetDiscount(int softwareId)
    {
        var discount = await _context.Discounts
            .Where(d => d.SoftwareId
                .Equals(softwareId) && d.StartDate < DateTime.Now && d.EndDate > DateTime.Now)
            .Select(d => d.Rate)
            .MaxAsync();

        return discount;
    }
}