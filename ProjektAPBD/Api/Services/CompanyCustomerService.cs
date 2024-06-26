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

    public async Task<CreatedCompanyCustomerDto> CreateCompanyCustomer(NewCompanyCustomerDto newCompanyCustomerDto)
    {
        EnsureKrsIsDigitsOnly(newCompanyCustomerDto);
        EnsureKrsLengthIsCorrect(newCompanyCustomerDto);

        Company? company = await _companyRepository.GetCompany(newCompanyCustomerDto.Krs);
        EnsureCompanyDoesNotExist(newCompanyCustomerDto, company);

        EnsureEmailIsCorrect(newCompanyCustomerDto);
        EnsurePhoneNumberIsDigitsOnly(newCompanyCustomerDto);
        EnsurePhoneNumberLengthIsCorrect(newCompanyCustomerDto);

        return await _customerRepository.CreateCompanyCustomer(newCompanyCustomerDto);
    }

    private static void EnsureCompanyDoesNotExist(NewCompanyCustomerDto newCompanyCustomerDto, Company? company)
    {
        if (company is not null)
        {
            throw new DomainException($"Company with KRS {newCompanyCustomerDto.Krs} already exists!");
        }
    }

    private static void EnsureEmailIsCorrect(NewCompanyCustomerDto newCompanyCustomerDto)
    {
        if (!newCompanyCustomerDto.Email.Contains("@") || !newCompanyCustomerDto.Email.Contains("."))
        {
            throw new DomainException("Email must contain '@' and '.'!");
        }
    }

    private static void EnsurePhoneNumberLengthIsCorrect(NewCompanyCustomerDto newCompanyCustomerDto)
    {
        if (newCompanyCustomerDto.PhoneNumber.Length != 9)
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

    private static void EnsurePhoneNumberIsDigitsOnly(NewCompanyCustomerDto newCompanyCustomerDto)
    {
        if (!newCompanyCustomerDto.PhoneNumber.All(char.IsDigit))
        {
            throw new DomainException("Phone number must contain only digits!");
        }
    }
}