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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePersonCustomer(int id)
    {
        await _personCustomerService.DeletePersonCustomer(id);

        return Ok($"Customer with id {id} has been deleted.");
    }
}