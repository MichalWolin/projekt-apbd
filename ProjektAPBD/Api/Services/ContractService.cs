using Api.Exceptions;
using Api.Interfaces;
using Api.Models;
using Api.RequestModels;
using Api.ResponseModels;

namespace Api.Services;

public class ContractService : IContractService
{
    private readonly ISoftwareRepository _softwareRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IContractRepository _contractRepository;
    private readonly IDiscountRepository _discountRepository;

    public ContractService(ISoftwareRepository softwareRepository, ICustomerRepository customerRepository,
                            IContractRepository contractRepository, IDiscountRepository discountRepository)
    {
        _softwareRepository = softwareRepository;
        _customerRepository = customerRepository;
        _contractRepository = contractRepository;
        _discountRepository = discountRepository;
    }

    public async Task<ContractDto> CreateContract(NewContractDto newContractDto, CancellationToken cancellationToken)
    {
        Software? software = await _softwareRepository.GetSoftware(newContractDto.SoftwareId, cancellationToken);
        EnsureSoftwareExists(software, newContractDto.SoftwareId);

        Customer? customer = await _customerRepository.GetCustomer(newContractDto.CustomerId, cancellationToken);
        EnsureCustomerExists(customer, newContractDto.CustomerId);
        EnsureCustomerIsNotDeleted(customer!);

        Contract? contract = await _contractRepository
            .GetCustomersContract(newContractDto.CustomerId, newContractDto.SoftwareId, cancellationToken);
        EnsureCustomerDoesntHaveExistingContractForSoftware(contract,
                                                        newContractDto.CustomerId, newContractDto.SoftwareId);

        EnsureStartDateIsNotInThePast(newContractDto);
        EnsureStartDateIsOlderThanEndDate(newContractDto);
        EnsureContractLengthIsAtLeast3Days(newContractDto);
        EnsureContractLengthIsMax30Days(newContractDto);
        EnsureAdditionalSupportIsValid(newContractDto);

        var price = software.Price;
        var discount = await _discountRepository.GetDiscount(newContractDto.SoftwareId, cancellationToken);
        if (await _contractRepository.IsCustomerReturning(customer.CustomerId, cancellationToken))
            discount += 5;

        return await _contractRepository.CreateContract(newContractDto, price, discount, cancellationToken);
    }

    public async Task<PaymentResponseDto> PayForContract(PaymentRequestDto paymentRequestDto,
        CancellationToken cancellationToken)
    {
        EnsureAmountIsPositive(paymentRequestDto.Amount);

        Customer? customer = await _customerRepository.GetCustomer(paymentRequestDto.CustomerId, cancellationToken);
        EnsureCustomerExists(customer, paymentRequestDto.CustomerId);
        EnsureCustomerIsNotDeleted(customer!);

        Contract? contract = await _contractRepository.GetContract(paymentRequestDto.ContractId, cancellationToken);
        EnsureContractExists(contract, paymentRequestDto.ContractId);
        EnsureContractBelongsToCustomer(contract, paymentRequestDto.CustomerId);
        EnsureContractIsNotSigned(contract);
        EnsureContractIsBeforeDeadline(contract);

        EnsureAmountIsValid(paymentRequestDto.Amount, contract);

        return await _contractRepository.PayForContract(paymentRequestDto, cancellationToken);
    }

    private static void EnsureSoftwareExists(Software? software, int softwareId)
    {
        if (software is null)
        {
            throw new DomainException($"Software with id {softwareId} does not exist!");
        }
    }

    private static void EnsureCustomerExists(Customer? customer, int customerId)
    {
        if (customer is null)
        {
            throw new DomainException($"Customer with id {customerId} does not exist!");
        }
    }

    private void EnsureCustomerDoesntHaveExistingContractForSoftware(Contract? contract, int customerId, int softwareId)
    {
        if (contract is not null)
        {
            throw new DomainException($"Customer with id {customerId} already has an active " +
                                      $"contract for software with id {softwareId}!");
        }
    }

    private static void EnsureContractLengthIsAtLeast3Days(NewContractDto newContractDto)
    {
        if (newContractDto.EndDate.Subtract(newContractDto.StartDate).Days < 3)
        {
            throw new DomainException("Contract length must be at least 3 days!");
        }
    }

    private static void EnsureContractLengthIsMax30Days(NewContractDto newContractDto)
    {
        if (newContractDto.EndDate.Subtract(newContractDto.StartDate).Days > 30)
        {
            throw new DomainException("Contract length must be at most 30 days!");
        }
    }

    private static void EnsureStartDateIsNotInThePast(NewContractDto newContractDto)
    {
        if (newContractDto.StartDate < DateTime.Now)
        {
            throw new DomainException("Start date cannot be in the past!");
        }
    }

    private static void EnsureStartDateIsOlderThanEndDate(NewContractDto newContractDto)
    {
        if (newContractDto.StartDate > newContractDto.EndDate)
        {
            throw new DomainException("Start date must be older than end date!");
        }
    }

    private static void EnsureAdditionalSupportIsValid(NewContractDto newContractDto)
    {
        if (newContractDto.AdditionalSupport is < 0 or > 3)
        {
            throw new DomainException("Additional support must be between 0 and 3 years!");
        }
    }

    private static void EnsureCustomerIsNotDeleted(Customer customer)
    {
        if (customer.IsDeleted)
        {
            throw new DomainException("Customer is deleted!");
        }
    }

    private static void EnsureAmountIsPositive(decimal amount)
    {
        if (amount <= 0)
        {
            throw new DomainException("Amount must be more than 0!");
        }
    }

    private static void EnsureContractExists(Contract? contract, int contractId)
    {
        if (contract is null)
        {
            throw new DomainException($"Contract with id {contractId} does not exist!");
        }
    }

    private static void EnsureContractBelongsToCustomer(Contract contract, int customerId)
    {
        if (contract.CustomerId != customerId)
        {
            throw new DomainException($"Contract with id {contract.ContractId} " +
                                      $"does not belong to customer with id {customerId}!");
        }
    }

    private static void EnsureContractIsNotSigned(Contract contract)
    {
        if (contract.Signed)
        {
            throw new DomainException($"Contract with id {contract.ContractId} is already signed!");
        }
    }

    private static void EnsureContractIsBeforeDeadline(Contract contract)
    {
        if (contract.SupportEndDate < DateTime.Now)
        {
            throw new DomainException($"Contract with id {contract.ContractId} is past the deadline!");
        }
    }

    private static void EnsureAmountIsValid(decimal amount, Contract contract)
    {
        if (amount > contract.Price - contract.Paid)
        {
            throw new DomainException("Amount is more than the remaining price!");
        }
    }
}