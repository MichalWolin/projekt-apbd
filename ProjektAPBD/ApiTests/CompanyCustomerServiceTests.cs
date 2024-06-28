using Api.Exceptions;
using Api.Interfaces;
using Api.RequestModels;
using Api.ResponseModels;
using Api.Services;
using ApiTests.TestObjects;
using Shouldly;

namespace ApiTests;

public class CompanyCustomerServiceTests
{
    private readonly ICompanyCustomerService _companyCustomerService;

    public CompanyCustomerServiceTests()
    {
        _companyCustomerService = new CompanyCustomerService(new FakeCompanyRepository(), new FakeCustomerRepository());
    }

    [Fact]
    public async Task Should_ThrowException_WhenAddressIsEmpty()
    {
        var command = new NewCompanyCustomerDto
        {
            Address = "",
            Email = "email@email.com",
            PhoneNumber = "123456789",
            Name = "Name",
            Krs = "1234567890"
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.CreateCompanyCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenEmailIsEmpty()
    {
        var command = new NewCompanyCustomerDto
        {
            Address = "Address",
            Email = "",
            PhoneNumber = "123456789",
            Name = "Name",
            Krs = "1234567890"
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.CreateCompanyCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPhoneNumberIsEmpty()
    {
        var command = new NewCompanyCustomerDto
        {
            Address = "Address",
            Email = "email@email.com",
            PhoneNumber = "",
            Name = "Name",
            Krs = "1234567890"
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.CreateCompanyCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenNameIsEmpty()
    {
        var command = new NewCompanyCustomerDto
        {
            Address = "Address",
            Email = "email@email.com",
            PhoneNumber = "123456789",
            Name = "",
            Krs = "1234567890"
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.CreateCompanyCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenKrsIsEmpty()
    {
        var command = new NewCompanyCustomerDto
        {
            Address = "Address",
            Email = "email@email.com",
            PhoneNumber = "123456789",
            Name = "Name",
            Krs = ""
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.CreateCompanyCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenKrsIsNotDigitsOnly()
    {
        var command = new NewCompanyCustomerDto
        {
            Address = "Address",
            Email = "email@email.com",
            PhoneNumber = "123456789",
            Name = "Name",
            Krs = "123a123"
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.CreateCompanyCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenKrsLengthIsNotCorrect()
    {
        var command = new NewCompanyCustomerDto
        {
            Address = "Address",
            Email = "email@email.com",
            PhoneNumber = "123456789",
            Name = "Name",
            Krs = "123456789"
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.CreateCompanyCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenCompanyExists()
    {
        var command = new NewCompanyCustomerDto
        {
            Address = "Address",
            Email = "email@email.com",
            PhoneNumber = "123456789",
            Name = "Name",
            Krs = "1234567890"
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.CreateCompanyCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenEmailIsNotCorrect()
    {
        var command = new NewCompanyCustomerDto
        {
            Address = "Address",
            Email = "emailemail.com",
            PhoneNumber = "123456789",
            Name = "Name",
            Krs = "1234567891"
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.CreateCompanyCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPhoneNumberIsNotDigitsOnly()
    {
        var command = new NewCompanyCustomerDto
        {
            Address = "Address",
            Email = "email@email.com",
            PhoneNumber = "1234a6789",
            Name = "Name",
            Krs = "1234567891"
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.CreateCompanyCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPhoneNumberLengthIsNotCorrect()
    {
        var command = new NewCompanyCustomerDto
        {
            Address = "Address",
            Email = "email@email.com",
            PhoneNumber = "12345678901",
            Name = "Name",
            Krs = "1234567891"
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.CreateCompanyCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ReturnCompanyCustomerDto_WhenCompanyCustomerIsCreated()
    {
        var command = new NewCompanyCustomerDto
        {
            Address = "Address",
            Email = "email@email.com",
            PhoneNumber = "123456789",
            Name = "Name",
            Krs = "1234567891"
        };

        var result = await _companyCustomerService
            .CreateCompanyCustomer(command, CancellationToken.None);

        result.ShouldBeOfType<CompanyCustomerDto>();
        result.Address.ShouldBe(command.Address);
        result.Email.ShouldBe(command.Email);
        result.PhoneNumber.ShouldBe(command.PhoneNumber);
        result.Name.ShouldBe(command.Name);
        result.Krs.ShouldBe(command.Krs);
    }

    [Fact]
    public async Task Should_ThrowException_WhenCustomerDoesNotExist()
    {
        var command = new UpdateCustomerCompanyDto
        {
            Address = "Address",
            Email = "email@email.com",
            PhoneNumber = "123456789",
            Name = "Name"
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.UpdateCompanyCustomer(4, command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenCustomerIsNotCompany()
    {
        var command = new UpdateCustomerCompanyDto
        {
            Address = "Address",
            Email = "email@email.com",
            PhoneNumber = "123456789",
            Name = "Name"
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.UpdateCompanyCustomer(2, command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenAddressIsEmptyOnUpdate()
    {
        var command = new UpdateCustomerCompanyDto
        {
            Address = "",
            Email = "mail@mail.com",
            PhoneNumber = "123456789",
            Name = "Name"
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.UpdateCompanyCustomer(1, command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenEmailIsEmptyOnUpdate()
    {
        var command = new UpdateCustomerCompanyDto
        {
            Address = "Address",
            Email = "",
            PhoneNumber = "123456789",
            Name = "Name"
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.UpdateCompanyCustomer(1, command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPhoneNumberIsEmptyOnUpdate()
    {
        var command = new UpdateCustomerCompanyDto
        {
            Address = "Address",
            Email = "email@email.com",
            PhoneNumber = "",
            Name = "Name"
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.UpdateCompanyCustomer(1, command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPhoneNumberIsNotDigitsOnlyOnUpdate()
    {
        var command = new UpdateCustomerCompanyDto
        {
            Address = "Address",
            Email = "email@email.com",
            PhoneNumber = "123456a789",
            Name = "Name"
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.UpdateCompanyCustomer(1, command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPhoneNumberLengthIsNotCorrectOnUpdate()
    {
        var command = new UpdateCustomerCompanyDto
        {
            Address = "Address",
            Email = "email@email.com",
            PhoneNumber = "1234567891",
            Name = "Name"
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.UpdateCompanyCustomer(1, command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenNameIsEmptyOnUpdate()
    {
        var command = new UpdateCustomerCompanyDto
        {
            Address = "Address",
            Email = "email@email.com",
            PhoneNumber = "123456789",
            Name = ""
        };

        await Should.ThrowAsync<DomainException>
            (_companyCustomerService.UpdateCompanyCustomer(1, command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ReturnCompanyCustomerDto_WhenCompanyCustomerIsUpdated()
    {
        var command = new UpdateCustomerCompanyDto
        {
            Address = "Address",
            Email = "email@email.com",
            PhoneNumber = "123456789",
            Name = "Name"
        };

        var result = await _companyCustomerService
            .UpdateCompanyCustomer(1, command, CancellationToken.None);

        result.ShouldBeOfType<CompanyCustomerDto>();
        result.Address.ShouldBe(command.Address);
        result.Email.ShouldBe(command.Email);
        result.PhoneNumber.ShouldBe(command.PhoneNumber);
        result.Name.ShouldBe(command.Name);
    }
}