using Api.Exceptions;
using Api.Interfaces;
using Api.Models;
using Api.RequestModels;
using Api.ResponseModels;

namespace Api.Services;

public class PersonCustomerService : IPersonCustomerService
{
    private readonly IPersonRepository _personRepository;
    private readonly ICustomerRepository _customerRepository;

    public PersonCustomerService(IPersonRepository personRepository, ICustomerRepository customerRepository)
    {
        _personRepository = personRepository;
        _customerRepository = customerRepository;
    }

    public async Task<PersonCustomerDto> CreatePersonCustomer(NewPersonCustomerDto newPersonCustomerDto)
    {
        EnsureEveryFieldIsFilled(newPersonCustomerDto);

        EnsurePeselIsDigitsOnly(newPersonCustomerDto);
        EnsurePeselLengthIsCorrect(newPersonCustomerDto);

        Person? person = await _personRepository.GetPerson(newPersonCustomerDto.Pesel);
        EnsurePersonDoesNotExist(newPersonCustomerDto, person);

        EnsureEmailIsCorrect(newPersonCustomerDto.Email);
        EnsurePhoneNumberIsDigitsOnly(newPersonCustomerDto.PhoneNumber);
        EnsurePhoneNumberLengthIsCorrect(newPersonCustomerDto.PhoneNumber);

        return await _customerRepository.CreatePersonCustomer(newPersonCustomerDto);
    }

    private static void EnsureEveryFieldIsFilled(NewPersonCustomerDto newPersonCustomerDto)
    {
        if (string.IsNullOrEmpty(newPersonCustomerDto.Address))
        {
            throw new DomainException("Address must be filled!");
        }

        if (string.IsNullOrEmpty(newPersonCustomerDto.Email))
        {
            throw new DomainException("Email must be filled!");
        }

        if (string.IsNullOrEmpty(newPersonCustomerDto.PhoneNumber))
        {
            throw new DomainException("Phone number must be filled!");
        }

        if (string.IsNullOrEmpty(newPersonCustomerDto.FirstName))
        {
            throw new DomainException("First name must be filled!");
        }

        if (string.IsNullOrEmpty(newPersonCustomerDto.LastName))
        {
            throw new DomainException("Last name must be filled!");
        }

        if (string.IsNullOrEmpty(newPersonCustomerDto.Pesel))
        {
            throw new DomainException("PESEL must be filled!");
        }
    }

    private static void EnsurePeselIsDigitsOnly(NewPersonCustomerDto newPersonCustomerDto)
    {
        if (!newPersonCustomerDto.Pesel.All(char.IsDigit))
        {
            throw new DomainException("PESEL must contain only digits!");
        }
    }

    private static void EnsurePeselLengthIsCorrect(NewPersonCustomerDto newPersonCustomerDto)
    {
        if (newPersonCustomerDto.Pesel.Length != 11)
        {
            throw new DomainException("PESEL must be 11 digits long!");
        }
    }

    private static void EnsureEmailIsCorrect(String email)
    {
        if (!email.Contains('@') || !email.Contains('.'))
        {
            throw new DomainException("Email must contain '@' and '.'!");
        }
    }

    private static void EnsurePersonDoesNotExist(NewPersonCustomerDto newPersonCustomerDto, Person? person)
    {
        if (person != null)
        {
            throw new DomainException($"Person with PESEL {newPersonCustomerDto.Pesel} already exists!");
        }
    }

    private static void EnsurePhoneNumberLengthIsCorrect(String phoneNumber)
    {
        if (phoneNumber.Length != 9)
        {
            throw new DomainException("Phone number must be 9 digits long!");
        }
    }

    private static void EnsurePhoneNumberIsDigitsOnly(String phoneNumber)
    {
        if (!phoneNumber.All(char.IsDigit))
        {
            throw new DomainException("Phone number must contain only digits!");
        }
    }
}