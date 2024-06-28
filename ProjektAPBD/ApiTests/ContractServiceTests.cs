using Api.Exceptions;
using Api.Interfaces;
using Api.RequestModels;
using Api.Services;
using ApiTests.TestObjects;
using Shouldly;

namespace ApiTests;

public class ContractServiceTests
{
    private readonly IContractService _contractService;

    public ContractServiceTests()
    {
        _contractService = new ContractService(new FakeSoftwareRepository(), new FakeCustomerRepository(),
            new FakeContractRepository(), new FakeDiscountRepository());
    }

    [Fact]
    public async Task Should_ThrowException_WhenSoftwareDoesntExist()
    {
        var newContractDto = new NewContractDto
        {
            SoftwareId = 3,
            CustomerId = 1,
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(2),
            AdditionalSupport = 1
        };

        await Should.ThrowAsync<DomainException>
            (_contractService.CreateContract(newContractDto, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenCustomerDoesntExist()
    {
        var newContractDto = new NewContractDto
        {
            SoftwareId = 1,
            CustomerId = 4,
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(2),
            AdditionalSupport = 1
        };

        await Should.ThrowAsync<DomainException>
            (_contractService.CreateContract(newContractDto, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenCustomerIsDeleted()
    {
        var newContractDto = new NewContractDto
        {
            SoftwareId = 1,
            CustomerId = 3,
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(2),
            AdditionalSupport = 1
        };

        await Should.ThrowAsync<DomainException>
            (_contractService.CreateContract(newContractDto, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenCustomerHasExistingContractForSoftware()
    {
        var newContractDto = new NewContractDto
        {
            SoftwareId = 1,
            CustomerId = 1,
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(2),
            AdditionalSupport = 1
        };

        await Should.ThrowAsync<DomainException>
            (_contractService.CreateContract(newContractDto, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenStartDateIsInThePast()
    {
        var newContractDto = new NewContractDto
        {
            SoftwareId = 2,
            CustomerId = 1,
            StartDate = DateTime.Now.AddDays(-1),
            EndDate = DateTime.Now.AddDays(2),
            AdditionalSupport = 1
        };

        await Should.ThrowAsync<DomainException>
            (_contractService.CreateContract(newContractDto, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenEndDateIsBeforeStartDate()
    {
        var newContractDto = new NewContractDto
        {
            SoftwareId = 2,
            CustomerId = 1,
            StartDate = DateTime.Now.AddDays(2),
            EndDate = DateTime.Now.AddDays(1),
            AdditionalSupport = 1
        };

        await Should.ThrowAsync<DomainException>
            (_contractService.CreateContract(newContractDto, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenContractLengthIsLessThan3Days()
    {
        var newContractDto = new NewContractDto
        {
            SoftwareId = 2,
            CustomerId = 1,
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(1),
            AdditionalSupport = 1
        };

        await Should.ThrowAsync<DomainException>
            (_contractService.CreateContract(newContractDto, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenContractLengthIsMoreThan30Days()
    {
        var newContractDto = new NewContractDto
        {
            SoftwareId = 2,
            CustomerId = 1,
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(32),
            AdditionalSupport = 1
        };

        await Should.ThrowAsync<DomainException>
            (_contractService.CreateContract(newContractDto, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenAdditionalSupportIsInvalid()
    {
        var newContractDto = new NewContractDto
        {
            SoftwareId = 2,
            CustomerId = 1,
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(10),
            AdditionalSupport = 4
        };

        await Should.ThrowAsync<DomainException>
            (_contractService.CreateContract(newContractDto, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ReturnContractDto_WhenContractIsCreated()
    {
        var newContractDto = new NewContractDto
        {
            SoftwareId = 2,
            CustomerId = 1,
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(10),
            AdditionalSupport = 1
        };

        var contractDto = await _contractService.CreateContract(newContractDto, CancellationToken.None);

        contractDto.ShouldNotBeNull();
        contractDto.CustomerId.ShouldBe(newContractDto.CustomerId);
        contractDto.SoftwareId.ShouldBe(newContractDto.SoftwareId);
        contractDto.StartDate.ShouldBe(newContractDto.StartDate);
        contractDto.EndDate.ShouldBe(newContractDto.EndDate);
        contractDto.SupportEndDate.ShouldBe(DateTime.Today.AddYears(2));
    }

    [Fact]
    public async Task Should_ThrowException_WhenAmountIsNotPositive()
    {
        var paymentRequestDto = new PaymentRequestDto
        {
            CustomerId = 1,
            ContractId = 1,
            Amount = -1
        };

        await Should.ThrowAsync<DomainException>
            (_contractService.PayForContract(paymentRequestDto, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenCustomerDoesntExistPayment()
    {
        var paymentRequestDto = new PaymentRequestDto
        {
            CustomerId = 4,
            ContractId = 1,
            Amount = 1
        };

        await Should.ThrowAsync<DomainException>
            (_contractService.PayForContract(paymentRequestDto, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenCustomerIsDeletedPayment()
    {
        var paymentRequestDto = new PaymentRequestDto
        {
            CustomerId = 3,
            ContractId = 1,
            Amount = 1
        };

        await Should.ThrowAsync<DomainException>
            (_contractService.PayForContract(paymentRequestDto, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenContractDoesntExistPayment()
    {
        var paymentRequestDto = new PaymentRequestDto
        {
            CustomerId = 1,
            ContractId = 4,
            Amount = 1
        };

        await Should.ThrowAsync<DomainException>
            (_contractService.PayForContract(paymentRequestDto, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenContractDoesntBelongToCustomerPayment()
    {
        var paymentRequestDto = new PaymentRequestDto
        {
            CustomerId = 1,
            ContractId = 2,
            Amount = 1
        };

        await Should.ThrowAsync<DomainException>
            (_contractService.PayForContract(paymentRequestDto, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenContractIsAlreadySignedPayment()
    {
        var paymentRequestDto = new PaymentRequestDto
        {
            CustomerId = 1,
            ContractId = 1,
            Amount = 1
        };

        await Should.ThrowAsync<DomainException>
            (_contractService.PayForContract(paymentRequestDto, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenContractIsAfterDealine()
    {
        var paymentRequestDto = new PaymentRequestDto
        {
            CustomerId = 1,
            ContractId = 3,
            Amount = 1
        };

        await Should.ThrowAsync<DomainException>
            (_contractService.PayForContract(paymentRequestDto, CancellationToken.None));
    }

    [Fact]
    public async Task Shoud_ThrowException_WhenAmountIsInvalid()
    {
        var paymentRequestDto = new PaymentRequestDto
        {
            CustomerId = 2,
            ContractId = 2,
            Amount = 1000
        };

        await Should.ThrowAsync<DomainException>
            (_contractService.PayForContract(paymentRequestDto, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ReturnPaymentResponseDto_WhenPaymentIsMade()
    {
        var paymentRequestDto = new PaymentRequestDto
        {
            CustomerId = 2,
            ContractId = 2,
            Amount = 1
        };

        var paymentResponseDto = await _contractService.PayForContract(paymentRequestDto, CancellationToken.None);

        paymentResponseDto.ContractId.ShouldBe(paymentRequestDto.ContractId);
        paymentResponseDto.Paid.ShouldBe(paymentRequestDto.Amount);
        paymentResponseDto.Price.ShouldBe(100);
    }
}