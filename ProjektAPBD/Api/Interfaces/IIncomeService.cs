using Api.ResponseModels;

namespace Api.Interfaces;

public interface IIncomeService
{
    Task<IncomeDto> GetIncome(int? softwareId, bool anticipatedIncomes, string currency,
        CancellationToken cancellationToken);
}