using Api.RequestModels;
using Api.ResponseModels;

namespace Api.Interfaces;

public interface IContractService
{
    Task<ContractDto> CreateContract(NewContractDto newContractDto);
}