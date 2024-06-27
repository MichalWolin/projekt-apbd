using Api.RequestModels;
using Api.ResponseModels;

namespace Api.Interfaces;

public interface IContractService
{
    Task<ContractDto> CreateContract(NewContractDto newContractDto, CancellationToken cancellationToken);
    Task<PaymentResponseDto> PayForContract(PaymentRequestDto paymentRequestDto, CancellationToken cancellationToken);
}