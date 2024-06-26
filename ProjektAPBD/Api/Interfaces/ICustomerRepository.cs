using Api.Models;
using Api.RequestModels;
using Api.ResponseModels;

namespace Api.Interfaces;

public interface ICustomerRepository
{
    Task<CompanyCustomerDto> CreateCompanyCustomer(NewCompanyCustomerDto newCompanyCustomerDto);
    Task<Customer?> GetCustomer(int id);
    Task<CompanyCustomerDto> UpdateCompanyCustomer(int id, UpdateCustomerCompanyDto updateCustomerCompanyDto);
    Task<PersonCustomerDto> CreatePersonCustomer(NewPersonCustomerDto newPersonCustomerDto);
    Task DeletePersonCustomer(Customer customer);
    Task<PersonCustomerDto> UpdatePersonCustomer(int id, UpdatePersonCustomerDto updatePersonCustomerDto);
}