using Api.Interfaces;
using Api.RequestModels;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreatePersonCustomer(NewPersonCustomerDto newPersonCustomerDto,
        CancellationToken cancellationToken)
    {
        return Ok(await _personCustomerService.CreatePersonCustomer(newPersonCustomerDto, cancellationToken));
    }

    [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePersonCustomer(int id, UpdatePersonCustomerDto updatePersonCustomerDto,
        CancellationToken cancellationToken)
    {
        return Ok(await _personCustomerService.UpdatePersonCustomer(id, updatePersonCustomerDto, cancellationToken));
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePersonCustomer(int id, CancellationToken cancellationToken)
    {
        await _personCustomerService.DeletePersonCustomer(id, cancellationToken);

        return Ok($"Customer with id {id} has been deleted.");
    }
}