using Api.Models;
using Api.RequestModels;
using Api.ResponseModels;

namespace Api.Interfaces;

public interface IContractRepository
{
    Task<Contract?> GetCustomersContract(int customerId, int softwareId, CancellationToken cancellationToken);
    Task<bool> IsCustomerReturning(int customerId, CancellationToken cancellationToken);
    Task<ContractDto> CreateContract(NewContractDto newContractDto, decimal price, int discount,
        CancellationToken cancellationToken);
    Task<Contract?> GetContract(int contractId, CancellationToken cancellationToken);
    Task<PaymentResponseDto> PayForContract(PaymentRequestDto paymentRequestDto, CancellationToken cancellationToken);
    Task<decimal> GetIncomeForSoftware(int softwareId, bool anticipatedIncomes, CancellationToken cancellationToken);
    Task<decimal> GetWholeIncome(bool anticipatedIncomes, CancellationToken cancellationToken);
}