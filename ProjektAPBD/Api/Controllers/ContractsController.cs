using Api.Interfaces;
using Api.RequestModels;
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

    [HttpPost("add")]
    public async Task<IActionResult> CreateContract(NewContractDto newContractDto)
    {
        return Ok(await _contractService.CreateContract(newContractDto));
    }
}