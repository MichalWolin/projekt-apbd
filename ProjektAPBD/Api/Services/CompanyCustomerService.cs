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
        EnsureEveryDataIsFilled(newCompanyCustomerDto);

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

        if (updateCustomerCompanyDto.Email is not null)
            EnsureEmailIsCorrect(updateCustomerCompanyDto.Email);
        if (updateCustomerCompanyDto.PhoneNumber is not null)
        {
            EnsurePhoneNumberIsDigitsOnly(updateCustomerCompanyDto.PhoneNumber);
            EnsurePhoneNumberLengthIsCorrect(updateCustomerCompanyDto.PhoneNumber);
        }

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

    private static void EnsureEveryDataIsFilled(NewCompanyCustomerDto newCompanyCustomerDto)
    {
        if (string.IsNullOrEmpty(newCompanyCustomerDto.Address))
        {
            throw new DomainException("Address must be filled!");
        }
        else if (string.IsNullOrEmpty(newCompanyCustomerDto.Email))
        {
            throw new DomainException("Email must be filled!");
        }
        else if (string.IsNullOrEmpty(newCompanyCustomerDto.PhoneNumber))
        {
            throw new DomainException("Phone number must be filled!");
        }
        else if (string.IsNullOrEmpty(newCompanyCustomerDto.Name))
        {
            throw new DomainException("Name must be filled!");
        }
        else if (string.IsNullOrEmpty(newCompanyCustomerDto.Krs))
        {
            throw new DomainException("KRS must be filled!");
        }
    }

    private static void EnsureCustomerExists(int id, Customer? customer)
    {
        if (customer is null)
        {
            throw new DomainException($"Customer with id {id} does not exist!");
        }
    }
}