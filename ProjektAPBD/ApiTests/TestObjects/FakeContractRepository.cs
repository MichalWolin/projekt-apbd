using Api.Interfaces;
using Api.Models;
using Api.RequestModels;
using Api.ResponseModels;

namespace ApiTests.TestObjects;

public class FakeContractRepository : IContractRepository
{
    private ICollection<Contract> _contracts;

    public FakeContractRepository()
    {
        _contracts = new List<Contract>
        {

        };
    }
    public Task<Contract?> GetCustomersContract(int customerId, int softwareId, CancellationToken cancellationToken)
    {
        var contract = _contracts.FirstOrDefault(c => c.CustomerId.Equals(customerId)
                                                      && c.SoftwareId.Equals(softwareId)
                                                      && c.SupportEndDate > DateTime.Now
                                                      && c.Signed.Equals(true));

        return Task.FromResult(contract);
    }

    public Task<bool> IsCustomerReturning(int customerId, CancellationToken cancellationToken)
    {
        var result = _contracts.Any(c => c.CustomerId.Equals(customerId) && c.Signed.Equals(true));

        return Task.FromResult(result);
    }

    public Task<ContractDto> CreateContract(NewContractDto newContractDto, decimal price, int discount,
        CancellationToken cancellationToken)
    {
        var additionalSupport = newContractDto.AdditionalSupport ?? 0;

        var contract = new Contract
        {
            CustomerId = newContractDto.CustomerId,
            SoftwareId = newContractDto.SoftwareId,
            StartDate = newContractDto.StartDate,
            EndDate = newContractDto.EndDate,
            Price = decimal.Round(price - (price * discount / 100), 2) + additionalSupport * 1000,
            Paid = 0,
            SupportEndDate = DateTime.Today.AddYears(additionalSupport),
            Signed = false
        };

        _contracts.Add(contract);

        return Task.FromResult(new ContractDto
        {
            ContractId = contract.ContractId,
            CustomerId = contract.CustomerId,
            SoftwareId = contract.SoftwareId,
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            Price = contract.Price,
            SupportEndDate = contract.SupportEndDate,
        });
    }

    public Task<Contract?> GetContract(int contractId, CancellationToken cancellationToken)
    {
        return Task.FromResult(_contracts.FirstOrDefault(c => c.ContractId.Equals(contractId)));
    }

    public Task<PaymentResponseDto> PayForContract(PaymentRequestDto paymentRequestDto,
        CancellationToken cancellationToken)
    {
        var contract = _contracts.FirstOrDefault(c => c.ContractId.Equals(paymentRequestDto.ContractId));

        contract.Paid += paymentRequestDto.Amount;

        if (contract.Paid >= contract.Price)
            contract.Signed = true;

        return Task.FromResult(new PaymentResponseDto
        {
            ContractId = contract.ContractId,
            Paid = contract.Paid,
            Price = contract.Price,
        });
    }

    public Task<decimal> GetIncomeForSoftware(int softwareId, bool anticipatedIncomes,
        CancellationToken cancellationToken)
    {
        var incomeFromSignedContracts = _contracts
            .Where(c => c.SoftwareId.Equals(softwareId) && c.Signed.Equals(true))
            .Sum(c => c.Price);

        if (anticipatedIncomes)
        {
            var incomeFromUnsignedContracts = _contracts
                .Where(c => c.SoftwareId.Equals(softwareId) && c.Signed.Equals(false)
                                                            && c.EndDate > DateTime.Now)
                .Sum(c => c.Price);

            return Task.FromResult(incomeFromSignedContracts + incomeFromUnsignedContracts);
        }

        return Task.FromResult(incomeFromSignedContracts);
    }

    public Task<decimal> GetWholeIncome(bool anticipatedIncomes, CancellationToken cancellationToken)
    {
        var incomeFromSignedContracts = _contracts
            .Where(c => c.Signed.Equals(true))
            .Sum(c => c.Price);

        if (anticipatedIncomes)
        {
            var incomeFromUnsignedContracts = _contracts
                .Where(c => c.Signed.Equals(false) && c.EndDate > DateTime.Now)
                .Sum(c => c.Price);

            return Task.FromResult(incomeFromSignedContracts + incomeFromUnsignedContracts);
        }

        return Task.FromResult(incomeFromSignedContracts);
    }
}