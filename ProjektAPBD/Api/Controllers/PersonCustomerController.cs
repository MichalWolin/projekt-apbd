using Api.Interfaces;
using Api.RequestModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonCustomerController : ControllerBase
{
    private readonly IPersonCustomerService _personCustomerService;

    public PersonCustomerController(IPersonCustomerService personCustomerService)
    {
        _personCustomerService = personCustomerService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePersonCustomer(NewPersonCustomerDto newPersonCustomerDto)
    {
        return Ok(await _personCustomerService.CreatePersonCustomer(newPersonCustomerDto));
    }
}