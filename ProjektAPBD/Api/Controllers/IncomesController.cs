using Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncomesController : ControllerBase
{
    private readonly IIncomeService _incomeService;

    public IncomesController(IIncomeService incomeService)
    {
        _incomeService = incomeService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetIncome(int? softwareId, bool anticipatedIncomes = false,
                                                string currency = "PLN")
    {
        return Ok(await _incomeService.GetIncome(softwareId, anticipatedIncomes, currency));
    }
}