using Api.Data;
using Api.Interfaces;
using Api.Models;
using Api.RequestModels;
using Api.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class ContractRepository : IContractRepository
{
    private readonly DatabaseContext _context;

    public ContractRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Contract?> GetCustomersContract(int customerId, int softwareId,
        CancellationToken cancellationToken)
    {
        var contract = await _context.Contracts
            .FirstOrDefaultAsync(c => c.CustomerId.Equals(customerId) && c.SoftwareId.Equals(softwareId)
            && c.SupportEndDate > DateTime.Now && c.Signed.Equals(true));

        return contract;
    }

    public async Task<bool> IsCustomerReturning(int customerId, CancellationToken cancellationToken)
    {
        var result = await _context.Contracts
            .AnyAsync(c => c.CustomerId.Equals(customerId) && c.Signed.Equals(true));

        return result;
    }

    public async Task<ContractDto> CreateContract(NewContractDto newContractDto, decimal price, int discount,
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
            SupportEndDate = DateTime.Today.AddYears(1 + additionalSupport),
            Signed = false
        };

        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();

        return new ContractDto
        {
            ContractId = contract.ContractId,
            CustomerId = contract.CustomerId,
            SoftwareId = contract.SoftwareId,
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            Price = contract.Price,
            SupportEndDate = contract.SupportEndDate,
        };
    }

    public async Task<Contract?> GetContract(int contractId, CancellationToken cancellationToken)
    {
        return await _context.Contracts
            .FirstOrDefaultAsync(c => c.ContractId.Equals(contractId));
    }

    public async Task<PaymentResponseDto> PayForContract(PaymentRequestDto paymentRequestDto,
        CancellationToken cancellationToken)
    {
        var contract = await _context.Contracts
            .FirstOrDefaultAsync(c => c.ContractId.Equals(paymentRequestDto.ContractId));

        contract.Paid += paymentRequestDto.Amount;

        if (contract.Paid >= contract.Price)
            contract.Signed = true;

        await _context.SaveChangesAsync();

        return new PaymentResponseDto
        {
            ContractId = contract.ContractId,
            Paid = contract.Paid,
            Price = contract.Price,
        };
    }

    public async Task<decimal> GetIncomeForSoftware(int softwareId, bool anticipatedIncomes,
        CancellationToken cancellationToken)
    {
        var incomeFromSignedContracts = await _context.Contracts
            .Where(c => c.SoftwareId.Equals(softwareId) && c.Signed.Equals(true))
            .SumAsync(c => c.Price);

        if (anticipatedIncomes)
        {
            var incomeFromUnsignedContracts = await _context.Contracts
                .Where(c => c.SoftwareId.Equals(softwareId) && c.Signed.Equals(false)
                                                            && c.EndDate > DateTime.Now)
                .SumAsync(c => c.Price);

            return incomeFromSignedContracts + incomeFromUnsignedContracts;
        }

        return incomeFromSignedContracts;
    }

    public async Task<decimal> GetWholeIncome(bool anticipatedIncomes, CancellationToken cancellationToken)
    {
        var incomeFromSignedContracts = await _context.Contracts
            .Where(c => c.Signed.Equals(true))
            .SumAsync(c => c.Price);

        if (anticipatedIncomes)
        {
            var incomeFromUnsignedContracts = await _context.Contracts
                .Where(c => c.Signed.Equals(false) && c.EndDate > DateTime.Now)
                .SumAsync(c => c.Price);

            return incomeFromSignedContracts + incomeFromUnsignedContracts;
        }

        return incomeFromSignedContracts;
    }
}