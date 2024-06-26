using Api.RequestModels;
using Api.ResponseModels;

namespace Api.Interfaces;

public interface ICompanyCustomerService
{
    Task<CreatedCompanyCustomerDto> CreateCompanyCustomer(NewCompanyCustomerDto newCompanyCustomerDto);
}