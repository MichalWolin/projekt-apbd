using Api.Models;
using Api.RequestModels;
using Api.ResponseModels;

namespace Api.Interfaces;

public interface IContractRepository
{
    Task<Contract?> GetCustomersContract(int customerId, int softwareId);
    Task<bool> IsCustomerReturning(int customerId);
    Task<ContractDto> CreateContract(NewContractDto newContractDto, decimal price, int discount);
    Task<Contract?> GetContract(int contractId);
    Task<PaymentResponseDto> PayForContract(PaymentRequestDto paymentRequestDto);
    Task<decimal> GetIncomeForSoftware(int softwareId, bool anticipatedIncomes);
    Task<decimal> GetWholeIncome(bool anticipatedIncomes);
}