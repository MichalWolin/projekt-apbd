using Api.Exceptions;
using Api.Interfaces;
using Api.Models;
using Api.RequestModels;
using Api.ResponseModels;

namespace Api.Services;

public class CompanyCustomerService : ICompanyCustomerService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ICustomerRepository _customerRepository;

    public CompanyCustomerService(ICompanyRepository companyRepository, ICustomerRepository customerRepository)
    {
        _companyRepository = companyRepository;
        _customerRepository = customerRepository;
    }

    public async Task<CompanyCustomerDto> CreateCompanyCustomer(NewCompanyCustomerDto newCompanyCustomerDto)
    {
        EnsureAddressIsNotEmpty(newCompanyCustomerDto.Address);
        EnsureEmailIsNotEmpty(newCompanyCustomerDto.Email);
        EnsurePhoneNumberIsNotEmpty(newCompanyCustomerDto.PhoneNumber);
        EnsureNameIsNotEmpty(newCompanyCustomerDto.Name);
        EnsureKrsIsNotEmpty(newCompanyCustomerDto.Krs);

        EnsureKrsIsDigitsOnly(newCompanyCustomerDto);
        EnsureKrsLengthIsCorrect(newCompanyCustomerDto);

        Company? company = await _companyRepository.GetCompany(newCompanyCustomerDto.Krs);
        EnsureCompanyDoesNotExist(newCompanyCustomerDto, company);

        EnsureEmailIsCorrect(newCompanyCustomerDto.Email);
        EnsurePhoneNumberIsDigitsOnly(newCompanyCustomerDto.PhoneNumber);
        EnsurePhoneNumberLengthIsCorrect(newCompanyCustomerDto.PhoneNumber);

        return await _customerRepository.CreateCompanyCustomer(newCompanyCustomerDto);
    }

    public async Task<CompanyCustomerDto> UpdateCompanyCustomer(int id,
                                                                UpdateCustomerCompanyDto updateCustomerCompanyDto)
    {
        Customer? customer = await _customerRepository.GetCustomer(id);
        EnsureCustomerExists(id, customer);
        EnsureCustomerIsCompany(customer);

        if (updateCustomerCompanyDto.Address is not null)
            EnsureAddressIsNotEmpty(updateCustomerCompanyDto.Address);
        if (updateCustomerCompanyDto.Email is not null)
        {
            EnsureEmailIsNotEmpty(updateCustomerCompanyDto.Email);
            EnsureEmailIsCorrect(updateCustomerCompanyDto.Email);
        }
        if (updateCustomerCompanyDto.PhoneNumber is not null)
        {
            EnsurePhoneNumberIsNotEmpty(updateCustomerCompanyDto.PhoneNumber);
            EnsurePhoneNumberIsDigitsOnly(updateCustomerCompanyDto.PhoneNumber);
            EnsurePhoneNumberLengthIsCorrect(updateCustomerCompanyDto.PhoneNumber);
        }
        if (updateCustomerCompanyDto.Name is not null)
            EnsureNameIsNotEmpty(updateCustomerCompanyDto.Name);

        return await _customerRepository.UpdateCompanyCustomer(id, updateCustomerCompanyDto);
    }

    private static void EnsureCompanyDoesNotExist(NewCompanyCustomerDto newCompanyCustomerDto, Company? company)
    {
        if (company is not null)
        {
            throw new DomainException($"Company with KRS {newCompanyCustomerDto.Krs} already exists!");
        }
    }

    private static void EnsureEmailIsCorrect(String email)
    {
        if (!email.Contains("@") || !email.Contains("."))
        {
            throw new DomainException("Email must contain '@' and '.'!");
        }
    }

    private static void EnsurePhoneNumberLengthIsCorrect(String phoneNumber)
    {
        if (phoneNumber.Length != 9)
        {
            throw new DomainException("Phone number must be 9 digits long!");
        }
    }

    private static void EnsureKrsLengthIsCorrect(NewCompanyCustomerDto newCompanyCustomerDto)
    {
        if (newCompanyCustomerDto.Krs.Length != 10)
        {
            throw new DomainException("KRS must be 10 digits long!");
        }
    }

    private static void EnsureKrsIsDigitsOnly(NewCompanyCustomerDto newCompanyCustomerDto)
    {
        if (!newCompanyCustomerDto.Krs.All(char.IsDigit))
        {
            throw new DomainException("KRS must contain only digits!");
        }
    }

    private static void EnsurePhoneNumberIsDigitsOnly(String phoneNumber)
    {
        if (!phoneNumber.All(char.IsDigit))
        {
            throw new DomainException("Phone number must contain only digits!");
        }
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

    private static void EnsureNameIsNotEmpty(String name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new DomainException("Name cannot be empty!");
        }
    }

    private static void EnsureKrsIsNotEmpty(String krs)
    {
        if (string.IsNullOrEmpty(krs))
        {
            throw new DomainException("KRS cannot be empty!");
        }
    }

    private static void EnsureCustomerExists(int id, Customer? customer)
    {
        if (customer is null)
        {
            throw new DomainException($"Customer with id {id} does not exist!");
        }
    }

    private static void EnsureCustomerIsCompany(Customer customer)
    {
        if (customer.PersonId is not null)
        {
            throw new DomainException($"Customer with id {customer.CustomerId} is not a company!");
        }
    }
}