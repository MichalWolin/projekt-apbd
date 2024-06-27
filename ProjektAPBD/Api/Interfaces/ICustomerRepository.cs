using Api.Models;
using Api.RequestModels;
using Api.ResponseModels;

namespace Api.Interfaces;

public interface ICustomerRepository
{
    Task<CompanyCustomerDto> CreateCompanyCustomer(NewCompanyCustomerDto newCompanyCustomerDto,
        CancellationToken cancellationToken);
    Task<Customer?> GetCustomer(int id,
        CancellationToken cancellationToken);
    Task<CompanyCustomerDto> UpdateCompanyCustomer(int id, UpdateCustomerCompanyDto updateCustomerCompanyDto,
        CancellationToken cancellationToken);
    Task<PersonCustomerDto> CreatePersonCustomer(NewPersonCustomerDto newPersonCustomerDto,
        CancellationToken cancellationToken);
    Task DeletePersonCustomer(Customer customer,
        CancellationToken cancellationToken);
    Task<PersonCustomerDto> UpdatePersonCustomer(int id, UpdatePersonCustomerDto updatePersonCustomerDto,
        CancellationToken cancellationToken);
}