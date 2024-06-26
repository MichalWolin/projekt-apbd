using Api.RequestModels;
using Api.ResponseModels;

namespace Api.Interfaces;

public interface IPersonCustomerService
{
    Task<PersonCustomerDto> CreatePersonCustomer(NewPersonCustomerDto newPersonCustomerDto);
    Task DeletePersonCustomer(int id);
}