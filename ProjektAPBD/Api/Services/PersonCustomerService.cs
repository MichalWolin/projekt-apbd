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
        EnsureAddressIsNotEmpty(newPersonCustomerDto.Address);
        EnsureEmailIsNotEmpty(newPersonCustomerDto.Email);
        EnsurePhoneNumberIsNotEmpty(newPersonCustomerDto.PhoneNumber);
        EnsureFirstNameIsNotEmpty(newPersonCustomerDto.FirstName);
        EnsureLastNameIsNotEmpty(newPersonCustomerDto.LastName);
        EnsurePeselIsNotEmpty(newPersonCustomerDto.Pesel);

        EnsurePeselIsDigitsOnly(newPersonCustomerDto);
        EnsurePeselLengthIsCorrect(newPersonCustomerDto);

        Person? person = await _personRepository.GetPerson(newPersonCustomerDto.Pesel);
        EnsurePersonDoesNotExist(newPersonCustomerDto, person);

        EnsureEmailIsCorrect(newPersonCustomerDto.Email);
        EnsurePhoneNumberIsDigitsOnly(newPersonCustomerDto.PhoneNumber);
        EnsurePhoneNumberLengthIsCorrect(newPersonCustomerDto.PhoneNumber);

        return await _customerRepository.CreatePersonCustomer(newPersonCustomerDto);
    }

    public async Task DeletePersonCustomer(int id)
    {
        Customer? customer = await _customerRepository.GetCustomer(id);
        EnsureCustomerExists(id, customer);

        EnsureCustomerIsNotDeleted(customer!);
        EnsureCustomerIsNotCompany(customer!);

        await _customerRepository.DeletePersonCustomer(customer!);
    }

    public async Task<PersonCustomerDto> UpdatePersonCustomer(int id, UpdatePersonCustomerDto updatePersonCustomerDto)
    {
        Customer? customer = await _customerRepository.GetCustomer(id);
        EnsureCustomerExists(id, customer);
        EnsureCustomerIsNotDeleted(customer);
        EnsureCustomerIsNotCompany(customer);

        if (updatePersonCustomerDto.Address is not null)
            EnsureAddressIsNotEmpty(updatePersonCustomerDto.Address);
        if (updatePersonCustomerDto.Email is not null)
        {
            EnsureEmailIsNotEmpty(updatePersonCustomerDto.Email);
            EnsureEmailIsCorrect(updatePersonCustomerDto.Email);
        }
        if (updatePersonCustomerDto.PhoneNumber is not null)
        {
            EnsurePhoneNumberIsDigitsOnly(updatePersonCustomerDto.PhoneNumber);
            EnsurePhoneNumberLengthIsCorrect(updatePersonCustomerDto.PhoneNumber);
        }
        if (updatePersonCustomerDto.FirstName is not null)
            EnsureFirstNameIsNotEmpty(updatePersonCustomerDto.FirstName);
        if (updatePersonCustomerDto.LastName is not null)
            EnsureLastNameIsNotEmpty(updatePersonCustomerDto.LastName);

        return await _customerRepository.UpdatePersonCustomer(id, updatePersonCustomerDto);
    }

    private static void EnsureAddressIsNotEmpty(String address)
    {
        if (string.IsNullOrEmpty(address))
        {
            throw new DomainException("Address cannot be empty!");
        }
    }

    private static void EnsureEmailIsNotEmpty(String email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new DomainException("Email cannot be empty!");
        }
    }

    private static void EnsurePhoneNumberIsNotEmpty(String phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber))
        {
            throw new DomainException("Phone number cannot be empty!");
        }
    }

    private static void EnsureFirstNameIsNotEmpty(String firstName)
    {
        if (string.IsNullOrEmpty(firstName))
        {
            throw new DomainException("First name cannot be empty!");
        }
    }

    private static void EnsureLastNameIsNotEmpty(String lastName)
    {
        if (string.IsNullOrEmpty(lastName))
        {
            throw new DomainException("Last name cannot be empty!");
        }
    }

    private static void EnsurePeselIsNotEmpty(String pesel)
    {
        if (string.IsNullOrEmpty(pesel))
        {
            throw new DomainException("PESEL cannot be empty!");
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

    private static void EnsureCustomerExists(int id, Customer? customer)
    {
        if (customer is null)
        {
            throw new DomainException("Customer with id {id} does not exist!");
        }
    }

    private static void EnsureCustomerIsNotDeleted(Customer customer)
    {
        if (customer.IsDeleted)
        {
            throw new DomainException("Customer is already deleted!");
        }
    }

    private static void EnsureCustomerIsNotCompany(Customer customer)
    {
        if (customer.CompanyId != null)
        {
            throw new DomainException("Companies cannot be deleted or modified by this endpoint!");
        }
    }
}