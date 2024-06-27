using Api.RequestModels;
using Api.ResponseModels;

namespace Api.Interfaces;

public interface IPersonCustomerService
{
    Task<PersonCustomerDto> CreatePersonCustomer(NewPersonCustomerDto newPersonCustomerDto,
        CancellationToken cancellationToken);
    Task DeletePersonCustomer(int id, CancellationToken cancellationToken);
    Task<PersonCustomerDto> UpdatePersonCustomer(int id, UpdatePersonCustomerDto updatePersonCustomerDto,
        CancellationToken cancellationToken);
}