namespace Api.Interfaces;

public interface IDiscountRepository
{
    Task<int> GetDiscount(int softwareId, CancellationToken cancellationToken);
}