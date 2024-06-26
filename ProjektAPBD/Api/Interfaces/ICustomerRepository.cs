using Api.RequestModels;
using Api.ResponseModels;

namespace Api.Interfaces;

public interface ICustomerRepository
{
    Task<CreatedCompanyCustomerDto> CreateCompanyCustomer(NewCompanyCustomerDto newCompanyCustomerDto);
}