using Api.Interfaces;
using Api.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController : ControllerBase
{
    private readonly IContractService _contractService;

    public ContractsController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [Authorize]
    [HttpPost("add")]
    public async Task<IActionResult> CreateContract(NewContractDto newContractDto)
    {
        return Ok(await _contractService.CreateContract(newContractDto));
    }

    [Authorize]
    [HttpPost("pay")]
    public async Task<IActionResult> PayForContract(PaymentRequestDto paymentRequestDto)
    {
        return Ok(await _contractService.PayForContract(paymentRequestDto));
    }
}