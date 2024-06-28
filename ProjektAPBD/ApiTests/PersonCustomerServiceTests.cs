using Api.Exceptions;
using Api.Interfaces;
using Api.RequestModels;
using Api.ResponseModels;
using Api.Services;
using ApiTests.TestObjects;
using Shouldly;

namespace ApiTests;

public class PersonCustomerServiceTests
{
    private readonly IPersonCustomerService _personCustomerService;

    public PersonCustomerServiceTests()
    {
        _personCustomerService = new PersonCustomerService(new FakePersonRepository(), new FakeCustomerRepository());
    }

    [Fact]
    public async Task Should_ThrowException_WhenAddressIsEmpty()
    {
        var command = new NewPersonCustomerDto
        {
            Address = "",
            Email = "email@email.com",
            PhoneNumber = "123456789",
            FirstName = "John",
            LastName = "Doe",
            Pesel = "12345678901"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.CreatePersonCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenEmailIsEmpty()
    {
        var command = new NewPersonCustomerDto
        {
            Address = "Street 1",
            Email = "",
            PhoneNumber = "123456789",
            FirstName = "John",
            LastName = "Doe",
            Pesel = "12345678901"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.CreatePersonCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPhoneNumberIsEmpty()
    {
        var command = new NewPersonCustomerDto
        {
            Address = "Some address",
            Email = "email@email.com",
            PhoneNumber = "",
            FirstName = "John",
            LastName = "Doe",
            Pesel = "12345678901"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.CreatePersonCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenFirstNameIsEmpty()
    {
        var command = new NewPersonCustomerDto
        {
            Address = "Some address",
            Email = "email@email.com",
            PhoneNumber = "123132132",
            FirstName = "",
            LastName = "Doe",
            Pesel = "12345678901"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.CreatePersonCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenLastNameIsEmpty()
    {
        var command = new NewPersonCustomerDto
        {
            Address = "Some address",
            Email = "email@email.com",
            PhoneNumber = "123132132",
            FirstName = "adsa",
            LastName = "",
            Pesel = "12345678901"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.CreatePersonCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPeselIsEmpty()
    {
        var command = new NewPersonCustomerDto
        {
            Address = "Some address",
            Email = "email@email.com",
            PhoneNumber = "123132132",
            FirstName = "asdasdasd",
            LastName = "Doe",
            Pesel = ""
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.CreatePersonCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPeselIsNotDigitsOnly()
    {
        var command = new NewPersonCustomerDto
        {
            Address = "Some address",
            Email = "email@email.com",
            PhoneNumber = "123132132",
            FirstName = "adsasdasd",
            LastName = "Doe",
            Pesel = "1234567a8901"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.CreatePersonCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPeselLengthIsNotCorrect()
    {
        var command = new NewPersonCustomerDto
        {
            Address = "Some address",
            Email = "email@email.com",
            PhoneNumber = "123132132",
            FirstName = "dsaasdasd",
            LastName = "Doe",
            Pesel = "123456789011"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.CreatePersonCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPersonExists()
    {
        var command = new NewPersonCustomerDto
        {
            Address = "Some address",
            Email = "email@email.com",
            PhoneNumber = "123132132",
            FirstName = "John",
            LastName = "Doe",
            Pesel = "12345678901"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.CreatePersonCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenEmailIsNotCorrect()
    {
        var command = new NewPersonCustomerDto
        {
            Address = "Some address",
            Email = "email",
            PhoneNumber = "123132132",
            FirstName = "John",
            LastName = "Doe",
            Pesel = "55555555555"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.CreatePersonCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPhoneNumberIsNotDigitsOnly()
    {
        var command = new NewPersonCustomerDto
        {
            Address = "Some address",
            Email = "email@email.com",
            PhoneNumber = "1231321a32",
            FirstName = "John",
            LastName = "Doe",
            Pesel = "55555555555"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.CreatePersonCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPhoneNumberLengthIsNotCorrect()
    {
        var command = new NewPersonCustomerDto
        {
            Address = "Some address",
            Email = "email@email.com",
            PhoneNumber = "1231321132",
            FirstName = "John",
            LastName = "Doe",
            Pesel = "55555555555"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.CreatePersonCustomer(command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ReturnPersonCustomerDto_WhenPersonCustomerIsCreated()
    {
        var command = new NewPersonCustomerDto
        {
            Address = "Some address",
            Email = "email@email.com",
            PhoneNumber = "123121132",
            FirstName = "John",
            LastName = "Doe",
            Pesel = "55555555555"
        };

        var result = await _personCustomerService.CreatePersonCustomer(command, CancellationToken.None);

        result.ShouldBeOfType<PersonCustomerDto>();
        result.Address.ShouldBe(command.Address);
        result.Email.ShouldBe(command.Email);
        result.PhoneNumber.ShouldBe(command.PhoneNumber);
        result.FirstName.ShouldBe(command.FirstName);
        result.LastName.ShouldBe(command.LastName);
        result.Pesel.ShouldBe(command.Pesel);
    }

    [Fact]
    public async Task Should_ThrowException_WhenPersonDoesNotExist()
    {
        await Should.ThrowAsync<DomainException>
            (_personCustomerService.DeletePersonCustomer(7, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPersonIsDeleted()
    {
        await Should.ThrowAsync<DomainException>
            (_personCustomerService.DeletePersonCustomer(3, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPersonIsCompany()
    {
        await Should.ThrowAsync<DomainException>
            (_personCustomerService.DeletePersonCustomer(1, CancellationToken.None));
    }

    [Fact]
    public async Task Should_DeletePersonCustomer_WhenPersonExists()
    {
        await _personCustomerService.DeletePersonCustomer(2, CancellationToken.None);
    }

    [Fact]
    public async Task Should_ThrowException_WhenCustomerDoesNotExist()
    {
        var command = new UpdatePersonCustomerDto
        {
            Address = "Some address",
            Email = "email@email.com",
            PhoneNumber = "123121132",
            FirstName = "John",
            LastName = "Doe"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.UpdatePersonCustomer(7, command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenCustomerIsDeleted()
    {
        var command = new UpdatePersonCustomerDto
        {
            Address = "Some address",
            Email = "email@email.com",
            PhoneNumber = "123121132",
            FirstName = "John",
            LastName = "Doe"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.UpdatePersonCustomer(3, command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenCustomerIsCompany()
    {
        var command = new UpdatePersonCustomerDto
        {
            Address = "Some address",
            Email = "email@email.com",
            PhoneNumber = "123121132",
            FirstName = "John",
            LastName = "Doe"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.UpdatePersonCustomer(1, command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenAddressIsEmptyUpdate()
    {
        var command = new UpdatePersonCustomerDto
        {
            Address = "",
            Email = "email@email.com",
            PhoneNumber = "123121132",
            FirstName = "John",
            LastName = "Doe"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.UpdatePersonCustomer(2, command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenEmailIsEmptyUpdate()
    {
        var command = new UpdatePersonCustomerDto
        {
            Address = "Some address",
            Email = "",
            PhoneNumber = "123121132",
            FirstName = "John",
            LastName = "Doe"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.UpdatePersonCustomer(2, command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenEmailIsNotCorrectUpdate()
    {
        var command = new UpdatePersonCustomerDto
        {
            Address = "Some address",
            Email = "email",
            PhoneNumber = "123121132",
            FirstName = "John",
            LastName = "Doe"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.UpdatePersonCustomer(2, command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPhoneNumberIsEmptyUpdate()
    {
        var command = new UpdatePersonCustomerDto
        {
            Address = "address",
            Email = "email@email.com",
            PhoneNumber = "12a3121132",
            FirstName = "John",
            LastName = "Doe"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.UpdatePersonCustomer(2, command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenPhoneNumberIsNotDigitsOnlyUpdate()
    {
        var command = new UpdatePersonCustomerDto
        {
            Address = "address",
            Email = "email@email.com",
            PhoneNumber = "1231121132",
            FirstName = "John",
            LastName = "Doe"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.UpdatePersonCustomer(2, command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenFirstNameIsEmptyUpdate()
    {
        var command = new UpdatePersonCustomerDto
        {
            Address = "address",
            Email = "email@email.com",
            PhoneNumber = "123121132",
            FirstName = "",
            LastName = "Doe"
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.UpdatePersonCustomer(2, command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ThrowException_WhenLastNameIsEmptyUpdate()
    {
        var command = new UpdatePersonCustomerDto
        {
            Address = "address",
            Email = "email@email.com",
            PhoneNumber = "123121132",
            FirstName = "John",
            LastName = ""
        };

        await Should.ThrowAsync<DomainException>
            (_personCustomerService.UpdatePersonCustomer(2, command, CancellationToken.None));
    }

    [Fact]
    public async Task Should_ReturnPersonCustomerDto_WhenPersonCustomerIsUpdated()
    {
        var command = new UpdatePersonCustomerDto
        {
            Address = "address",
            Email = "email@email.com",
            PhoneNumber = "123121132",
            FirstName = "John",
            LastName = "Doe"
        };

        var result = await _personCustomerService.UpdatePersonCustomer(2, command,
            CancellationToken.None);

        result.ShouldBeOfType<PersonCustomerDto>();
        result.Address.ShouldBe(command.Address);
        result.Email.ShouldBe(command.Email);
        result.PhoneNumber.ShouldBe(command.PhoneNumber);
        result.FirstName.ShouldBe(command.FirstName);
        result.LastName.ShouldBe(command.LastName);
    }
}