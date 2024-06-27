using Api.RequestModels;
using Api.ResponseModels;

namespace Api.Interfaces;

public interface ICompanyCustomerService
{
    Task<CompanyCustomerDto> CreateCompanyCustomer(NewCompanyCustomerDto newCompanyCustomerDto,
        CancellationToken cancellationToken);
    Task<CompanyCustomerDto> UpdateCompanyCustomer(int id, UpdateCustomerCompanyDto updateCustomerCompanyDto,
        CancellationToken cancellationToken);
}